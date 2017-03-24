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
            try
            {                
                ParseCommandLineArgs(args, out cvtPathFilename, out outPathFilename);
                byte[] imageData = DiskImageFile.ReadCvtFile(cvtPathFilename);
                CleanCvt(imageData, outPathFilename);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                ShowUsageMsg();
            }
            Console.ReadKey();
        }
        static void ParseCommandLineArgs(string[] args, out string cvtPathFilename, out string outPathFilename)
        {
            // <filename>                    ... Commodore disk image filename with path ... fix position parameter (at first position)
            // [-o <filename>]               ... filename with path of output file       ... option with one value (required)
            const string OPTION_OUTFILENAME = "-o";

            cvtPathFilename = "";
            outPathFilename = "";

            Dictionary<String, int[]> arguments = new Dictionary<string, int[]>();
            CmdLineArgsParser.AddArgument(ref arguments, "first", true, true, true, 0);
            CmdLineArgsParser.AddArgument(ref arguments, OPTION_OUTFILENAME, false, true, true, 0);
            String firstArgValue;
            Dictionary<string, string> optionList = CmdLineArgsParser.Parse(args, arguments, out firstArgValue);
            cvtPathFilename = firstArgValue;
            if (optionList.ContainsKey(OPTION_OUTFILENAME))
            {
                outPathFilename = optionList[OPTION_OUTFILENAME];
            }

        }
        static void CleanCvt(byte[] cvtData, string outPathFilename)
        {
            byte[] cleanCvt = GEOSDisk.GetCleanCvtFromCvt(cvtData);
            DiskImageFile.WriteFile(cleanCvt, outPathFilename);
        }
        public static void ShowUsageMsg()
        {
            Console.WriteLine("Usage: CVT2CleanCVT <CVT input filename> -o <CVT output filename>");
        }
    }
}
