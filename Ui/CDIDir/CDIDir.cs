﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections;
using ZAK256.CBMDiskImageTools.Logic.Core;
namespace ZAK256.CBMDiskImageTools.Ui.CDIDir
{
    // limitations:
    // -only D64 images
    // -only 1541 image
    // -only single sided images
    // -only 35 tracks per image
    // -only ASCII output no PETSCII

    class CDIDir
    {

        static void Main(string[] args)
        {
            bool showHelpMsg = false;
            if (args.Length == 1)
            {
                string imagePathFilename = args[0]; //@"geos20_d1a.d64";
                try
                {
                    ShowDir(imagePathFilename);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message.ToString());
                    showHelpMsg = true; 
                }
            }
            else
            {
                showHelpMsg = true;
            }
            if (showHelpMsg)
            {
                ShowHelpMsg();
            }
            Console.ReadKey();
        }

        #region Consolen APP (OUTPUT)
        static void ShowDir(string imagePathFilename)
        {
            byte[] bamBlock;
            ArrayList dirEntryArrayList = new ArrayList();
            //string imagePathFilename = @"free.d64";

            bamBlock = DOSDisk.ReadBAMBlock(imagePathFilename);
            Console.Write("0 \"{0}\" {1} {2}"
                , Core.ConvertPETSCII2ASCII(DOSDisk.GetDiskName(bamBlock))
                , Core.ConvertPETSCII2ASCII(DOSDisk.GetDiskID(bamBlock))
                , Core.ConvertPETSCII2ASCII(DOSDisk.GetDOSType(bamBlock)));
            Console.Write("    ");
            Console.Write("|{0}", "DirIndex");
            Console.Write("|{0}", "  GEOS  ");
            Console.Write("|{0}", "MD5");
            Console.WriteLine();
            DOSDisk.FillDirEntryList(bamBlock, imagePathFilename, ref dirEntryArrayList);
            int dirIndex = 0;
            foreach (byte[] de in dirEntryArrayList)
            {
                dirIndex++;
                if (DOSDisk.GetFileType(de) != 0)
                {
                    string filename = Core.ConvertPETSCII2ASCII(DOSDisk.GetFilename(de));
                    Console.Write("{0,-5}\"{1}\"{2}{3}{4}{5}"
                        , DOSDisk.GetFileSizeInBlocks(de).ToString()
                        , filename
                        , Core.ConvertPETSCII2ASCII(DOSDisk.GetPartAfterFilename(de))
                        , DOSDisk.GetSplatFileSign(de)
                        , DOSDisk.GetFileTypeExt(de)
                        , DOSDisk.GetLockFlagSign(de)
                    );
                    Console.Write("  ");
                    Console.Write("|{0,8}", dirIndex.ToString());
                    Console.Write("|{0} {1}"
                            , (GEOSDisk.IsGeosFile(de) ? GEOSDisk.GetGEOSFiletypeName(de) : "   ")
                            , (GEOSDisk.IsGeosFile(de) ? GEOSDisk.GetGEOSFileStructureName(de) : "    ")
                    );
                    Console.Write("|{0}"
                        , DOSDisk.GetMD5ByFile(de, imagePathFilename)
                    );
                    Console.WriteLine();


                    if (GEOSDisk.IsGeosFile(de))
                    {
                        DiskImageFile.WriteCVTFile(de, imagePathFilename, filename);
                    }
                    else
                    {
                        DiskImageFile.WriteFileBlockChain(de[1], de[2], imagePathFilename, filename);
                    }
                }
            }
            Console.WriteLine("{0} BLOCKS FREE."
              , DOSDisk.GetFreeBlocks(bamBlock).ToString()
            );
        }
        public static void ShowHelpMsg()
        {
            Console.WriteLine("Usage: CDIDir [Commodore disk image filename]");
        }
        #endregion
    }
}
