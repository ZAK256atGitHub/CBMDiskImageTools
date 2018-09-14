using System;
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
            openFileDialog1.Filter = "D64 image (*.D64)|*.d64|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                listView1.Items.Clear();
                listView1.Columns[0].TextAlign = HorizontalAlignment.Right;
                string imagePathFilename = openFileDialog1.FileName;
                byte[] bamBlock;
                ArrayList dirEntryList = new ArrayList();
                byte[] imageData = DiskImageFile.ReadFile(imagePathFilename);
                int imageDataType = DiskImageFile.GetImageDataType(imagePathFilename);
                bamBlock = DOSDisk.ReadBAMBlock(imageData, imageDataType);
                textBoxDiskLabel.Text = String.Format("0 \"{0}\" {1} {2}"
                    , Core.ConvertPETSCII2ASCII(DOSDisk.GetDiskName(bamBlock))
                    , Core.ConvertPETSCII2ASCII(DOSDisk.GetDiskID(bamBlock))
                    , Core.ConvertPETSCII2ASCII(DOSDisk.GetDOSType(bamBlock)));

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
                        listViewItem.SubItems.Add(DOSDisk.GetMD5ByFile(de, imageData, imageDataType));
                        listView1.Items.Add(listViewItem);                        
                    }
                }
            }
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
