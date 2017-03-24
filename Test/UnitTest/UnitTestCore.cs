using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections;
using ZAK256.CBMDiskImageTools.Logic.Core;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class UnitTestCore
    {
        [TestMethod]
        public void TestMethod1()
        {
            ArrayList dirEntryList = new ArrayList();
            byte[] imageData = Properties.Resources.GEOS64_D64;
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
    }
}
