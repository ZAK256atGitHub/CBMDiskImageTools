﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Collections;
using ZAK256.CBMDiskImageTools.Logic.Core;
namespace ZAK256.CBMDiskImageTools.Ui.CDITools
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            listView1.FullRowSelect = true;
        }
        private void openToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "D64 image (*.D64)|*.d64|D71 image (*.D71)|*.d71|D81 image (*.D81)|*.d81|All files (*.*)|*.*"; // D71 D81
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                doOpenFile(openFileDialog1.FileName);
            }
        }
        private void doOpenFile(string fileName)
        {
            listView1.Items.Clear();
            listView1.Columns[0].TextAlign = HorizontalAlignment.Right;
            
            string imagePathFilename = fileName;
            byte[] bamBlock;
            ArrayList dirEntryList = new ArrayList();
            byte[] imageData = DiskImageFile.ReadFile(imagePathFilename);
            textBoxFilename.Text = fileName + " MD5:" + Core.GetMD5Hash(imageData);
            int imageDataType = DiskImageFile.GetImageDataType(imagePathFilename);            
            bamBlock = DOSDisk.ReadBAMBlock(imageData, imageDataType);
            textBoxDiskLabel.Text = String.Format("0 \"{0}\" {1} {2}"
                , Core.ConvertPETSCII2ASCII(DOSDisk.GetDiskName(bamBlock, imageDataType))
                , Core.ConvertPETSCII2ASCII(DOSDisk.GetDiskID(bamBlock, imageDataType))
                , Core.ConvertPETSCII2ASCII(DOSDisk.GetDOSType(bamBlock, imageDataType)));

            dirEntryList = DOSDisk.GetDirEntryList(bamBlock, imageData, imageDataType);
            int dirIndex = 0;
            foreach (byte[] de in dirEntryList)
            {
                dirIndex++;
                if (!DOSDisk.IsDirEntryEmpty(de))
                {
                    ListViewItem listViewItem = new ListViewItem(Core.ConvertPETSCII2ASCII(DOSDisk.GetFilename(de)) + Core.ConvertPETSCII2ASCII(DOSDisk.GetPartAfterFilename(de))); // first col
                    listViewItem.SubItems.Add(DOSDisk.GetFileSizeInBlocks(de).ToString());
                    listViewItem.SubItems.Add(DOSDisk.GetSplatFileSign(de));
                    listViewItem.SubItems.Add(DOSDisk.GetFileTypeExt(de));
                    listViewItem.SubItems.Add(DOSDisk.GetLockFlagSign(de));
                    listViewItem.SubItems.Add(dirIndex.ToString());
                    listViewItem.SubItems.Add((GEOSDisk.IsGeosFile(de) ? GEOSDisk.GetGEOSFiletypeName(de) : "   "));
                    listViewItem.SubItems.Add((GEOSDisk.IsGeosFile(de) ? GEOSDisk.GetGEOSFileStructureName(de) : "    "));
                    listViewItem.SubItems.Add(DOSDisk.GetMD5ByCBMFile(de, imageData, imageDataType));
                    listViewItem.SubItems.Add(DOSDisk.GetInfoTextByMP3File(de, imageData, imageDataType));
                    listView1.Items.Add(listViewItem);
                }
            }
        }
        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            // if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 0)
            {
                var ext = System.IO.Path.GetExtension(files[0]);
                if (
                    ext.Equals(".D64", StringComparison.CurrentCultureIgnoreCase) ||
                    ext.Equals(".D71", StringComparison.CurrentCultureIgnoreCase) ||
                    ext.Equals(".D81", StringComparison.CurrentCultureIgnoreCase)
                    )
                {
                    e.Effect = DragDropEffects.Copy;
                }                    
            }
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 0)
            {                
                doOpenFile(files[0]);
            }
        }
    }
}
