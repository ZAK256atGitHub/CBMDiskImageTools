using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections;
using ZAK256.CBMDiskImageTools.Logic.Core;

namespace ZAK256.CBMDiskImageTools.Ui.CVTChecksum
{
    class CVTChecksum
    {
        static void Main(string[] args)
        {
            string cvtPathFilename;            
            try
            {
                ParseCommandLineArgs(args, out cvtPathFilename);
                ShowCvtChecksum(cvtPathFilename);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                ShowUsageMsg();
            }
            Console.ReadKey();
        }
        static void ParseCommandLineArgs(string[] args, out string cvtPathFilename)
        {
            // <filename>                    ... Commodore disk image filename with path ... fix position parameter (at first position)
            cvtPathFilename = "";

            Dictionary<String, int[]> arguments = new Dictionary<string, int[]>();
            CmdLineArgsParser.AddArgument(ref arguments, "first", true, true, true, 0);

            String firstArgValue;
            Dictionary<string, string> optionList = CmdLineArgsParser.Parse(args, arguments, out firstArgValue);
            cvtPathFilename = firstArgValue;
        }
        static void ShowCvtChecksum(string cvtPathFilename)
        {
            byte[] imageData = DiskImageFile.ReadCvtFile(cvtPathFilename);
            byte[] cleanCvt = GEOSDisk.GetCleanCvtFromCvt(imageData,false);
            byte[] dirEntry = cleanCvt.Take(30).ToArray();
            string filename = Core.ConvertPETSCII2ASCII(DOSDisk.GetFilename(dirEntry));
            Console.WriteLine("{0,-16} MD5: {1}"
                ,filename
                ,Core.GetMD5Hash(cleanCvt));            
        }
        public static void ShowUsageMsg()
        {
            Console.WriteLine("Usage: CVTChecksum <CVT input filename>");
        }
    }
}
