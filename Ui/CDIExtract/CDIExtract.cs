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
            string imagePathFilename;
            int dirIndex;
            string asciiCbmFilename;
            string outPathFilename;
            try
            {
                ParseCommandLineArgs(args, out imagePathFilename, out dirIndex, out asciiCbmFilename, out outPathFilename);
                ExtraxctFile(imagePathFilename, dirIndex, asciiCbmFilename, outPathFilename);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                ShowUsageMsg();
            }           
        }
        static void ParseCommandLineArgs(string[] args, out string imagePathFilename, out int dirIndex, out string asciiCbmFilename, out string outPathFilename)
        {
            //// <filename>                    ... Commodore disk image filename with path ... fix position parameter (at first position)
            //// -i <index> | -f <fielename>   ... Commodore DOS filename in ascii         ... option with one value (-i or -f is required)
            //// [-o <filename>]               ... filename with path of output file       ... option with one value (optional)
            const string OPTION_INDEX = "-i";
            const string OPTION_FILENAME = "-f";
            const string OPTION_OUTFILENAME = "-o";

            imagePathFilename = "";
            dirIndex = 0;
            asciiCbmFilename = "";
            outPathFilename = "";

            Dictionary<String, int[]> arguments = new Dictionary<string, int[]>();
            CmdLineArgsParser.AddArgument(ref arguments, "first", true, true, true, 0);
            CmdLineArgsParser.AddArgument(ref arguments, OPTION_INDEX, false, false, true, 1);
            CmdLineArgsParser.AddArgument(ref arguments, OPTION_FILENAME, false, false, true, 1);
            CmdLineArgsParser.AddArgument(ref arguments, OPTION_OUTFILENAME, false, false, true, 0);
            String firstArgValue;
            Dictionary<string, string> optionList = CmdLineArgsParser.Parse(args, arguments, out firstArgValue);
            imagePathFilename = firstArgValue;
            if (optionList.ContainsKey(OPTION_INDEX))
            {
                try
                {
                    dirIndex = Int32.Parse(optionList[OPTION_INDEX]);
                }
                catch
                {
                    throw new Exception(String.Format("The Option {} requires a numeric value!", OPTION_INDEX));
                }
            }
            if (optionList.ContainsKey(OPTION_FILENAME))
            {
                asciiCbmFilename = optionList[OPTION_FILENAME];
            }
            if (optionList.ContainsKey(OPTION_OUTFILENAME))
            {
                outPathFilename = optionList[OPTION_OUTFILENAME];
            }

        }      
        static void ExtraxctFile(string imagePathFilename,int dirIndex, string asciiCbmFilename, string outPathFilename)
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
            if (outPathFilename != "")
            {
                DiskImageFile.WriteFile(fileData, outPathFilename);
            }
            else
            {
                DiskImageFile.WriteFile(fileData, filename);
            }

        }
        public static void ShowUsageMsg()
        {
            Console.WriteLine("Usage: CDIDir [Commodore disk image filename] -i <dir index> | -f <filename> [-o <filename>]");
        }
    }
}
