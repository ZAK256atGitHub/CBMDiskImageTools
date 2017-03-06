using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections;
using ZAK256.CBMDiskImageTools.Logic.Core;
namespace ZAK256.CBMDiskImageTools.Ui.CDIExtract
{
    class CDIExtract
    {
        static void Main(string[] args)
        {
            string imagePathFilename = args[0];
            int dirIndex = 0;
            string asciiCbmFilename = "GEOS KERNAL";
            ExtraxctFile(imagePathFilename, dirIndex, asciiCbmFilename);
        }
        static void ExtraxctFile(string imagePathFilename,int dirIndex, string asciiCbmFilename)
        {
            byte[] bamBlock;
            byte[] dirEntry = null;
            ArrayList dirEntryList = new ArrayList();
            bamBlock = DOSDisk.ReadBAMBlock(imagePathFilename);
            dirEntryList = DOSDisk.GetDirEntryList(bamBlock, imagePathFilename);
            // by DirIndex
            if (dirIndex > 0)
            {
                dirEntry = (byte[])dirEntryList[dirIndex - 1];
            }
            else
            {
                if (asciiCbmFilename.Length > 0)
                {
                    bool found = false;
                    foreach (byte[] de in dirEntryList)
                    {
                        if (Core.ConvertPETSCII2ASCII(DOSDisk.GetFilename(de)) == asciiCbmFilename)
                        {
                            if (!found)
                            {
                                dirEntry = de;
                                found = true;
                            }
                            else
                            {
                                throw new Exception("Filename is not unique!");
                            }                           
                        }
                    }
                    if (!found)
                    {
                        throw new Exception(String.Format("Filename {0} not found!",asciiCbmFilename));
                    }
                }
                else
                {
                    throw new Exception("No file specified");
                }
            }
            string filename = (Core.ConvertPETSCII2ASCII(DOSDisk.GetFilename(dirEntry)));
            if (DOSDisk.GetFileType(dirEntry) == 0)
            {
                throw new Exception(String.Format("File {0} is deleted!", filename));
            }
            byte[] fileData = DOSDisk.getFileData(dirEntry, imagePathFilename);
            DiskImageFile.WriteFile(fileData,filename);
            Console.WriteLine("OK");
            Console.ReadKey();
        }

    }
}
