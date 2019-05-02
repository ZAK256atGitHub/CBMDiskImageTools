using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections;
using ZAK256.CBMDiskImageTools.Logic.Core;
namespace ZAK256.CBMDiskImageTools.Ui.CVT2CleanCVT
{
    class CVT2CleanCVT
    {
        static void Main(string[] args)
        {
            string cvtPathFilename;
            string outPathFilename;
            bool ignoreFileSignature;
            try
            {                
                ParseCommandLineArgs(args, out cvtPathFilename, out outPathFilename,out ignoreFileSignature);
                byte[] imageData = DiskImageFile.ReadCvtFile(cvtPathFilename);
                CleanCvt(imageData, outPathFilename, ignoreFileSignature);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                ShowUsageMsg();
            }
            //Console.ReadKey();
        }
        static void ParseCommandLineArgs(string[] args, out string cvtPathFilename, out string outPathFilename, out bool ignoreFileSignature)
        {
            // <filename>                    ... Commodore disk image filename with path ... fix position parameter (at first position)
            // [-i]                          ... ignore file signature
            // [-o <filename>]               ... filename with path of output file       ... option with one value (required)            
            const string OPTION_OUTFILENAME = "-o";
            const string IGNORE_FILE_SIGNATURE = "-i";
            cvtPathFilename = "";
            outPathFilename = "";
            ignoreFileSignature = false;

            Dictionary<String, int[]> arguments = new Dictionary<string, int[]>();
            CmdLineArgsParser.AddArgument(ref arguments, "first", true, true, true, 0);
            CmdLineArgsParser.AddArgument(ref arguments, IGNORE_FILE_SIGNATURE, false, false, false, 0);
            CmdLineArgsParser.AddArgument(ref arguments, OPTION_OUTFILENAME, false, true, true, 0);
            String firstArgValue;
            Dictionary<string, string> optionList = CmdLineArgsParser.Parse(args, arguments, out firstArgValue);
            cvtPathFilename = firstArgValue;
            if (optionList.ContainsKey(OPTION_OUTFILENAME))
            {
                outPathFilename = optionList[OPTION_OUTFILENAME];
            }
            if (optionList.ContainsKey(IGNORE_FILE_SIGNATURE))
            {
                ignoreFileSignature = true;
            }
        }
        static void CleanCvt(byte[] cvtData, string outPathFilename, bool ignoreFileSignature)
        {
            byte[] cleanCvt = GEOSDisk.GetCleanCvtFromCvt(cvtData, ignoreFileSignature);
            DiskImageFile.WriteFile(cleanCvt, outPathFilename);
        }
        public static void ShowUsageMsg()
        {
            Console.WriteLine("Usage: CVT2CleanCVT <CVT input filename> [-i] -o <CVT output filename>");
            Console.WriteLine("[-i] ... ignore file signature");
        }
    }
}
