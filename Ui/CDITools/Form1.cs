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
namespace CDITools
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            listView1.FullRowSelect = true;
            listView1.Columns.Add("",0); // empty first col, first col is every time HorizontalAlignment.LEFT
            listView1.Columns.Add("Blocks", 50, HorizontalAlignment.Right);
            listView1.Columns.Add("Filename", 150, HorizontalAlignment.Left);
            listView1.Columns.Add(" ", 30, HorizontalAlignment.Center);
            listView1.Columns.Add(" ", 50, HorizontalAlignment.Left);
            listView1.Columns.Add(" ", 30, HorizontalAlignment.Center);
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
                bamBlock = DOSDisk.ReadBAMBlock(imagePathFilename);
                dirEntryList = DOSDisk.GetDirEntryList(bamBlock, imagePathFilename);
                foreach (byte[] de in dirEntryList)
                {
                    ListViewItem listViewItem = new ListViewItem(""); // first col empty
                    listViewItem.SubItems.Add(DOSDisk.GetFileSizeInBlocks(de).ToString());
                    listViewItem.SubItems.Add(Core.ConvertPETSCII2ASCII(DOSDisk.GetFilename(de)) + Core.ConvertPETSCII2ASCII(DOSDisk.GetPartAfterFilename(de)));
                    listViewItem.SubItems.Add(DOSDisk.GetSplatFileSign(de));
                    listViewItem.SubItems.Add(DOSDisk.GetFileTypeExt(de));
                    listViewItem.SubItems.Add(DOSDisk.GetLockFlagSign(de));
                    listView1.Items.Add(listViewItem);
                }
            }
        }
    }
}
