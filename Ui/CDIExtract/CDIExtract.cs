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
            
            string imagePathFilename = "";
            int dirIndex = 0;
            string asciiCbmFilename = "";
            string outPathFilename = "";

            try
            {
                ParseCommandLineArgs(args, ref imagePathFilename, ref dirIndex, ref asciiCbmFilename, ref outPathFilename);
                ExtraxctFile(imagePathFilename, dirIndex, asciiCbmFilename, outPathFilename);
            }            
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                ShowUsageMsg();
            }
        }
        static void ParseCommandLineArgs(string[] args,ref string imagePathFilename,ref int dirIndex,ref  string asciiCbmFilename,ref string outPathFilename)
        {
            // <filename>                    ... Commodore disk image filename with path ... fix position parameter (at first position)
            // -i <index> | -f <fielename>   ... Commodore DOS filename in ascii         ... option with one value (-i or -f is required)
            // [-o <filename>]               ... filename with path of output file       ... option with one value (optional)
            imagePathFilename = "";
            dirIndex = 0;
            asciiCbmFilename = "";
            outPathFilename = "";
            
            ArrayList options = new ArrayList();
            options.Add("-i");
            options.Add("-f");
            options.Add("-o");
            string currOption = "";
            Dictionary<string, string> existArgs = new Dictionary<string, string>();

            if (args.Length <= 0)
            {
                throw new Exception("Parameters are required!");
            }
            if (options.IndexOf(args[0]) >= 0) 
            {
                // the first argument must been a filename
                throw new Exception("Commodore disk image filename is required!");
            }
            imagePathFilename = args[0];       
            // split arguments and values
            // check of duplicate options
            for (int argsIndex = 1; argsIndex < args.Length; argsIndex++)
            {
                if (options.IndexOf(args[argsIndex]) >= 0)
                {
                    // Option
                    currOption = args[argsIndex];
                    if (existArgs.ContainsKey(currOption))
                    {
                        throw new Exception(String.Format("Option {0} occurs several times!", currOption));
                    }
                    else
                    { 
                        existArgs.Add(currOption, "");
                    }
                }
                else
                {
                    if (currOption != "")
                    {
                        existArgs.Remove(currOption);
                        existArgs.Add(currOption, args[argsIndex]);
                        currOption = "";
                    }
                    else
                    {
                        throw new Exception(String.Format("The value {0} can not be assigned to any option!", args[argsIndex]));
                    }
                }
            }
            // check options ans values
            
            if ((existArgs.ContainsKey("-i")) && (existArgs.ContainsKey("-f")))
            {
                throw new Exception("The options -i and -f can not be used at the same time!");
            }
            if ((!existArgs.ContainsKey("-i")) && (!existArgs.ContainsKey("-f")))
            {
                throw new Exception("Option -i or -f is required!");
            }
            if (existArgs.ContainsKey("-i"))
            {
                if (existArgs["-i"] == "")
                {
                    throw new Exception("The option -i require a value!");
                }
                try
                {
                    dirIndex = Int32.Parse(existArgs["-i"]);
                }
                catch
                {
                    throw new Exception("The Option -i requires a numeric value!");
                }
            }
            if (existArgs.ContainsKey("-f"))
            {
                if (existArgs["-f"] == "")
                {
                    throw new Exception("The option -f require a value!");
                }
                asciiCbmFilename = existArgs["-f"];
            }
            if (existArgs.ContainsKey("-o"))
            {
                if (existArgs["-o"] == "")
                {
                    throw new Exception("The option -o require a value!");
                }
                outPathFilename = existArgs["-o"];
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
