using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections;
using ZAK256.CBMDiskImageTools.Logic.Core;
using System.Collections.Generic;
using System.Resources;

namespace ZAK256.CBMDiskImageTools.Test.UnitTest
{
    // Test files:
    // 8EB414AB37B23A1D1D348D456896A1B0* APPS64.D64
    // F004B907634A30C21D4DF39E362C0789* GEOS64.D64
    // 5518FA80D16F09EB8DF16749423FE74A* GPT64.CVT
    // 7BB3438CBE86A08448BB03585AF68787* GW64.CVT

    [TestClass]
    public class UnitTestCore
    {
        [TestMethod]
        public void TestMethod_GEOS64_D64()
        {
            ResourceManager LocRM = new ResourceManager("ZAK256.CBMDiskImageTools.Test.UnitTest.Resources.cbmfiles.com_GEOS64_ZIP", System.Reflection.Assembly.GetExecutingAssembly());

            byte[] imageData = (byte[])LocRM.GetObject("GEOS64_D64");

            ArrayList dirEntryList = new ArrayList();
            //byte[] imageData = Properties.Resources.GEOS64_D64;

            Assert.AreEqual("F004B907634A30C21D4DF39E362C0789", Core.GetMD5Hash(imageData), "The test file is incorrect!");
            int imageDataType = 0; // sorry hard coding 0 = D64
            byte[] bamBlock = DOSDisk.ReadBAMBlock(imageData, imageDataType);
            dirEntryList = DOSDisk.GetDirEntryList(bamBlock, imageData, imageDataType);



            // 0 "System          " 00 2A    |DirIndex|  GEOS  |MD5
            // 2    "GEOS"             PRG   |       1|BAS SEQ |D4189F0FA391276504B680E1205402D6
            // 86   "GEOBOOT"          PRG   |       2|BAS SEQ |2992D902F031FF10BE10E74B64779DF6
            // 78   "CONFIGURE"        USR   |       3|AUT VLIR|48D1D7B7A765C8492D4A7C76B209680C
            // 120  "DESK TOP"         USR   |       4|SYS VLIR|F5D400333C8A6BCA4DFFCEFC2AF4AE93
            // 3    "JOYSTICK"         USR   |       5|INP SEQ |8A6C7F925A94BE2A9F7D3F2703E17E75
            // 5    "MPS-803"          USR   |       6|PRN SEQ |EFEA6E62E40F3201E3A068D9C55246CF
            // 22   "preference mgr"   USR   |       7|ACC SEQ |B3AA1A856F31E45491C194E1998D8545
            // 22   "pad color mgr"    USR   |       8|ACC SEQ |A36BFC4C78DD76E95073A87B3F11B796
            // 13   "alarm clock"      USR   |       9|ACC SEQ |A409DF281F24BA040C71C43E4255DBCF
            // 18   "PAINT DRIVERS"    USR   |      10|APP SEQ |F1A989AA4D8C9EEFA15F525C20BFF072
            // 2    "RBOOT"            PRG   |      11|BAS SEQ |16F8EF09CAE68824A07A788E3894995B
            // 4    "Star NL-10(com)"  USR   |      12|PRN SEQ |EA73673D31DE636B01DCF77EB858C1A0
            // 3    "ASCII Only"       USR   |      13|PRN SEQ |D332C0F0C9CAB30C76595E4A1E3D1275
            // 3    "COMM 1351"        USR   |      14|INP SEQ |7D531B58F898208E1CBFD1C34AF58778
            // 3    "COMM 1351(a)"     USR   |      15|INP SEQ |CAACED9DF9C176DA2C8A3071A0D0896A
            // 20   "CONVERT"          USR   |      16|APP SEQ |71507FC1130C74FAC0F641331436EB75
            // 259 BLOCKS FREE.
            List<List<string>> myList = new List<List<string>>();
            myList.Add(new List<string> { "2", "GEOS", "PRG", "BAS", "SEQ", "D4189F0FA391276504B680E1205402D6" });
            myList.Add(new List<string> { "86", "GEOBOOT", "PRG", "BAS", "SEQ", "2992D902F031FF10BE10E74B64779DF6" });
            myList.Add(new List<string> { "78", "CONFIGURE", "USR", "AUT", "VLIR", "48D1D7B7A765C8492D4A7C76B209680C" });
            myList.Add(new List<string> { "120", "DESK TOP", "USR", "SYS", "VLIR", "F5D400333C8A6BCA4DFFCEFC2AF4AE93" });
            myList.Add(new List<string> { "3", "JOYSTICK", "USR", "INP", "SEQ", "8A6C7F925A94BE2A9F7D3F2703E17E75" });
            myList.Add(new List<string> { "5", "MPS-803", "USR", "PRN", "SEQ", "EFEA6E62E40F3201E3A068D9C55246CF" });
            myList.Add(new List<string> { "22", "preference mgr", "USR", "ACC", "SEQ", "B3AA1A856F31E45491C194E1998D8545" });
            myList.Add(new List<string> { "22", "pad color mgr", "USR", "ACC", "SEQ", "A36BFC4C78DD76E95073A87B3F11B796" });
            myList.Add(new List<string> { "13", "alarm clock", "USR", "ACC", "SEQ", "A409DF281F24BA040C71C43E4255DBCF" });
            myList.Add(new List<string> { "18", "PAINT DRIVERS", "USR", "APP", "SEQ", "F1A989AA4D8C9EEFA15F525C20BFF072" });
            myList.Add(new List<string> { "2", "RBOOT", "PRG", "BAS", "SEQ", "16F8EF09CAE68824A07A788E3894995B" });
            myList.Add(new List<string> { "4", "Star NL-10(com)", "USR", "PRN", "SEQ", "EA73673D31DE636B01DCF77EB858C1A0" });
            myList.Add(new List<string> { "3", "ASCII Only", "USR", "PRN", "SEQ", "D332C0F0C9CAB30C76595E4A1E3D1275" });
            myList.Add(new List<string> { "3", "COMM 1351", "USR", "INP", "SEQ", "7D531B58F898208E1CBFD1C34AF58778" });
            myList.Add(new List<string> { "3", "COMM 1351(a)", "USR", "INP", "SEQ", "CAACED9DF9C176DA2C8A3071A0D0896A" });
            myList.Add(new List<string> { "20", "CONVERT", "USR", "APP", "SEQ", "71507FC1130C74FAC0F641331436EB75" });
            int index = 0;
            int dirIndex = 0;
            foreach (byte[] dirEntry in dirEntryList)
            {
                dirIndex++;
                if (DOSDisk.GetFileType(dirEntry) != 0)
                {
                    List<string> currList = myList[index];

                    string expectedFileSizeInBlocks = currList[0];
                    string fileSizeInBlocks = DOSDisk.GetFileSizeInBlocks(dirEntry).ToString();
                    Assert.AreEqual(expectedFileSizeInBlocks, fileSizeInBlocks, "The file size is incorrect.");
                    Console.WriteLine("File Size {0} = {1}", expectedFileSizeInBlocks, fileSizeInBlocks); // see at test explorer --> "output"   

                    string expectedFilename = currList[1];
                    string filename = Core.ConvertPETSCII2ASCII(DOSDisk.GetFilename(dirEntry));
                    Assert.AreEqual(expectedFilename, filename, "The filename is incorrect.");
                    Console.WriteLine("Filename {0} = {1}", expectedFilename, filename); // see at test explorer --> "output"            

                    string expectedFileTypeExt = currList[2];
                    string fileTypeExt = DOSDisk.GetFileTypeExt(dirEntry);
                    Assert.AreEqual(expectedFileTypeExt, fileTypeExt, "The file type is incorrect.");
                    Console.WriteLine("File Type {0} = {1}", expectedFileTypeExt, fileTypeExt); // see at test explorer --> "output"       

                    string expectedGEOSFiletypeName = currList[3];
                    string GEOSFiletypeName = GEOSDisk.GetGEOSFiletypeName(dirEntry);
                    Assert.AreEqual(expectedGEOSFiletypeName, GEOSFiletypeName, "The GEOS filetype is incorrect.");
                    Console.WriteLine("GEOS filetype {0} = {1}", expectedGEOSFiletypeName, GEOSFiletypeName); // see at test explorer --> "output"

                    string expectedGEOSFileStructureName = currList[4];
                    string GEOSFileStructureName = GEOSDisk.GetGEOSFileStructureName(dirEntry).Trim();
                    Assert.AreEqual(expectedGEOSFileStructureName, GEOSFileStructureName, "The GEOS file structure is incorrect.");
                    Console.WriteLine("GEOS file structure {0} = {1}", expectedGEOSFileStructureName, GEOSFileStructureName); // see at test explorer --> "output"

                    string expectedMd5 = currList[5];
                    string md5 = DOSDisk.GetMD5ByFile(dirEntry, imageData, imageDataType);
                    Assert.AreEqual(expectedMd5, md5, "The MD5 checksum is incorrect.");
                    Console.WriteLine("MD5 {0} = {1}", expectedMd5, md5); // see at test explorer --> "output"                    
                }
                index++;
            }
            Assert.AreEqual(16, dirIndex, "The number of fils is incorrect.");
        }
        [TestMethod]
        public void TestMethod_APPS64_D64()
        {
            ResourceManager ResMgrGEOS64_ZIP = new ResourceManager("ZAK256.CBMDiskImageTools.Test.UnitTest.Resources.cbmfiles.com_GEOS64_ZIP", System.Reflection.Assembly.GetExecutingAssembly());
            ResourceManager ResMgrCVT = new ResourceManager("ZAK256.CBMDiskImageTools.Test.UnitTest.Resources.cbmfiles.com_CVT", System.Reflection.Assembly.GetExecutingAssembly());
            ArrayList dirEntryList = new ArrayList();
            byte[] imageData = (byte[])ResMgrGEOS64_ZIP.GetObject("APPS64_D64");
            Assert.AreEqual("8EB414AB37B23A1D1D348D456896A1B0", Core.GetMD5Hash(imageData), "The test file is incorrect!");
            int imageDataType = 0; // sorry hard coding 0 = D64
            byte[] bamBlock = DOSDisk.ReadBAMBlock(imageData, imageDataType);
            dirEntryList = DOSDisk.GetDirEntryList(bamBlock, imageData, imageDataType);

            // 0 "Applications    " ML 2A | DirIndex | GEOS | MD5
            // 120  "DESK TOP"  USR | 1 | SYS VLIR | F5D400333C8A6BCA4DFFCEFC2AF4AE93
            // 141  "GEOWRITE"  USR | 2 | APP VLIR | 7F16CB41C1C885BB02D118FC0411C1C6
            // 152  "GEOPAINT"  USR | 3 | APP VLIR | 492A38CDA1A9385DD8F501C043352514
            // 41   "photo manager"  USR | 4 | ACC SEQ | FD24833DA9184D6B28A0C376FA524D91
            // 15   "calculator"  USR | 5 | ACC SEQ | C1B61367915D2360AE6030DA96EB38A4
            // 19   "note pad"  USR | 6 | ACC SEQ | CCCB6812019B90B9B8370664C6419E71
            // 26   "California"  USR | 7 | FNT VLIR | C0F2E40D33FBB81390FB258F6B6FF702
            // 23   "Cory"  USR | 8 | FNT VLIR | 8B2B387AC0EA5AC66D15A25B2FAD6ECB
            // 13   "Dwinelle"  USR | 9 | FNT VLIR | 47E58B32C121C8CCDF49806EE59F21F7
            // 34   "Roma"  USR | 10 | FNT VLIR | 1F10A57D2052DF486AE5BAAAD3C89A83
            // 40   "University"  USR | 11 | FNT VLIR | 8D464DC9D97612168A025435AEB15CBE
            // 7    "Commodore"  USR | 12 | FNT VLIR | 52D872F4FAAFD30400282F085E990C63
            // 9    "ReadMe"  USR | 13 | DOC VLIR | 72E1B96410FDF050BC7F2EF2C28D0B09
            // 23 BLOCKS FREE.

            // "GEOWRITE"
            byte[] dirEntry = (byte[])dirEntryList[1];
            string filename = Core.ConvertPETSCII2ASCII(DOSDisk.GetFilename(dirEntry));
            string expectedMd5 = "7F16CB41C1C885BB02D118FC0411C1C6";

            string md5 = DOSDisk.GetMD5ByFile(dirEntry, imageData, imageDataType);
            Assert.AreEqual(expectedMd5, md5, "The MD5 checksum is incorrect.");
            Console.WriteLine("Filename = {0}", filename);
            Console.WriteLine("MD5 {0} = {1}", expectedMd5, md5); // see at test explorer --> "output"

            byte[] cleanCvt = GEOSDisk.GetCleanCvtFromCvt((byte[])ResMgrCVT.GetObject("GW64_CVT"));
            Assert.AreEqual("7BB3438CBE86A08448BB03585AF68787", Core.GetMD5Hash((byte[])ResMgrCVT.GetObject("GW64_CVT")), "The test file is incorrect!");
            string cvtMD5 = Core.GetMD5Hash(cleanCvt);
            Assert.AreEqual(expectedMd5, cvtMD5, "The MD5 checksum of CVT file is incorrect.");

            // "GEOPAINT"
            dirEntry = (byte[])dirEntryList[2];
            filename = Core.ConvertPETSCII2ASCII(DOSDisk.GetFilename(dirEntry));
            expectedMd5 = "492A38CDA1A9385DD8F501C043352514";

            md5 = DOSDisk.GetMD5ByFile(dirEntry, imageData, imageDataType);
            Assert.AreEqual(expectedMd5, md5, "The MD5 checksum is incorrect.");
            Console.WriteLine("Filename = {0}", filename);
            Console.WriteLine("MD5 {0} = {1}", expectedMd5, md5); // see at test explorer --> "output"

            cleanCvt = GEOSDisk.GetCleanCvtFromCvt((byte[])ResMgrCVT.GetObject("GPT64_CVT"));
            Assert.AreEqual("5518FA80D16F09EB8DF16749423FE74A", Core.GetMD5Hash((byte[])ResMgrCVT.GetObject("GPT64_CVT")), "The test file is incorrect!");
            cvtMD5 = Core.GetMD5Hash(cleanCvt);
            Assert.AreEqual(expectedMd5, cvtMD5, "The MD5 checksum of CVT file is incorrect.");
        }
        [TestMethod]
        public void TestMethod_GEOS64_ZIP_vs_CVT()
        {
            // Ein Dictionary mit den MD5 Prüfsummen aller CVT Datein wird anlegen.
            Dictionary<string, string> cvtMD5Dict = new Dictionary<string, string>();
            cvtMD5Dict.Add("ALARM64_CVT", "379562fce34ff3aa92ac1bf94ab0c82a");
            cvtMD5Dict.Add("ASC_CVT", "9e50e4f08f6362684ccc0b2c40f5bacd");
            cvtMD5Dict.Add("CALC64_CVT", "a64b5be35b6ecdfb73d36b6af2037ee7");
            cvtMD5Dict.Add("CALIF_CVT", "63774ddbd94c6d3c84021b540a5cc352");
            cvtMD5Dict.Add("COM1351A_CVT", "36e9ce9693babce60c22f6418e884cdf");
            cvtMD5Dict.Add("COMM1351_CVT", "b84b6ca934d95399d4f23fd719d27135");
            cvtMD5Dict.Add("COMMFONT_CVT", "ae09c60ed90700b37c0db1043bc1da7b");
            cvtMD5Dict.Add("CORY_CVT", "c25d5fb668ce7913dd5f78bafd5f861b");
            cvtMD5Dict.Add("DICT_CVT", "8b064b856eba099bb639265a09e9935f");
            cvtMD5Dict.Add("DWIN_CVT", "eb734844875c33d4b225cfa4c6cfbb73");
            cvtMD5Dict.Add("GEOLASER_CVT", "93b40e916e698d0a47f0d5d8d28a0d00");
            cvtMD5Dict.Add("GM64_CVT", "7de625e9d7717d57774ac7fae7860d0e");
            cvtMD5Dict.Add("GPT64_CVT", "5518fa80d16f09eb8df16749423fe74a");
            cvtMD5Dict.Add("GW64_CVT", "7bb3438cbe86a08448bb03585af68787");
            cvtMD5Dict.Add("JOYSTICK_CVT", "9e35f7d3c2183fa1a3a9caaa551ea342");
            cvtMD5Dict.Add("LWBARR_CVT", "7a9a5764bc4fb097ba95d549a26ebc7b");
            cvtMD5Dict.Add("LWCAL_CVT", "57770a581fda04cd57e804f7d6a0731e");
            cvtMD5Dict.Add("LWGREEK_CVT", "95bca643dc832d061a7601dd4ce8dc48");
            cvtMD5Dict.Add("LWROMA_CVT", "bfc38a9c73fd4e348b136a35b58a8311");
            cvtMD5Dict.Add("MPS803_CVT", "b322c99316a0e3c34be6bd8cbfb0a571");
            cvtMD5Dict.Add("NOTE64_CVT", "c311d37f2b19e28115b54989511ca7ec");
            cvtMD5Dict.Add("PDMGR64_CVT", "d23fddc59754fc9cc352b5fe81728996");
            cvtMD5Dict.Add("PHMGR64_CVT", "97420e1433004b66e2688afeac3e20ea");
            cvtMD5Dict.Add("PNTDRVRS_CVT", "24a38885fcd5a3338ccc2acf666a0862");
            cvtMD5Dict.Add("PRMGR64_CVT", "c119773201639db0e144e1a88858b8e7");
            cvtMD5Dict.Add("RBOOT_CVT", "4f47c60627388f4cd4b2790b4885c9fb");
            cvtMD5Dict.Add("ROMA_CVT", "1a8ad4d1afb18dba780fe26abd6fcd87");
            cvtMD5Dict.Add("SNL10COM_CVT", "4333aaddb404780978d546961ff97b01");
            cvtMD5Dict.Add("SPELL64_CVT", "66d08a0603b9fad4f2f9aaa7162a82ec");
            cvtMD5Dict.Add("TG64_CVT", "8a009581e8ad1929963edf257b753b12");
            cvtMD5Dict.Add("TGESF64_CVT", "472b7dff778c44d73c160c8e8bd5b6f7");
            cvtMD5Dict.Add("TGG1F64_CVT", "da9d506fd586976dc8bea41588a3f895");
            cvtMD5Dict.Add("TGG2F64_CVT", "1e710083dab4c15f64cc6259b077ed2d");
            cvtMD5Dict.Add("TGG3F64_CVT", "94ec38829b83232d6582ac32a11e74a6");
            cvtMD5Dict.Add("TGPCF64_CVT", "8eadca507e1d249582ce54a4ab84673d");
            cvtMD5Dict.Add("TGSSF64_CVT", "1f898e2b53c3a6b2a07a42063d7971a1");
            cvtMD5Dict.Add("TGWWF64_CVT", "aed320a2a2c31c12e0f8eb4e251fc047");
            cvtMD5Dict.Add("TXMGR64_CVT", "ac929f4df22ab959c7a8228d15bfc004");
            cvtMD5Dict.Add("UNIV_CVT", "cb853b51ffde80b1cd8b192dca5ce4d8");

            // Um leichter auf die Resources Dateien zugreifen zu können, werden diese in ein Dictionary gelesen.
            // Speicher spielt dabei keine Rolle!
            Dictionary<string, byte[]> cvtFileDataDict = new Dictionary<string, byte[]>();
            {
                ResourceManager resMgr = new ResourceManager("ZAK256.CBMDiskImageTools.Test.UnitTest.Resources.cbmfiles.com_CVT", System.Reflection.Assembly.GetExecutingAssembly());
                foreach (KeyValuePair<string, string> kp in cvtMD5Dict)
                {
                    byte[] fileData = (byte[])resMgr.GetObject(kp.Key);
                    if (fileData == null)
                    {
                        throw new Exception("The test file is incorrect!");
                    }
                    cvtFileDataDict.Add(kp.Key, fileData);
                }
            }
            
            // Prüfen, ob die Ausgangs CVT Dateien korrekt sind. Dazu werden die MD5 Prüfsummen der Dateien
            // errechnet und mit den vorgebnen (hart codierten) MD5 Prüfsummen verglichen.
            foreach (KeyValuePair<string, string> kp in cvtMD5Dict)
            {
                byte[] imageData = cvtFileDataDict[kp.Key];
                Assert.AreEqual(kp.Value.ToUpper(), Core.GetMD5Hash(imageData), "The test file is incorrect!");
            }

            // Ein Dictionary mit den MD5 Prüfsummen für die 4 D64 Images aus dem Archiv GEOS64.ZIP.
            Dictionary<string, string> geos64ZIPD64Images = new Dictionary<string, string>();
            geos64ZIPD64Images.Add("APPS64_D64", "8EB414AB37B23A1D1D348D456896A1B0");
            geos64ZIPD64Images.Add("GEOS64_D64", "F004B907634A30C21D4DF39E362C0789");
            geos64ZIPD64Images.Add("SPELL64_D64", "05425BE1824E99534CC30E60EDFF49C7");
            geos64ZIPD64Images.Add("WRUTIL64_D64", "3B52C7A91F794C0E5599AF804A515878");

            // Um leichter auf die Resources Dateien zugreifen zu können, werden diese in ein Dictionary gelesen.
            // Speicher spielt dabei keine Rolle!
            Dictionary<string, byte[]> geos64ZIPD64ImagesData = new Dictionary<string, byte[]>();
            {
                ResourceManager resMgr = new ResourceManager("ZAK256.CBMDiskImageTools.Test.UnitTest.Resources.cbmfiles.com_GEOS64_ZIP", System.Reflection.Assembly.GetExecutingAssembly());
                foreach (KeyValuePair<string, string> kp in geos64ZIPD64Images)
                {
                    byte[] fileData = (byte[])resMgr.GetObject(kp.Key);
                    if (fileData == null)
                    {
                        throw new Exception("The test file is incorrect!");
                    }
                    geos64ZIPD64ImagesData.Add(kp.Key, fileData);
                }
            }
            // Prüfen, ob die Ausgangs Dateien korrekt sind. Dazu werden die MD5 Prüfsummen der Dateien
            // errechnet und mit den vorgebnen (hart codierten) MD5 Prüfsummen verglichen.
            foreach (KeyValuePair<string, string> kp in geos64ZIPD64Images)
            {
                byte[] imageData = geos64ZIPD64ImagesData[kp.Key];
                Assert.AreEqual(kp.Value.ToUpper(), Core.GetMD5Hash(imageData), "The test file is incorrect!");
            }

            //
            Dictionary<string, string> assignFileToCVT = new Dictionary<string, string>();
            assignFileToCVT.Add("GW64_CVT", "GEOWRITE");
            assignFileToCVT.Add("GPT64_CVT", "GEOPAINT");
            assignFileToCVT.Add("PHMGR64_CVT", "photo manager");
            assignFileToCVT.Add("CALC64_CVT", "calculator");
            assignFileToCVT.Add("NOTE64_CVT", "note pad");
            assignFileToCVT.Add("CALIF_CVT", "California");
            assignFileToCVT.Add("CORY_CVT", "Cory");
            assignFileToCVT.Add("DWIN_CVT", "Dwinelle");
            assignFileToCVT.Add("ROMA_CVT", "Roma");
            assignFileToCVT.Add("UNIV_CVT", "University");
            assignFileToCVT.Add("COMMFONT_CVT", "Commodore");
            assignFileToCVT.Add("JOYSTICK_CVT", "JOYSTICK");
            assignFileToCVT.Add("MPS803_CVT", "MPS-803");
            assignFileToCVT.Add("PRMGR64_CVT", "preference mgr");
            assignFileToCVT.Add("PDMGR64_CVT", "pad color mgr");
            assignFileToCVT.Add("ALARM64_CVT", "alarm clock");
            assignFileToCVT.Add("PNTDRVRS_CVT", "PAINT DRIVERS");
            assignFileToCVT.Add("RBOOT_CVT", "RBOOT");
            assignFileToCVT.Add("SNL10COM_CVT", "Star NL-10(com)");
            assignFileToCVT.Add("ASC_CVT", "ASCII Only");
            assignFileToCVT.Add("COMM1351_CVT", "COMM 1351");
            assignFileToCVT.Add("COM1351A_CVT", "COMM 1351(a)");
            assignFileToCVT.Add("SPELL64_CVT", "GEOSPELL");
            assignFileToCVT.Add("DICT_CVT", "GeoDictionary");
            assignFileToCVT.Add("TG64_CVT", "TEXT GRABBER");
            assignFileToCVT.Add("GEOLASER_CVT", "GEOLASER");
            assignFileToCVT.Add("GM64_CVT", "GEOMERGE");
            assignFileToCVT.Add("TXMGR64_CVT", "text manager");
            assignFileToCVT.Add("TGESF64_CVT", "EasyScript Form");
            assignFileToCVT.Add("TGPCF64_CVT", "PaperClip Form");
            assignFileToCVT.Add("TGSSF64_CVT", "SpeedScript Form");
            assignFileToCVT.Add("TGWWF64_CVT", "WordWriter Form");
            assignFileToCVT.Add("TGG1F64_CVT", "Generic I Form");
            assignFileToCVT.Add("TGG2F64_CVT", "Generic II Form");
            assignFileToCVT.Add("TGG3F64_CVT", "Generic III Form");
            assignFileToCVT.Add("LWROMA_CVT", "LW_Roma");
            assignFileToCVT.Add("LWCAL_CVT", "LW_Cal");
            assignFileToCVT.Add("LWGREEK_CVT", "LW_Greek");
            assignFileToCVT.Add("LWBARR_CVT", "LW_Barrows");

            Dictionary<string, int> assignFileIndexToCVT = new Dictionary<string, int>();
            assignFileIndexToCVT.Add("GW64_CVT", 2); // "GEOWRITE"
            assignFileIndexToCVT.Add("GPT64_CVT", 3); // "GEOPAINT"
            assignFileIndexToCVT.Add("PHMGR64_CVT", 4); // "photo manager"
            assignFileIndexToCVT.Add("CALC64_CVT", 5); // "calculator"
            assignFileIndexToCVT.Add("NOTE64_CVT", 6); // "note pad"
            assignFileIndexToCVT.Add("CALIF_CVT", 7); // "California"
            assignFileIndexToCVT.Add("CORY_CVT", 8); // "Cory"
            assignFileIndexToCVT.Add("DWIN_CVT", 9); // "Dwinelle"
            assignFileIndexToCVT.Add("ROMA_CVT", 10); // "Roma"
            assignFileIndexToCVT.Add("UNIV_CVT", 11); // "University"
            assignFileIndexToCVT.Add("COMMFONT_CVT", 12); // "Commodore"
            assignFileIndexToCVT.Add("JOYSTICK_CVT", 5); // "JOYSTICK"
            assignFileIndexToCVT.Add("MPS803_CVT", 6); // "MPS-803"
            assignFileIndexToCVT.Add("PRMGR64_CVT", 7); // "preference mgr"
            assignFileIndexToCVT.Add("PDMGR64_CVT", 8); // "pad color mgr"
            assignFileIndexToCVT.Add("ALARM64_CVT", 9); // "alarm clock"
            assignFileIndexToCVT.Add("PNTDRVRS_CVT", 10); // "PAINT DRIVERS"
            assignFileIndexToCVT.Add("RBOOT_CVT", 11); // "RBOOT"
            assignFileIndexToCVT.Add("SNL10COM_CVT", 12); // "Star NL-10(com)"
            assignFileIndexToCVT.Add("ASC_CVT", 13); // "ASCII Only"
            assignFileIndexToCVT.Add("COMM1351_CVT", 14); // "COMM 1351"
            assignFileIndexToCVT.Add("COM1351A_CVT", 15); // "COMM 1351(a)"
            assignFileIndexToCVT.Add("SPELL64_CVT", 2); // "GEOSPELL"
            assignFileIndexToCVT.Add("DICT_CVT", 3); // "GeoDictionary"
            assignFileIndexToCVT.Add("TG64_CVT", 2); // "TEXT GRABBER"
            assignFileIndexToCVT.Add("GEOLASER_CVT", 3); // "GEOLASER"
            assignFileIndexToCVT.Add("GM64_CVT", 4); // "GEOMERGE"
            assignFileIndexToCVT.Add("TXMGR64_CVT", 5); // "text manager"
            assignFileIndexToCVT.Add("TGESF64_CVT", 6); // "EasyScript Form"
            assignFileIndexToCVT.Add("TGPCF64_CVT", 7); // "PaperClip Form"
            assignFileIndexToCVT.Add("TGSSF64_CVT", 8); // "SpeedScript Form"
            assignFileIndexToCVT.Add("TGWWF64_CVT", 9); // "WordWriter Form"
            assignFileIndexToCVT.Add("TGG1F64_CVT", 10); // "Generic I Form"
            assignFileIndexToCVT.Add("TGG2F64_CVT", 11); // "Generic II Form"
            assignFileIndexToCVT.Add("TGG3F64_CVT", 12); // "Generic III Form"
            assignFileIndexToCVT.Add("LWROMA_CVT", 13); // "LW_Roma"
            assignFileIndexToCVT.Add("LWCAL_CVT", 14); // "LW_Cal"
            assignFileIndexToCVT.Add("LWGREEK_CVT", 15); // "LW_Greek"
            assignFileIndexToCVT.Add("LWBARR_CVT", 16); // "LW_Barrows"

            Dictionary<string, string> assignDiskToCVT = new Dictionary<string, string>();
            assignDiskToCVT.Add("GW64_CVT", "APPS64_D64");
            assignDiskToCVT.Add("GPT64_CVT", "APPS64_D64");
            assignDiskToCVT.Add("PHMGR64_CVT", "APPS64_D64");
            assignDiskToCVT.Add("CALC64_CVT", "APPS64_D64");
            assignDiskToCVT.Add("NOTE64_CVT", "APPS64_D64");
            assignDiskToCVT.Add("CALIF_CVT", "APPS64_D64");
            assignDiskToCVT.Add("CORY_CVT", "APPS64_D64");
            assignDiskToCVT.Add("DWIN_CVT", "APPS64_D64");
            assignDiskToCVT.Add("ROMA_CVT", "APPS64_D64");
            assignDiskToCVT.Add("UNIV_CVT", "APPS64_D64");
            assignDiskToCVT.Add("COMMFONT_CVT", "APPS64_D64");
            assignDiskToCVT.Add("JOYSTICK_CVT", "GEOS64_D64");
            assignDiskToCVT.Add("MPS803_CVT", "GEOS64_D64");
            assignDiskToCVT.Add("PRMGR64_CVT", "GEOS64_D64");
            assignDiskToCVT.Add("PDMGR64_CVT", "GEOS64_D64");
            assignDiskToCVT.Add("ALARM64_CVT", "GEOS64_D64");
            assignDiskToCVT.Add("PNTDRVRS_CVT", "GEOS64_D64");
            assignDiskToCVT.Add("RBOOT_CVT", "GEOS64_D64");
            assignDiskToCVT.Add("SNL10COM_CVT", "GEOS64_D64");
            assignDiskToCVT.Add("ASC_CVT", "GEOS64_D64");
            assignDiskToCVT.Add("COMM1351_CVT", "GEOS64_D64");
            assignDiskToCVT.Add("COM1351A_CVT", "GEOS64_D64");
            assignDiskToCVT.Add("SPELL64_CVT", "SPELL64_D64");
            assignDiskToCVT.Add("DICT_CVT", "SPELL64_D64");
            assignDiskToCVT.Add("TG64_CVT", "WRUTIL64_D64");
            assignDiskToCVT.Add("GEOLASER_CVT", "WRUTIL64_D64");
            assignDiskToCVT.Add("GM64_CVT", "WRUTIL64_D64");
            assignDiskToCVT.Add("TXMGR64_CVT", "WRUTIL64_D64");
            assignDiskToCVT.Add("TGESF64_CVT", "WRUTIL64_D64");
            assignDiskToCVT.Add("TGPCF64_CVT", "WRUTIL64_D64");
            assignDiskToCVT.Add("TGSSF64_CVT", "WRUTIL64_D64");
            assignDiskToCVT.Add("TGWWF64_CVT", "WRUTIL64_D64");
            assignDiskToCVT.Add("TGG1F64_CVT", "WRUTIL64_D64");
            assignDiskToCVT.Add("TGG2F64_CVT", "WRUTIL64_D64");
            assignDiskToCVT.Add("TGG3F64_CVT", "WRUTIL64_D64");
            assignDiskToCVT.Add("LWROMA_CVT", "WRUTIL64_D64");
            assignDiskToCVT.Add("LWCAL_CVT", "WRUTIL64_D64");
            assignDiskToCVT.Add("LWGREEK_CVT", "WRUTIL64_D64");
            assignDiskToCVT.Add("LWBARR_CVT", "WRUTIL64_D64");



            foreach (KeyValuePair<string, string> kp in cvtMD5Dict)
            {
                ArrayList dirEntryList = new ArrayList();
                byte[] imageData = geos64ZIPD64ImagesData[assignDiskToCVT[kp.Key]];
                int imageDataType = 0; // sorry hard coding 0 = D64
                byte[] bamBlock = DOSDisk.ReadBAMBlock(imageData, imageDataType);
                dirEntryList = DOSDisk.GetDirEntryList(bamBlock, imageData, imageDataType);

                byte[] dirEntry = (byte[])dirEntryList[assignFileIndexToCVT[kp.Key] - 1];
                string filename = Core.ConvertPETSCII2ASCII(DOSDisk.GetFilename(dirEntry));
                //string expectedMd5 = ;

                string md5 = DOSDisk.GetMD5ByFile(dirEntry, imageData, imageDataType);
                Assert.AreEqual(expectedMd5, md5, "The MD5 checksum is incorrect.");
                Console.WriteLine("Filename = {0}", filename);
                Console.WriteLine("MD5 {0} = {1}", expectedMd5, md5); // see at test explorer --> "output"
            }
        }
    }
}
