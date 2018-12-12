using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

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
            openFileDialog1.Filter = "D64 image (*.D64)|*.d64|G64 image (*.G64)|*.g64|D71 image (*.D71)|*.d71|D81 image (*.D81)|*.d81|All files (*.*)|*.*"; // D71 D81 G64
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
            textBoxInfoText.Text = "";
            string[] MP3FileArray = {
              "StartMP3_64",
              "StartMP64.1",
              "StartMP64.2",
              "StartMP64.3",
              "StartMP64.4",
              "StartMP64.5",
              "StartMP3_128",
              "StartMP128.1",
              "StartMP128.2",
              "StartMP128.3",
              "StartMP128.4",
              "StartMP128.5"
            };
            listView1.Columns[0].TextAlign = HorizontalAlignment.Right;
            
            string imagePathFilename = fileName;
            byte[] bamBlock;
            ArrayList dirEntryList = new ArrayList();
            byte[] imageData = DiskImageFile.ReadFile(imagePathFilename);
            String md5 = Core.GetMD5Hash(imageData);
            textBoxFilename.Text = fileName + " MD5:" + md5;

            int imageDataType = DiskImageFile.GetImageDataType(imagePathFilename);            
            bamBlock = DOSDisk.ReadBAMBlock(imageData, imageDataType);
            textBoxDiskLabel.Text = String.Format("0 \"{0}\" {1} {2}"
                , Core.ConvertPETSCII2ASCII(DOSDisk.GetDiskName(bamBlock, imageDataType))
                , Core.ConvertPETSCII2ASCII(DOSDisk.GetDiskID(bamBlock, imageDataType))
                , Core.ConvertPETSCII2ASCII(DOSDisk.GetDOSType(bamBlock, imageDataType)));

            dirEntryList = DOSDisk.GetDirEntryList(bamBlock, imageData, imageDataType);

            textBoxInfoText.Text += "{|class=\"wikitable\"" + Environment.NewLine;
            textBoxInfoText.Text += "|" + Environment.NewLine;
            textBoxInfoText.Text += "{|" + Environment.NewLine;
            textBoxInfoText.Text += "| '''Imagedatei'''" + Environment.NewLine;
            textBoxInfoText.Text += "| ''':'''" + Environment.NewLine;
            textBoxInfoText.Text += "|" + Path.GetFileName(imagePathFilename) + Environment.NewLine;
            textBoxInfoText.Text += "|-" + Environment.NewLine;
            textBoxInfoText.Text += "|'''Imagedatei MD5'''" + Environment.NewLine;
            textBoxInfoText.Text += "|''':'''" + Environment.NewLine;
            textBoxInfoText.Text += "|" + md5 + Environment.NewLine;
            textBoxInfoText.Text += "|-" + Environment.NewLine;
            textBoxInfoText.Text += "|'''Quelle'''" + Environment.NewLine;
            textBoxInfoText.Text += "|''':'''" + Environment.NewLine;
            textBoxInfoText.Text += "|" + Environment.NewLine;
            textBoxInfoText.Text += "|}" + Environment.NewLine;
            textBoxInfoText.Text += "{| class=\"wikitable sortable\"" + Environment.NewLine;
            textBoxInfoText.Text += "!Nr." + Environment.NewLine;
            textBoxInfoText.Text += "!Datei" + Environment.NewLine;
            textBoxInfoText.Text += "!style=\"text-align:right\"|Größe" + Environment.NewLine;
            textBoxInfoText.Text += "!MD5" + Environment.NewLine;
            textBoxInfoText.Text += "!Info" + Environment.NewLine;

            int dirIndex = 0;
            foreach (byte[] de in dirEntryList)
            {
                dirIndex++;
                if (!DOSDisk.IsDirEntryEmpty(de))
                {
                    String[] colText = {
                        Core.ConvertPETSCII2ASCII(DOSDisk.GetFilename(de)) + Core.ConvertPETSCII2ASCII(DOSDisk.GetPartAfterFilename(de)),
                        DOSDisk.GetFileSizeInBlocks(de).ToString(),
                        DOSDisk.GetSplatFileSign(de),
                        DOSDisk.GetFileTypeExt(de),
                        DOSDisk.GetLockFlagSign(de),
                        dirIndex.ToString(),
                        (GEOSDisk.IsGeosFile(de) ? GEOSDisk.GetGEOSFiletypeName(de) : "   "),
                        (GEOSDisk.IsGeosFile(de) ? GEOSDisk.GetGEOSFileStructureName(de) : "    "),
                        DOSDisk.GetMD5ByCBMFile(de, imageData, imageDataType),
                        DOSDisk.GetInfoTextByMP3File(de, imageData, imageDataType),
                    };
                    ListViewItem listViewItem = new ListViewItem(colText[0]); // first col
                    listViewItem.SubItems.Add(colText[1]);
                    listViewItem.SubItems.Add(colText[2]);
                    listViewItem.SubItems.Add(colText[3]);
                    listViewItem.SubItems.Add(colText[4]);
                    listViewItem.SubItems.Add(colText[5]);
                    listViewItem.SubItems.Add(colText[6]);
                    listViewItem.SubItems.Add(colText[7]);
                    listViewItem.SubItems.Add(colText[8]);
                    listViewItem.SubItems.Add(colText[9]);
                    listView1.Items.Add(listViewItem);
                    //textBoxInfoText.Text += String.Format("{0,-16} {1,5}  {2,-32} {3}{4}", colText[0],colText[1],colText[8], colText[9],Environment.NewLine);
                    Boolean IsBold = MP3FileArray.Contains(colText[0].TrimEnd());
                    textBoxInfoText.Text += "|-" + Environment.NewLine;
                    textBoxInfoText.Text += "|" + dirIndex + Environment.NewLine;
                    if (IsBold)
                        textBoxInfoText.Text += "|'''" + colText[0].TrimEnd() + "'''" + Environment.NewLine;
                    else
                        textBoxInfoText.Text += "|" + colText[0].TrimEnd() + Environment.NewLine;
                    textBoxInfoText.Text += "|style = \"text-align:right\" |" + colText[1] + Environment.NewLine;
                    textBoxInfoText.Text += "|" + colText[8] + Environment.NewLine;
                    textBoxInfoText.Text += "|" + colText[9] + Environment.NewLine;
                }
            }
            textBoxInfoText.Text += "|}" + Environment.NewLine;
            textBoxInfoText.Text += "|}" + Environment.NewLine;
    /*
{|class="wikitable"
|
  {|
  |'''Imagedatei'''
  |''':'''
  | MP3_64D.D81
  |-
  |'''Imagedatei MD5'''
  |''':'''
  | 93C4E05B0C50BBD6A4B6A9976FA6B35D
  |-
  |'''Quelle'''
  |''':'''
  |
  |}
  {| class="wikitable sortable"
  !Datei
  !style="text-align:right"|Größe
  !MD5
  !Info
  |-
  |StartMP3_64
  |style="text-align:right"|56
  |9296FD29D63ED70733F943ACF7B4FD88
  |BUILD:FULL-090100.2020 Deutsch C64
  |}
|} 
    */
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
                    ext.Equals(".G64", StringComparison.CurrentCultureIgnoreCase) ||
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

        private void button1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBoxInfoText.Text);
        }

        public void CopyListBox(ListView list)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in list.SelectedItems)
            {
                ListViewItem l = item as ListViewItem;
                if (l != null)
                    foreach (ListViewItem.ListViewSubItem sub in l.SubItems)
                        sb.Append(sub.Text + "\t");
                sb.AppendLine();
            }
            Clipboard.SetDataObject(sb.ToString());
        }

        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                CopyListBox(listView1);
            }
            if (e.KeyCode == Keys.A && e.Control)
            {
                listView1.MultiSelect = true;
                foreach (ListViewItem item in listView1.Items)
                {
                    item.Selected = true;
                }
            }
        }
    }
}
