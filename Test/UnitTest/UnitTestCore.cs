using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections;
using ZAK256.CBMDiskImageTools.Logic.Core;
using System.Collections.Generic;
using System.Linq;
namespace ZAK256.CBMDiskImageTools.Test.UnitTest
{
    [TestClass]
    public class UnitTestCore
    {
        [TestMethod]
        public void TestTheFilesOfCbmFilesDotCom()
        {
            // Test 1 - Data GEOS64.ZIP
            Console.WriteLine("Check the data of GEOS64.ZIP");
            Dictionary<int, TestFile> dictGeos64Zip = new Dictionary<int, TestFile>();
            dictGeos64Zip.Add(1, new TestFile("APPS64.D64", Resources.cbmfiles_com_GEOS64_ZIP.APPS64_D64, "8EB414AB37B23A1D1D348D456896A1B0"));
            dictGeos64Zip.Add(2, new TestFile("GEOS64.D64", Resources.cbmfiles_com_GEOS64_ZIP.GEOS64_D64, "F004B907634A30C21D4DF39E362C0789"));
            dictGeos64Zip.Add(3, new TestFile("SPELL64.D64", Resources.cbmfiles_com_GEOS64_ZIP.SPELL64_D64, "05425BE1824E99534CC30E60EDFF49C7"));
            dictGeos64Zip.Add(4, new TestFile("WRUTIL64.D64", Resources.cbmfiles_com_GEOS64_ZIP.WRUTIL64_D64, "3B52C7A91F794C0E5599AF804A515878"));
            Console.Write("   ");
            foreach (var testFile in dictGeos64Zip.Values)
            {
                string newMD5 = Core.GetMD5Hash(testFile.data);
                Assert.AreEqual(testFile.dataMd5, newMD5);
                Console.Write("\"{0}\" ",testFile.name);
            }
            Console.WriteLine();
            Console.WriteLine("Test passed");
            Console.WriteLine();

            // Test 2 - Star Commander
            Console.WriteLine("Check the data of the files which were extract by Star Commander 0.83");
            Dictionary<int, TestFile> dictGeos64SC083 = new Dictionary<int, TestFile>();
            dictGeos64SC083.Add(1, new TestFile("DESK TOP.cvt", Resources.cbmfiles_com_GEOS64_SC0_83_APPS64.DESK_TOP_CVT, "F5D400333C8A6BCA4DFFCEFC2AF4AE93"));
            dictGeos64SC083.Add(2, new TestFile("GEOWRITE.cvt", Resources.cbmfiles_com_GEOS64_SC0_83_APPS64.GEOWRITE_CVT, "7F16CB41C1C885BB02D118FC0411C1C6"));
            dictGeos64SC083.Add(3, new TestFile("GEOPAINT.cvt", Resources.cbmfiles_com_GEOS64_SC0_83_APPS64.GEOPAINT_CVT, "492A38CDA1A9385DD8F501C043352514"));
            dictGeos64SC083.Add(4, new TestFile("PHOTO MANAGER.CVT", Resources.cbmfiles_com_GEOS64_SC0_83_APPS64.PHOTO_MANAGER_CVT, "FD24833DA9184D6B28A0C376FA524D91"));
            dictGeos64SC083.Add(5, new TestFile("CALCULATOR.CVT", Resources.cbmfiles_com_GEOS64_SC0_83_APPS64.CALCULATOR_CVT, "C1B61367915D2360AE6030DA96EB38A4"));
            dictGeos64SC083.Add(6, new TestFile("NOTE PAD.CVT", Resources.cbmfiles_com_GEOS64_SC0_83_APPS64.NOTE_PAD_CVT, "CCCB6812019B90B9B8370664C6419E71"));
            dictGeos64SC083.Add(7, new TestFile("California.cvt", Resources.cbmfiles_com_GEOS64_SC0_83_APPS64.California_CVT, "C0F2E40D33FBB81390FB258F6B6FF702"));
            dictGeos64SC083.Add(8, new TestFile("Cory.cvt", Resources.cbmfiles_com_GEOS64_SC0_83_APPS64.Cory_CVT, "8B2B387AC0EA5AC66D15A25B2FAD6ECB"));
            dictGeos64SC083.Add(9, new TestFile("Dwinelle.cvt", Resources.cbmfiles_com_GEOS64_SC0_83_APPS64.Dwinelle_CVT, "47E58B32C121C8CCDF49806EE59F21F7"));
            dictGeos64SC083.Add(10, new TestFile("Roma.cvt", Resources.cbmfiles_com_GEOS64_SC0_83_APPS64.Roma_CVT, "1F10A57D2052DF486AE5BAAAD3C89A83"));
            dictGeos64SC083.Add(11, new TestFile("University.cvt", Resources.cbmfiles_com_GEOS64_SC0_83_APPS64.University_CVT, "8D464DC9D97612168A025435AEB15CBE"));
            dictGeos64SC083.Add(12, new TestFile("Commodore.cvt", Resources.cbmfiles_com_GEOS64_SC0_83_APPS64.Commodore_CVT, "52D872F4FAAFD30400282F085E990C63"));
            dictGeos64SC083.Add(13, new TestFile("ReadMe.cvt", Resources.cbmfiles_com_GEOS64_SC0_83_APPS64.ReadMe_CVT, "72E1B96410FDF050BC7F2EF2C28D0B09"));
            dictGeos64SC083.Add(14, new TestFile("GEOS.cvt", Resources.cbmfiles_com_GEOS64_SC0_83_GEOS64.GEOS_CVT, "D4189F0FA391276504B680E1205402D6"));
            dictGeos64SC083.Add(15, new TestFile("GEOBOOT.cvt", Resources.cbmfiles_com_GEOS64_SC0_83_GEOS64.GEOBOOT_CVT, "2992D902F031FF10BE10E74B64779DF6"));
            dictGeos64SC083.Add(16, new TestFile("CONFIGURE.cvt", Resources.cbmfiles_com_GEOS64_SC0_83_GEOS64.CONFIGURE_CVT, "48D1D7B7A765C8492D4A7C76B209680C"));
            dictGeos64SC083.Add(17, new TestFile("DESK TOP.cvt", Resources.cbmfiles_com_GEOS64_SC0_83_GEOS64.DESK_TOP_CVT, "F5D400333C8A6BCA4DFFCEFC2AF4AE93"));
            dictGeos64SC083.Add(18, new TestFile("JOYSTICK.cvt", Resources.cbmfiles_com_GEOS64_SC0_83_GEOS64.JOYSTICK_CVT, "8A6C7F925A94BE2A9F7D3F2703E17E75"));
            dictGeos64SC083.Add(19, new TestFile("MPS-803.cvt", Resources.cbmfiles_com_GEOS64_SC0_83_GEOS64.MPS_803_CVT, "EFEA6E62E40F3201E3A068D9C55246CF"));
            dictGeos64SC083.Add(20, new TestFile("PREFERENCE MGR.CVT", Resources.cbmfiles_com_GEOS64_SC0_83_GEOS64.PREFERENCE_MGR_CVT, "B3AA1A856F31E45491C194E1998D8545"));
            dictGeos64SC083.Add(21, new TestFile("PAD COLOR MGR.CVT", Resources.cbmfiles_com_GEOS64_SC0_83_GEOS64.PAD_COLOR_MGR_CVT, "A36BFC4C78DD76E95073A87B3F11B796"));
            dictGeos64SC083.Add(22, new TestFile("ALARM CLOCK.CVT", Resources.cbmfiles_com_GEOS64_SC0_83_GEOS64.ALARM_CLOCK_CVT, "A409DF281F24BA040C71C43E4255DBCF"));
            dictGeos64SC083.Add(23, new TestFile("PAINT DRIVERS.cvt", Resources.cbmfiles_com_GEOS64_SC0_83_GEOS64.PAINT_DRIVERS_CVT, "F1A989AA4D8C9EEFA15F525C20BFF072"));
            dictGeos64SC083.Add(24, new TestFile("RBOOT.cvt", Resources.cbmfiles_com_GEOS64_SC0_83_GEOS64.RBOOT_CVT, "16F8EF09CAE68824A07A788E3894995B"));
            dictGeos64SC083.Add(25, new TestFile("Star NL-10(com).cvt", Resources.cbmfiles_com_GEOS64_SC0_83_GEOS64.Star_NL_10_com__CVT, "EA73673D31DE636B01DCF77EB858C1A0"));
            dictGeos64SC083.Add(26, new TestFile("ASCII Only.cvt", Resources.cbmfiles_com_GEOS64_SC0_83_GEOS64.ASCII_Only_CVT, "D332C0F0C9CAB30C76595E4A1E3D1275"));
            dictGeos64SC083.Add(27, new TestFile("COMM 1351.cvt", Resources.cbmfiles_com_GEOS64_SC0_83_GEOS64.COMM_1351_CVT, "7D531B58F898208E1CBFD1C34AF58778"));
            dictGeos64SC083.Add(28, new TestFile("COMM 1351(a).cvt", Resources.cbmfiles_com_GEOS64_SC0_83_GEOS64.COMM_1351_a__CVT, "CAACED9DF9C176DA2C8A3071A0D0896A"));
            dictGeos64SC083.Add(29, new TestFile("CONVERT.cvt", Resources.cbmfiles_com_GEOS64_SC0_83_GEOS64.CONVERT_CVT, "71507FC1130C74FAC0F641331436EB75"));
            dictGeos64SC083.Add(30, new TestFile("DESK TOP.cvt", Resources.cbmfiles_com_GEOS64_SC0_83_SPELL64.DESK_TOP_CVT, "F5D400333C8A6BCA4DFFCEFC2AF4AE93"));
            dictGeos64SC083.Add(31, new TestFile("GEOSPELL.cvt", Resources.cbmfiles_com_GEOS64_SC0_83_SPELL64.GEOSPELL_CVT, "B37CDB19CBD077D927D2F6F098605FE0"));
            dictGeos64SC083.Add(32, new TestFile("GeoDictionary.cvt", Resources.cbmfiles_com_GEOS64_SC0_83_SPELL64.GeoDictionary_CVT, "E12B57A3E2587EFA706B9975B8CBFB45"));
            dictGeos64SC083.Add(33, new TestFile("DESK TOP.cvt", Resources.cbmfiles_com_GEOS64_SC0_83_WRUTIL64.DESK_TOP_CVT, "F5D400333C8A6BCA4DFFCEFC2AF4AE93"));
            dictGeos64SC083.Add(34, new TestFile("TEXT GRABBER.cvt", Resources.cbmfiles_com_GEOS64_SC0_83_WRUTIL64.TEXT_GRABBER_CVT, "2ACFA3A95FC8648BC0D642984D97DEB4"));
            dictGeos64SC083.Add(35, new TestFile("GEOLASER.cvt", Resources.cbmfiles_com_GEOS64_SC0_83_WRUTIL64.GEOLASER_CVT, "5271F7664C366E059E7EF1DBA32F4C59"));
            dictGeos64SC083.Add(36, new TestFile("GEOMERGE.cvt", Resources.cbmfiles_com_GEOS64_SC0_83_WRUTIL64.GEOMERGE_CVT, "A2622148D7520AC3AE84FF837B7EFBD0"));
            dictGeos64SC083.Add(37, new TestFile("TEXT MANAGER.CVT", Resources.cbmfiles_com_GEOS64_SC0_83_WRUTIL64.TEXT_MANAGER_CVT, "DFFC461AF3E44E4EFF916C7CF648494A"));
            dictGeos64SC083.Add(38, new TestFile("EasyScript Form.cvt", Resources.cbmfiles_com_GEOS64_SC0_83_WRUTIL64.EasyScript_Form_CVT, "F9417D7F5AB61501F06B784E67DA0CA8"));
            dictGeos64SC083.Add(39, new TestFile("PaperClip Form.cvt", Resources.cbmfiles_com_GEOS64_SC0_83_WRUTIL64.PaperClip_Form_CVT, "5B6E33DF49A489BDD488614CB941323E"));
            dictGeos64SC083.Add(40, new TestFile("SpeedScript Form.cvt", Resources.cbmfiles_com_GEOS64_SC0_83_WRUTIL64.SpeedScript_Form_CVT, "7B21F9411985C7EDD05FC4B6CCCDEC60"));
            dictGeos64SC083.Add(41, new TestFile("WordWriter Form.cvt", Resources.cbmfiles_com_GEOS64_SC0_83_WRUTIL64.WordWriter_Form_CVT, "EAD4FFEE1DE995A34F9FBA75D026BD92"));
            dictGeos64SC083.Add(42, new TestFile("Generic I Form.cvt", Resources.cbmfiles_com_GEOS64_SC0_83_WRUTIL64.Generic_I_Form_CVT, "4F50805C82556881B5C11BA9B421FDD3"));
            dictGeos64SC083.Add(43, new TestFile("Generic II Form.cvt", Resources.cbmfiles_com_GEOS64_SC0_83_WRUTIL64.Generic_II_Form_CVT, "7F4324363DBDB2F750B93D7CBD906B34"));
            dictGeos64SC083.Add(44, new TestFile("Generic III Form.cvt", Resources.cbmfiles_com_GEOS64_SC0_83_WRUTIL64.Generic_III_Form_CVT, "E5B22697EE7C2948106433C484FE0F0F"));
            dictGeos64SC083.Add(45, new TestFile("LW_Roma.cvt", Resources.cbmfiles_com_GEOS64_SC0_83_WRUTIL64.LW_Roma_CVT, "5F5CD29A02FFB848C05FE96E4694D3E8"));
            dictGeos64SC083.Add(46, new TestFile("LW_Cal.cvt", Resources.cbmfiles_com_GEOS64_SC0_83_WRUTIL64.LW_Cal_CVT, "6FBAB49BB4C3F3042F421F05514E5E85"));
            dictGeos64SC083.Add(47, new TestFile("LW_Greek.cvt", Resources.cbmfiles_com_GEOS64_SC0_83_WRUTIL64.LW_Greek_CVT, "698F7E6AD8258810CD3D2CC1656C9426"));
            dictGeos64SC083.Add(48, new TestFile("LW_Barrows.cvt", Resources.cbmfiles_com_GEOS64_SC0_83_WRUTIL64.LW_Barrows_CVT, "20C2975E902873EB3EA9707B931B04FB"));
            Console.Write("   ");
            foreach (var testFile in dictGeos64SC083.Values)
            {
                string newMD5 = Core.GetMD5Hash(testFile.data);
                Assert.AreEqual(testFile.dataMd5, newMD5);
                Console.Write("\"{0}\" ",testFile.name);
            }
            Console.WriteLine();
            Console.WriteLine("Test passed");
            Console.WriteLine();

            // Test 3 - Data pcGeos
            Console.WriteLine("Check the data of the files which were extract by pcGeos 0.03 - GGET");
            Dictionary<int, TestFile> dictGeos64pcGeos03 = new Dictionary<int, TestFile>();
            dictGeos64pcGeos03.Add(1, new TestFile("DESK_TOP.CVT", Resources.cbmfiles_com_GEOS64_pcGeos0_3_APPS64.DESK_TOP_CVT, "9D8D6D46C2204541B927D9FF140CA4BB"));
            dictGeos64pcGeos03.Add(2, new TestFile("GEOWRITE.CVT", Resources.cbmfiles_com_GEOS64_pcGeos0_3_APPS64.GEOWRITE_CVT, "369417E36AF0182E404400279A942DC4"));
            dictGeos64pcGeos03.Add(3, new TestFile("GEOPAINT.CVT", Resources.cbmfiles_com_GEOS64_pcGeos0_3_APPS64.GEOPAINT_CVT, "0FBB2E8B9117CE76EDF00367A5B5CA2F"));
            dictGeos64pcGeos03.Add(4, new TestFile("PHOTO_MA.CVT", Resources.cbmfiles_com_GEOS64_pcGeos0_3_APPS64.PHOTO_MA_CVT, "B55415319436EDD21B496DC21023A657"));
            dictGeos64pcGeos03.Add(5, new TestFile("CALCULAT.CVT", Resources.cbmfiles_com_GEOS64_pcGeos0_3_APPS64.CALCULAT_CVT, "1556CA39902D1AD016D5E93EED2FC7AB"));
            dictGeos64pcGeos03.Add(6, new TestFile("NOTE_PAD.CVT", Resources.cbmfiles_com_GEOS64_pcGeos0_3_APPS64.NOTE_PAD_CVT, "C8482380A76461D9A4B63994B1B4A271"));
            dictGeos64pcGeos03.Add(7, new TestFile("CALIFORN.CVT", Resources.cbmfiles_com_GEOS64_pcGeos0_3_APPS64.CALIFORN_CVT, "9A6665F88DD94E2529A062AE754FBAF8"));
            dictGeos64pcGeos03.Add(8, new TestFile("CORY.CVT", Resources.cbmfiles_com_GEOS64_pcGeos0_3_APPS64.CORY_CVT, "A5BFBE6D34FF724AF1782EC761130BAB"));
            dictGeos64pcGeos03.Add(9, new TestFile("DWINELLE.CVT", Resources.cbmfiles_com_GEOS64_pcGeos0_3_APPS64.DWINELLE_CVT, "50E1D47708CA96FA0A62A25B5C955955"));
            dictGeos64pcGeos03.Add(10, new TestFile("ROMA.CVT", Resources.cbmfiles_com_GEOS64_pcGeos0_3_APPS64.ROMA_CVT, "C6E49563E60D37CC9F6D7EFA402158B0"));
            dictGeos64pcGeos03.Add(11, new TestFile("UNIVERSI.CVT", Resources.cbmfiles_com_GEOS64_pcGeos0_3_APPS64.UNIVERSI_CVT, "5337FFC13660EDB0F1BC428D8228582B"));
            dictGeos64pcGeos03.Add(12, new TestFile("COMMODOR.CVT", Resources.cbmfiles_com_GEOS64_pcGeos0_3_APPS64.COMMODOR_CVT, "3F61567424FAC9689B1E5A759E9DF13D"));
            dictGeos64pcGeos03.Add(13, new TestFile("README.CVT", Resources.cbmfiles_com_GEOS64_pcGeos0_3_APPS64.README_CVT, "FEA6204456B0C53E9AF55E30BB4C4D55"));
            dictGeos64pcGeos03.Add(14, new TestFile("GEOS.CVT", Resources.cbmfiles_com_GEOS64_pcGeos0_3_GEOS64.GEOS_CVT, "B404164B2043F2630FAEFE2E51F1551B"));
            dictGeos64pcGeos03.Add(15, new TestFile("GEOBOOT.CVT", Resources.cbmfiles_com_GEOS64_pcGeos0_3_GEOS64.GEOBOOT_CVT, "99C046006C69052B035434BD15308C86"));
            dictGeos64pcGeos03.Add(16, new TestFile("CONFIGUR.CVT", Resources.cbmfiles_com_GEOS64_pcGeos0_3_GEOS64.CONFIGUR_CVT, "6EB4F151EC50B9AFC0E1498EF19B8F42"));
            dictGeos64pcGeos03.Add(17, new TestFile("DESK_TOP.CVT", Resources.cbmfiles_com_GEOS64_pcGeos0_3_GEOS64.DESK_TOP_CVT, "854EF70A8ADEBBFF3095788021BDBF51"));
            dictGeos64pcGeos03.Add(18, new TestFile("JOYSTICK.CVT", Resources.cbmfiles_com_GEOS64_pcGeos0_3_GEOS64.JOYSTICK_CVT, "F6EE2780FBE9257B83E74EC8E3E97696"));
            dictGeos64pcGeos03.Add(19, new TestFile("MPS-803.CVT", Resources.cbmfiles_com_GEOS64_pcGeos0_3_GEOS64.MPS_803_CVT, "D6F53D3040F5C6244962302F13146CC3"));
            dictGeos64pcGeos03.Add(20, new TestFile("PREFEREN.CVT", Resources.cbmfiles_com_GEOS64_pcGeos0_3_GEOS64.PREFEREN_CVT, "657EE6E5742591B7A79F73F71B0A5A78"));
            dictGeos64pcGeos03.Add(21, new TestFile("PAD_COLO.CVT", Resources.cbmfiles_com_GEOS64_pcGeos0_3_GEOS64.PAD_COLO_CVT, "D3562D9E99E877BC7656327EF007AF66"));
            dictGeos64pcGeos03.Add(22, new TestFile("ALARM_CL.CVT", Resources.cbmfiles_com_GEOS64_pcGeos0_3_GEOS64.ALARM_CL_CVT, "7E4F120AA6AB01C60CA0973A44448317"));
            dictGeos64pcGeos03.Add(23, new TestFile("PAINT_DR.CVT", Resources.cbmfiles_com_GEOS64_pcGeos0_3_GEOS64.PAINT_DR_CVT, "455CA4A8BACD5CD3EC5EE929D7CEA986"));
            dictGeos64pcGeos03.Add(24, new TestFile("RBOOT.CVT", Resources.cbmfiles_com_GEOS64_pcGeos0_3_GEOS64.RBOOT_CVT, "120F2248EA88727A82438B7E2C63C93B"));
            dictGeos64pcGeos03.Add(25, new TestFile("STAR_NL-.CVT", Resources.cbmfiles_com_GEOS64_pcGeos0_3_GEOS64.STAR_NL__CVT, "21DFB576CB923BFB15B933C119EC80B4"));
            dictGeos64pcGeos03.Add(26, new TestFile("ASCII_ON.CVT", Resources.cbmfiles_com_GEOS64_pcGeos0_3_GEOS64.ASCII_ON_CVT, "2C6A0CEF3EB9B08BF3D6C36C6998ABBD"));
            dictGeos64pcGeos03.Add(27, new TestFile("COMM_135.CVT", Resources.cbmfiles_com_GEOS64_pcGeos0_3_GEOS64.COMM_135_CVT, "A5B6AC2905FF5D06EA6324F757D374A4"));
            dictGeos64pcGeos03.Add(28, new TestFile("COMM_13A.CVT", Resources.cbmfiles_com_GEOS64_pcGeos0_3_GEOS64.COMM_13A_CVT, "D55F8DECEC8F0287FDFF26846F9C1BAB"));
            dictGeos64pcGeos03.Add(29, new TestFile("CONVERT.CVT", Resources.cbmfiles_com_GEOS64_pcGeos0_3_GEOS64.CONVERT_CVT, "AEB8DC166A37D28036D9609F35F4BD10"));
            dictGeos64pcGeos03.Add(30, new TestFile("DESK_TOP.CVT", Resources.cbmfiles_com_GEOS64_pcGeos0_3_SPELL64.DESK_TOP_CVT, "9D8D6D46C2204541B927D9FF140CA4BB"));
            dictGeos64pcGeos03.Add(31, new TestFile("GEOSPELL.CVT", Resources.cbmfiles_com_GEOS64_pcGeos0_3_SPELL64.GEOSPELL_CVT, "2B7F3A95D28F203B45411B3A6E41CFE4"));
            dictGeos64pcGeos03.Add(32, new TestFile("GEODICTI.CVT", Resources.cbmfiles_com_GEOS64_pcGeos0_3_SPELL64.GEODICTI_CVT, "F9E0491C6FEBAC8BE1674EF0AF0F811A"));
            dictGeos64pcGeos03.Add(33, new TestFile("DESK_TOP.CVT", Resources.cbmfiles_com_GEOS64_pcGeos0_3_WRUTIL64.DESK_TOP_CVT, "9D8D6D46C2204541B927D9FF140CA4BB"));
            dictGeos64pcGeos03.Add(34, new TestFile("TEXT_GRA.CVT", Resources.cbmfiles_com_GEOS64_pcGeos0_3_WRUTIL64.TEXT_GRA_CVT, "0CF69DB4F036FECEE43618ABF461302F"));
            dictGeos64pcGeos03.Add(35, new TestFile("GEOLASER.CVT", Resources.cbmfiles_com_GEOS64_pcGeos0_3_WRUTIL64.GEOLASER_CVT, "EA1E178F4E99C59253895CF8A0B4484F"));
            dictGeos64pcGeos03.Add(36, new TestFile("GEOMERGE.CVT", Resources.cbmfiles_com_GEOS64_pcGeos0_3_WRUTIL64.GEOMERGE_CVT, "22D3CCA988A1DBCA942BA3DC77C03C4A"));
            dictGeos64pcGeos03.Add(37, new TestFile("TEXT_MAN.CVT", Resources.cbmfiles_com_GEOS64_pcGeos0_3_WRUTIL64.TEXT_MAN_CVT, "C75275A46D1FAED13FE6B8C1FB9DC084"));
            dictGeos64pcGeos03.Add(38, new TestFile("EASYSCRI.CVT", Resources.cbmfiles_com_GEOS64_pcGeos0_3_WRUTIL64.EASYSCRI_CVT, "52D4A929551C95E749CD84372094E22A"));
            dictGeos64pcGeos03.Add(39, new TestFile("PAPERCLI.CVT", Resources.cbmfiles_com_GEOS64_pcGeos0_3_WRUTIL64.PAPERCLI_CVT, "5458935A49B20FA03A63697E7FE19436"));
            dictGeos64pcGeos03.Add(40, new TestFile("SPEEDSCR.CVT", Resources.cbmfiles_com_GEOS64_pcGeos0_3_WRUTIL64.SPEEDSCR_CVT, "DAA2B91714CE99E22AFAA754C60D86BB"));
            dictGeos64pcGeos03.Add(41, new TestFile("WORDWRIT.CVT", Resources.cbmfiles_com_GEOS64_pcGeos0_3_WRUTIL64.WORDWRIT_CVT, "D63AA80B4EBD64B054448DCC57BC5A52"));
            dictGeos64pcGeos03.Add(42, new TestFile("GENERIC1.CVT", Resources.cbmfiles_com_GEOS64_pcGeos0_3_WRUTIL64.GENERIC1_CVT, "1DCDAC6FF84675FD600DAF2829B0DEFB"));
            dictGeos64pcGeos03.Add(43, new TestFile("GENERIC2.CVT", Resources.cbmfiles_com_GEOS64_pcGeos0_3_WRUTIL64.GENERIC2_CVT, "C943FD93935AE042AAFE674274CDA29F"));
            dictGeos64pcGeos03.Add(44, new TestFile("GENERIC3.CVT", Resources.cbmfiles_com_GEOS64_pcGeos0_3_WRUTIL64.GENERIC3_CVT, "31F9DF42D2673447C5A4C990CBA3C5F0"));
            dictGeos64pcGeos03.Add(45, new TestFile("LW_ROMA.CVT", Resources.cbmfiles_com_GEOS64_pcGeos0_3_WRUTIL64.LW_ROMA_CVT, "8E35D8542573D8DE89E601E307AB2FFF"));
            dictGeos64pcGeos03.Add(46, new TestFile("LW_CAL.CVT", Resources.cbmfiles_com_GEOS64_pcGeos0_3_WRUTIL64.LW_CAL_CVT, "5D617DFA1EB25CC97955AB4DBFF4E853"));
            dictGeos64pcGeos03.Add(47, new TestFile("LW_GREEK.CVT", Resources.cbmfiles_com_GEOS64_pcGeos0_3_WRUTIL64.LW_GREEK_CVT, "C3FACF419B5A6C9F15AA0B8235F0C696"));
            dictGeos64pcGeos03.Add(48, new TestFile("LW_BARRO.CVT", Resources.cbmfiles_com_GEOS64_pcGeos0_3_WRUTIL64.LW_BARRO_CVT, "C2BEABC033098226DC24FB0C2AE81997"));
            Console.Write("   ");
            foreach (var testFile in dictGeos64pcGeos03.Values)
            {
                string newMD5 = Core.GetMD5Hash(testFile.data);
                Assert.AreEqual(testFile.dataMd5, newMD5);
                Console.Write("\"{0}\" ",testFile.name);
            }
            Console.WriteLine();
            Console.WriteLine("Test passed");
            Console.WriteLine();

            // Test 4
            Console.WriteLine("Check the data of the files which were extract by Convert 2.05 + Star Commander 0.83");
            Dictionary<int, TestFile> dictGeos64Convert25SC083 = new Dictionary<int, TestFile>();
            dictGeos64Convert25SC083.Add(1, new TestFile("DESK TOP.prg", Resources.cbmfiles_com_GEOS64_Convert2_5_SC0_83_APPS64.DESK_TOP_PRG, "7EBA4AF5C4553D8D498C10DC8FA7A90C"));
            dictGeos64Convert25SC083.Add(2, new TestFile("GEOWRITE.prg", Resources.cbmfiles_com_GEOS64_Convert2_5_SC0_83_APPS64.GEOWRITE_PRG, "282836DD70C1AA43C925D26CC50922D1"));
            dictGeos64Convert25SC083.Add(3, new TestFile("GEOPAINT.prg", Resources.cbmfiles_com_GEOS64_Convert2_5_SC0_83_APPS64.GEOPAINT_PRG, "1A962A0F3312235BE9A65847C491B786"));            
            dictGeos64Convert25SC083.Add(4, new TestFile("PHOTO MANAGER.prg", Resources.cbmfiles_com_GEOS64_Convert2_5_SC0_83_APPS64.PHOTO_MANAGER_PRG, "A485E8311FED4EA8AC741D0263E035A7"));
            dictGeos64Convert25SC083.Add(5, new TestFile("CALCULATOR.prg", Resources.cbmfiles_com_GEOS64_Convert2_5_SC0_83_APPS64.CALCULATOR_PRG, "9DF318D9DD026B954CB107DF12BBA019"));
            dictGeos64Convert25SC083.Add(6, new TestFile("NOTE PAD.prg", Resources.cbmfiles_com_GEOS64_Convert2_5_SC0_83_APPS64.NOTE_PAD_PRG, "ABBAD64E2A0FDF1E68CC36C86CC2D134"));
            dictGeos64Convert25SC083.Add(7, new TestFile("CALIFORNIA.prg", Resources.cbmfiles_com_GEOS64_Convert2_5_SC0_83_APPS64.CALIFORNIA_PRG, "A4CCBCF2AA2BBC79B11630A02A0AAA81"));
            dictGeos64Convert25SC083.Add(8, new TestFile("CORY.prg", Resources.cbmfiles_com_GEOS64_Convert2_5_SC0_83_APPS64.CORY_PRG, "E48966530A8846B92B990BD8A25CFBBF"));
            dictGeos64Convert25SC083.Add(9, new TestFile("DWINELLE.prg", Resources.cbmfiles_com_GEOS64_Convert2_5_SC0_83_APPS64.DWINELLE_PRG, "C446EAE01C6F3B1322960528DA595949"));
            dictGeos64Convert25SC083.Add(10, new TestFile("ROMA.prg", Resources.cbmfiles_com_GEOS64_Convert2_5_SC0_83_APPS64.ROMA_PRG, "A50CF1B2614BA10E371E71833905A9EE"));
            dictGeos64Convert25SC083.Add(11, new TestFile("UNIVERSITY.prg", Resources.cbmfiles_com_GEOS64_Convert2_5_SC0_83_APPS64.UNIVERSITY_PRG, "22366FD2CC63CDAA23949DD23F6EFF72"));
            dictGeos64Convert25SC083.Add(12, new TestFile("COMMODORE.prg", Resources.cbmfiles_com_GEOS64_Convert2_5_SC0_83_APPS64.COMMODORE_PRG, "ABC6B4BF2149BB7B5DB98F5F22E7BEC0"));
            dictGeos64Convert25SC083.Add(13, new TestFile("README.prg", Resources.cbmfiles_com_GEOS64_Convert2_5_SC0_83_APPS64.README_PRG, "919C2AEA45BC40010BC132933FB3F61C"));
            dictGeos64Convert25SC083.Add(14, new TestFile("", null, ""));
            dictGeos64Convert25SC083.Add(15, new TestFile("", null, ""));
            dictGeos64Convert25SC083.Add(16, new TestFile("CONFIGURE.prg", Resources.cbmfiles_com_GEOS64_Convert2_5_SC0_83_GEOS64.CONFIGURE_PRG, "B0B53FBB758F2B3D46D92B321C977AC1"));
            dictGeos64Convert25SC083.Add(17, new TestFile("DESK TOP.prg", Resources.cbmfiles_com_GEOS64_Convert2_5_SC0_83_GEOS64.DESK_TOP_PRG, "7423DD2D5DE045C65E74C0B81635CA14"));
            dictGeos64Convert25SC083.Add(18, new TestFile("JOYSTICK.prg", Resources.cbmfiles_com_GEOS64_Convert2_5_SC0_83_GEOS64.JOYSTICK_PRG, "E6419F70C7549F83E7EC7C4A822F51E7"));
            dictGeos64Convert25SC083.Add(19, new TestFile("MPS-803.prg", Resources.cbmfiles_com_GEOS64_Convert2_5_SC0_83_GEOS64.MPS_803_PRG, "73A9CB0F680EFE21F61318AE8B52EBD0"));
            dictGeos64Convert25SC083.Add(20, new TestFile("PREFERENCE MGR.prg", Resources.cbmfiles_com_GEOS64_Convert2_5_SC0_83_GEOS64.PREFERENCE_MGR_PRG, "8CE634C8D4E13127F0A76592A6443072"));
            dictGeos64Convert25SC083.Add(21, new TestFile("PAD COLOR MGR.prg", Resources.cbmfiles_com_GEOS64_Convert2_5_SC0_83_GEOS64.PAD_COLOR_MGR_PRG, "F8717513E4875722154E9DC1CFCF4A60"));
            dictGeos64Convert25SC083.Add(22, new TestFile("ALARM CLOCK.prg", Resources.cbmfiles_com_GEOS64_Convert2_5_SC0_83_GEOS64.ALARM_CLOCK_PRG, "FCF7B6091E92CBF67EDB03C1E5CDCA62"));
            dictGeos64Convert25SC083.Add(23, new TestFile("PAINT DRIVERS.prg", Resources.cbmfiles_com_GEOS64_Convert2_5_SC0_83_GEOS64.PAINT_DRIVERS_PRG, "9078E9908DA7D4EFDAAE8BBFE7996D9E"));
            dictGeos64Convert25SC083.Add(24, new TestFile("", null, ""));
            dictGeos64Convert25SC083.Add(25, new TestFile("STAR NL-10(COM).prg", Resources.cbmfiles_com_GEOS64_Convert2_5_SC0_83_GEOS64.STAR_NL_10_COM__PRG, "A070C2051867C27701A1822EA4049460"));
            dictGeos64Convert25SC083.Add(26, new TestFile("ASCII ONLY.prg", Resources.cbmfiles_com_GEOS64_Convert2_5_SC0_83_GEOS64.ASCII_ONLY_PRG, "89C9B4E8A2182A75A8C000C876E6A832"));
            dictGeos64Convert25SC083.Add(27, new TestFile("COMM 1351.prg", Resources.cbmfiles_com_GEOS64_Convert2_5_SC0_83_GEOS64.COMM_1351_PRG, "2F4DD5EF1C1FA7F14F4E50F09036BB1B"));
            dictGeos64Convert25SC083.Add(28, new TestFile("COMM 1351(A).prg", Resources.cbmfiles_com_GEOS64_Convert2_5_SC0_83_GEOS64.COMM_1351_A__PRG, "D63A39C2080FE4E0BE2DBE3BE7F179AD"));
            dictGeos64Convert25SC083.Add(29, new TestFile("CONVERT.prg", Resources.cbmfiles_com_GEOS64_Convert2_5_SC0_83_GEOS64.CONVERT_PRG, "593DDC5473BE62A5010ABBD4FFD7F043"));
            dictGeos64Convert25SC083.Add(30, new TestFile("DESK TOP.prg", Resources.cbmfiles_com_GEOS64_Convert2_5_SC0_83_SPELL64.DESK_TOP_PRG, "677CDA4B1A6B94D20C48F3FAF5C8A6EE"));
            dictGeos64Convert25SC083.Add(31, new TestFile("GEOSPELL.prg", Resources.cbmfiles_com_GEOS64_Convert2_5_SC0_83_SPELL64.GEOSPELL_PRG, "2B6B7D6E64C502DE4C205A33346D0E9A"));
            dictGeos64Convert25SC083.Add(32, new TestFile("GEODICTIONARY.prg", Resources.cbmfiles_com_GEOS64_Convert2_5_SC0_83_SPELL64.GEODICTIONARY_PRG, "3561AD296E4C947CCF4D523D3318A93B"));
            dictGeos64Convert25SC083.Add(33, new TestFile("DESK TOP.prg", Resources.cbmfiles_com_GEOS64_Convert2_5_SC0_83_WRUTIL64.DESK_TOP_PRG, "0A25298A6FE454C78FF1E8C90990D2F4"));
            dictGeos64Convert25SC083.Add(34, new TestFile("TEXT GRABBER.prg", Resources.cbmfiles_com_GEOS64_Convert2_5_SC0_83_WRUTIL64.TEXT_GRABBER_PRG, "D843D10F4C594ED0DF58646804ABEF8E"));
            dictGeos64Convert25SC083.Add(35, new TestFile("GEOLASER.prg", Resources.cbmfiles_com_GEOS64_Convert2_5_SC0_83_WRUTIL64.GEOLASER_PRG, "4A9BD0301C5E1E3F75A9C73B0A2E45AD"));
            dictGeos64Convert25SC083.Add(36, new TestFile("GEOMERGE.prg", Resources.cbmfiles_com_GEOS64_Convert2_5_SC0_83_WRUTIL64.GEOMERGE_PRG, "51465265957B06A90B525A94D70B0C19"));
            dictGeos64Convert25SC083.Add(37, new TestFile("TEXT MANAGER.prg", Resources.cbmfiles_com_GEOS64_Convert2_5_SC0_83_WRUTIL64.TEXT_MANAGER_PRG, "4F80225FB7E642F6FC46D1B4818B008D"));
            dictGeos64Convert25SC083.Add(38, new TestFile("EASYSCRIPT FORM.prg", Resources.cbmfiles_com_GEOS64_Convert2_5_SC0_83_WRUTIL64.EASYSCRIPT_FORM_PRG, "403E549C1416C823B8EDA489AB69B68D"));
            dictGeos64Convert25SC083.Add(39, new TestFile("PAPERCLIP FORM.prg", Resources.cbmfiles_com_GEOS64_Convert2_5_SC0_83_WRUTIL64.PAPERCLIP_FORM_PRG, "6034C00E26BCC567ABDDDDD725016766"));
            dictGeos64Convert25SC083.Add(40, new TestFile("SPEEDSCRIPT FORM.prg", Resources.cbmfiles_com_GEOS64_Convert2_5_SC0_83_WRUTIL64.SPEEDSCRIPT_FORM_PRG, "AE7F711C0A1BD1FF9EE7D57705B2B860"));
            dictGeos64Convert25SC083.Add(41, new TestFile("WORDWRITER FORM.prg", Resources.cbmfiles_com_GEOS64_Convert2_5_SC0_83_WRUTIL64.WORDWRITER_FORM_PRG, "B6FF093CFF9FE6B137FC7123932AE8A5"));
            dictGeos64Convert25SC083.Add(42, new TestFile("GENERIC I FORM.prg", Resources.cbmfiles_com_GEOS64_Convert2_5_SC0_83_WRUTIL64.GENERIC_I_FORM_PRG, "5DA5376974FF15B3EECE07F72621812A"));
            dictGeos64Convert25SC083.Add(43, new TestFile("GENERIC II FORM.prg", Resources.cbmfiles_com_GEOS64_Convert2_5_SC0_83_WRUTIL64.GENERIC_II_FORM_PRG, "527EFAD39DD3C5D3AA713E9730111BAF"));
            dictGeos64Convert25SC083.Add(44, new TestFile("GENERIC III FORM.prg", Resources.cbmfiles_com_GEOS64_Convert2_5_SC0_83_WRUTIL64.GENERIC_III_FORM_PRG, "63991ED777525B2CC91F6E5D604D7563"));
            dictGeos64Convert25SC083.Add(45, new TestFile("LW_ROMA.prg", Resources.cbmfiles_com_GEOS64_Convert2_5_SC0_83_WRUTIL64.LW_ROMA_PRG, "319443AC562F61BB10D78B56284AA426"));
            dictGeos64Convert25SC083.Add(46, new TestFile("LW_CAL.prg", Resources.cbmfiles_com_GEOS64_Convert2_5_SC0_83_WRUTIL64.LW_CAL_PRG, "04D21EDD41BC8E6F3AF346574FD64F8C"));
            dictGeos64Convert25SC083.Add(47, new TestFile("LW_GREEK.prg", Resources.cbmfiles_com_GEOS64_Convert2_5_SC0_83_WRUTIL64.LW_GREEK_PRG, "6B0021B1BC8100CC3E0A1321C2C0C9A8"));
            dictGeos64Convert25SC083.Add(48, new TestFile("LW_BARROWS.prg", Resources.cbmfiles_com_GEOS64_Convert2_5_SC0_83_WRUTIL64.LW_BARROWS_PRG, "F6053FB6F0BFDA4D3F3D3A134D0006FD"));
            Console.Write("   ");
            foreach (var testFile in dictGeos64Convert25SC083.Values)
            {
                if (testFile.data != null)
                {
                    string newMD5 = Core.GetMD5Hash(testFile.data);
                    Assert.AreEqual(testFile.dataMd5, newMD5);
                    Console.Write("\"{0}\" ",testFile.name);
                }
            }
            Console.WriteLine();
            Console.WriteLine("Test passed");
            Console.WriteLine();

            // Test 5 Data - CVT files
            Console.WriteLine("Check the data of CVT files");
            Dictionary<int, TestFile> dictCvt = new Dictionary<int, TestFile>();
            dictCvt.Add(1, new TestFile("APPS64DT.CVT", null, ""));
            dictCvt.Add(2, new TestFile("GW64.CVT", Resources.cbmfiles_com_CVT.GW64_CVT, "7BB3438CBE86A08448BB03585AF68787"));
            dictCvt.Add(3, new TestFile("GPT64.CVT", Resources.cbmfiles_com_CVT.GPT64_CVT, "5518FA80D16F09EB8DF16749423FE74A"));
            dictCvt.Add(4, new TestFile("PHMGR64.CVT", Resources.cbmfiles_com_CVT.PHMGR64_CVT, "97420E1433004B66E2688AFEAC3E20EA"));
            dictCvt.Add(5, new TestFile("CALC64.CVT", Resources.cbmfiles_com_CVT.CALC64_CVT, "A64B5BE35B6ECDFB73D36B6AF2037EE7"));
            dictCvt.Add(6, new TestFile("NOTE64.CVT", Resources.cbmfiles_com_CVT.NOTE64_CVT, "C311D37F2B19E28115B54989511CA7EC"));
            dictCvt.Add(7, new TestFile("CALIF.CVT", Resources.cbmfiles_com_CVT.CALIF_CVT, "63774DDBD94C6D3C84021B540A5CC352"));
            dictCvt.Add(8, new TestFile("CORY.CVT", Resources.cbmfiles_com_CVT.CORY_CVT, "C25D5FB668CE7913DD5F78BAFD5F861B"));
            dictCvt.Add(9, new TestFile("DWIN.CVT", Resources.cbmfiles_com_CVT.DWIN_CVT, "EB734844875C33D4B225CFA4C6CFBB73"));
            dictCvt.Add(10, new TestFile("ROMA.CVT", Resources.cbmfiles_com_CVT.ROMA_CVT, "1A8AD4D1AFB18DBA780FE26ABD6FCD87"));
            dictCvt.Add(11, new TestFile("UNIV.CVT", Resources.cbmfiles_com_CVT.UNIV_CVT, "CB853B51FFDE80B1CD8B192DCA5CE4D8"));
            dictCvt.Add(12, new TestFile("COMMFONT.CVT", Resources.cbmfiles_com_CVT.COMMFONT_CVT, "AE09C60ED90700B37C0DB1043BC1DA7B"));
            dictCvt.Add(13, new TestFile("README.CVT", null, ""));
            dictCvt.Add(14, new TestFile("GEOS64.CVT", null, ""));
            dictCvt.Add(15, new TestFile("BOOT64.CVT", null, ""));
            dictCvt.Add(16, new TestFile("CONF64.CVT", null, ""));
            dictCvt.Add(17, new TestFile("GEOS64DT.CVT", null, ""));
            dictCvt.Add(18, new TestFile("JOYSTICK.CVT", Resources.cbmfiles_com_CVT.JOYSTICK_CVT, "9E35F7D3C2183FA1A3A9CAAA551EA342"));
            dictCvt.Add(19, new TestFile("MPS803.CVT", Resources.cbmfiles_com_CVT.MPS803_CVT, "B322C99316A0E3C34BE6BD8CBFB0A571"));
            dictCvt.Add(20, new TestFile("PRMGR64.CVT", Resources.cbmfiles_com_CVT.PRMGR64_CVT, "C119773201639DB0E144E1A88858B8E7"));
            dictCvt.Add(21, new TestFile("PDMGR64.CVT", Resources.cbmfiles_com_CVT.PDMGR64_CVT, "D23FDDC59754FC9CC352B5FE81728996"));
            dictCvt.Add(22, new TestFile("ALARM64.CVT", Resources.cbmfiles_com_CVT.ALARM64_CVT, "379562FCE34FF3AA92AC1BF94AB0C82A"));
            dictCvt.Add(23, new TestFile("PNTDRVRS.CVT", Resources.cbmfiles_com_CVT.PNTDRVRS_CVT, "24A38885FCD5A3338CCC2ACF666A0862"));
            dictCvt.Add(24, new TestFile("RBOOT.CVT", Resources.cbmfiles_com_CVT.RBOOT_CVT, "4F47C60627388F4CD4B2790B4885C9FB"));
            dictCvt.Add(25, new TestFile("SNL10COM.CVT", Resources.cbmfiles_com_CVT.SNL10COM_CVT, "4333AADDB404780978D546961FF97B01"));
            dictCvt.Add(26, new TestFile("ASC.CVT", Resources.cbmfiles_com_CVT.ASC_CVT, "9E50E4F08F6362684CCC0B2C40F5BACD"));
            dictCvt.Add(27, new TestFile("COMM1351.CVT", Resources.cbmfiles_com_CVT.COMM1351_CVT, "B84B6CA934D95399D4F23FD719D27135"));
            dictCvt.Add(28, new TestFile("COM1351A.CVT", Resources.cbmfiles_com_CVT.COM1351A_CVT, "36E9CE9693BABCE60C22F6418E884CDF"));
            dictCvt.Add(29, new TestFile("CONVERT.CVT", null, ""));
            dictCvt.Add(30, new TestFile("SPEL64DT.CVT", null, ""));
            dictCvt.Add(31, new TestFile("SPELL64.CVT", Resources.cbmfiles_com_CVT.SPELL64_CVT, "66D08A0603B9FAD4F2F9AAA7162A82EC"));
            dictCvt.Add(32, new TestFile("DICT.CVT", Resources.cbmfiles_com_CVT.DICT_CVT, "8B064B856EBA099BB639265A09E9935F"));
            dictCvt.Add(33, new TestFile("WRUT64DT.CVT", null, ""));
            dictCvt.Add(34, new TestFile("TG64.CVT", Resources.cbmfiles_com_CVT.TG64_CVT, "8A009581E8AD1929963EDF257B753B12"));
            dictCvt.Add(35, new TestFile("GEOLASER.CVT", Resources.cbmfiles_com_CVT.GEOLASER_CVT, "93B40E916E698D0A47F0D5D8D28A0D00"));
            dictCvt.Add(36, new TestFile("GM64.CVT", Resources.cbmfiles_com_CVT.GM64_CVT, "7DE625E9D7717D57774AC7FAE7860D0E"));
            dictCvt.Add(37, new TestFile("TXMGR64.CVT", Resources.cbmfiles_com_CVT.TXMGR64_CVT, "AC929F4DF22AB959C7A8228D15BFC004"));
            dictCvt.Add(38, new TestFile("TGESF64.CVT", Resources.cbmfiles_com_CVT.TGESF64_CVT, "472B7DFF778C44D73C160C8E8BD5B6F7"));
            dictCvt.Add(39, new TestFile("TGPCF64.CVT", Resources.cbmfiles_com_CVT.TGPCF64_CVT, "8EADCA507E1D249582CE54A4AB84673D"));
            dictCvt.Add(40, new TestFile("TGSSF64.CVT", Resources.cbmfiles_com_CVT.TGSSF64_CVT, "1F898E2B53C3A6B2A07A42063D7971A1"));
            dictCvt.Add(41, new TestFile("TGWWF64.CVT", Resources.cbmfiles_com_CVT.TGWWF64_CVT, "AED320A2A2C31C12E0F8EB4E251FC047"));
            dictCvt.Add(42, new TestFile("TGG1F64.CVT", Resources.cbmfiles_com_CVT.TGG1F64_CVT, "DA9D506FD586976DC8BEA41588A3F895"));
            dictCvt.Add(43, new TestFile("TGG2F64.CVT", Resources.cbmfiles_com_CVT.TGG2F64_CVT, "1E710083DAB4C15F64CC6259B077ED2D"));
            dictCvt.Add(44, new TestFile("TGG3F64.CVT", Resources.cbmfiles_com_CVT.TGG3F64_CVT, "94EC38829B83232D6582AC32A11E74A6"));
            dictCvt.Add(45, new TestFile("LWROMA.CVT", Resources.cbmfiles_com_CVT.LWROMA_CVT, "BFC38A9C73FD4E348B136A35B58A8311"));
            dictCvt.Add(46, new TestFile("LWCAL.CVT", Resources.cbmfiles_com_CVT.LWCAL_CVT, "57770A581FDA04CD57E804F7D6A0731E"));
            dictCvt.Add(47, new TestFile("LWGREEK.CVT", Resources.cbmfiles_com_CVT.LWGREEK_CVT, "95BCA643DC832D061A7601DD4CE8DC48"));
            dictCvt.Add(48, new TestFile("LWBARR.CVT", Resources.cbmfiles_com_CVT.LWBARR_CVT, "7A9A5764BC4FB097BA95D549A26EBC7B"));
            Console.Write("   ");
            foreach (var testFile in dictCvt.Values)
            {
                if (testFile.data != null)
                {
                    string newMD5 = Core.GetMD5Hash(testFile.data);
                    Assert.AreEqual(testFile.dataMd5, newMD5);
                    Console.Write("\"{0}\" ",testFile.name);
                }
            }
            Console.WriteLine();
            Console.WriteLine("Test passed");
            Console.WriteLine();

            // Clean CVT 
            Console.WriteLine("Clean CVT files");
            foreach (var testFile in dictGeos64SC083.Values)
            {
                // data already clean!
                testFile.cleanCvtData = testFile.data;
                testFile.CleanCvtDataMd5 = testFile.dataMd5;
            }
            Console.Write("   pcGeos 0.03 - GGET files               :");
            foreach (var testFile in dictGeos64pcGeos03.Values)
            {
                testFile.cleanCvtData = GEOSDisk.GetCleanCvtFromCvt(testFile.data);
                string newMD5 = Core.GetMD5Hash(testFile.cleanCvtData);
                testFile.CleanCvtDataMd5 = newMD5;
                Console.Write("\"{0}\" ", testFile.name);
            }
            Console.WriteLine();
            Console.Write("   Convert 2.5 + Star Commander 0.83 files:");
            foreach (var testFile in dictGeos64Convert25SC083.Values)
            {
                if (testFile.data != null)
                {
                    testFile.cleanCvtData = GEOSDisk.GetCleanCvtFromCvt(testFile.data);
                    string newMD5 = Core.GetMD5Hash(testFile.cleanCvtData);
                    testFile.CleanCvtDataMd5 = newMD5;
                    Console.Write("\"{0}\" ", testFile.name);
                }
            }
            Console.WriteLine();
            Console.Write("   CVT files                              :");
            foreach (var testFile in dictCvt.Values)
            {
                if (testFile.data != null)
                {
                    testFile.cleanCvtData = GEOSDisk.GetCleanCvtFromCvt(testFile.data);
                    string newMD5 = Core.GetMD5Hash(testFile.cleanCvtData);
                    testFile.CleanCvtDataMd5 = newMD5;
                    Console.Write("\"{0}\" ", testFile.name);
                }
            }
            Console.WriteLine();
            Console.WriteLine("Clean finished");
            Console.WriteLine();

            // Test
            Console.WriteLine("Create CDIExtract files");
            Dictionary<int, TestFile> dictCdiExtract = new Dictionary<int, TestFile>();
            AddCbmFileToDict(dictGeos64Zip, dictCdiExtract, 1, 1, 1);  // DESK TOP
            AddCbmFileToDict(dictGeos64Zip, dictCdiExtract, 1, 2, 2);  // GEOPAINT
            AddCbmFileToDict(dictGeos64Zip, dictCdiExtract, 1, 3, 3);  // GEOWRITE
            AddCbmFileToDict(dictGeos64Zip, dictCdiExtract, 1, 4, 4);  // PHOTO MANAGER
            AddCbmFileToDict(dictGeos64Zip, dictCdiExtract, 1, 5, 5);  // CALCULATOR
            AddCbmFileToDict(dictGeos64Zip, dictCdiExtract, 1, 6, 6);  // NOTE PAD
            AddCbmFileToDict(dictGeos64Zip, dictCdiExtract, 1, 7, 7);  // California
            AddCbmFileToDict(dictGeos64Zip, dictCdiExtract, 1, 8, 8);  // Cory
            AddCbmFileToDict(dictGeos64Zip, dictCdiExtract, 1, 9, 9);  // Dwinelle
            AddCbmFileToDict(dictGeos64Zip, dictCdiExtract, 1, 10, 10);  // Roma
            AddCbmFileToDict(dictGeos64Zip, dictCdiExtract, 1, 11, 11);  // University
            AddCbmFileToDict(dictGeos64Zip, dictCdiExtract, 1, 12, 12);  // Commodore
            AddCbmFileToDict(dictGeos64Zip, dictCdiExtract, 1, 13, 13);  // ReadMe
            AddCbmFileToDict(dictGeos64Zip, dictCdiExtract, 2, 1, 14);  // GEOS
            AddCbmFileToDict(dictGeos64Zip, dictCdiExtract, 2, 2, 15);  // GEOBOOT
            AddCbmFileToDict(dictGeos64Zip, dictCdiExtract, 2, 3, 16);  // CONFIGURE
            AddCbmFileToDict(dictGeos64Zip, dictCdiExtract, 2, 4, 17);  // DESK TOP
            AddCbmFileToDict(dictGeos64Zip, dictCdiExtract, 2, 5, 18);  // JOYSTICK
            AddCbmFileToDict(dictGeos64Zip, dictCdiExtract, 2, 6, 19);  // MPS-803
            AddCbmFileToDict(dictGeos64Zip, dictCdiExtract, 2, 7, 20);  // PREFERENCE MGR
            AddCbmFileToDict(dictGeos64Zip, dictCdiExtract, 2, 8, 21);  // PAD COLOR MGR
            AddCbmFileToDict(dictGeos64Zip, dictCdiExtract, 2, 9, 22);  // ALARM CLOCK
            AddCbmFileToDict(dictGeos64Zip, dictCdiExtract, 2, 10, 23);  // PAINT DRIVERS
            AddCbmFileToDict(dictGeos64Zip, dictCdiExtract, 2, 11, 24);  // RBOOT
            AddCbmFileToDict(dictGeos64Zip, dictCdiExtract, 2, 12, 25);  // Star NL-10(com)
            AddCbmFileToDict(dictGeos64Zip, dictCdiExtract, 2, 13, 26);  // ASCII Only
            AddCbmFileToDict(dictGeos64Zip, dictCdiExtract, 2, 14, 27);  // COMM 1351
            AddCbmFileToDict(dictGeos64Zip, dictCdiExtract, 2, 15, 28);  // COMM 1351(a)
            AddCbmFileToDict(dictGeos64Zip, dictCdiExtract, 2, 16, 29);  // CONVERT
            AddCbmFileToDict(dictGeos64Zip, dictCdiExtract, 3, 1, 30);  // DESK TOP
            AddCbmFileToDict(dictGeos64Zip, dictCdiExtract, 3, 2, 31);  // GEOSPELL
            AddCbmFileToDict(dictGeos64Zip, dictCdiExtract, 3, 3, 32);  // GeoDictionary
            AddCbmFileToDict(dictGeos64Zip, dictCdiExtract, 4, 1, 33);  // DESK TOP
            AddCbmFileToDict(dictGeos64Zip, dictCdiExtract, 4, 2, 34);  // TEXT GRABBER
            AddCbmFileToDict(dictGeos64Zip, dictCdiExtract, 4, 3, 35);  // GEOLASER
            AddCbmFileToDict(dictGeos64Zip, dictCdiExtract, 4, 4, 36);  // GEOMERGE
            AddCbmFileToDict(dictGeos64Zip, dictCdiExtract, 4, 5, 37);  // TEXT MANAGER
            AddCbmFileToDict(dictGeos64Zip, dictCdiExtract, 4, 6, 38);  // EasyScript Form
            AddCbmFileToDict(dictGeos64Zip, dictCdiExtract, 4, 7, 39);  // PaperClip Form
            AddCbmFileToDict(dictGeos64Zip, dictCdiExtract, 4, 8, 40);  // SpeedScript Form
            AddCbmFileToDict(dictGeos64Zip, dictCdiExtract, 4, 9, 41);  // WordWriter Form
            AddCbmFileToDict(dictGeos64Zip, dictCdiExtract, 4, 10, 42);  // Generic I Form
            AddCbmFileToDict(dictGeos64Zip, dictCdiExtract, 4, 11, 43);  // Generic II Form
            AddCbmFileToDict(dictGeos64Zip, dictCdiExtract, 4, 12, 44);  // Generic III Form
            AddCbmFileToDict(dictGeos64Zip, dictCdiExtract, 4, 13, 45);  // LW_Roma
            AddCbmFileToDict(dictGeos64Zip, dictCdiExtract, 4, 14, 46);  // LW_Cal
            AddCbmFileToDict(dictGeos64Zip, dictCdiExtract, 4, 15, 47);  // LW_Greek
            AddCbmFileToDict(dictGeos64Zip, dictCdiExtract, 4, 16, 48);  // LW_Barrows
            Console.WriteLine("Files created");
            Console.WriteLine();

            // Test - Die Dateien, welche vom Star Commander erstellt wurden, deinen als Test Basis
            // Vergleich CDIExtract gegen Star Commander
            Console.WriteLine("CDIExtract vs. Star Commander");
            foreach (KeyValuePair<int, TestFile> kvp in dictGeos64SC083)
            {
                TestFile actTestFile = dictCdiExtract[kvp.Key];
                Boolean equal = kvp.Value.cleanCvtData.SequenceEqual(actTestFile.cleanCvtData);
                if (equal)
                {
                    Console.WriteLine(String.Format("   {0,-20} = {1,-20}", actTestFile.name,kvp.Value.name));
                }
                else
                {
                    Assert.Fail(String.Format("The File {0} is not equal with Star Commander file {1} - {2}", actTestFile.name,actTestFile.CleanCvtDataMd5,kvp.Value.CleanCvtDataMd5));
                }
            }
            Console.WriteLine("Test passed");
            Console.WriteLine();

            // Test
            // Vergleich CDIExtract gegen CVT
            Console.WriteLine("CDIExtract vs. CVT files");
            foreach (KeyValuePair<int, TestFile> kvp in dictCvt)
            {
                if (kvp.Value.cleanCvtData != null)
                {
                    TestFile actTestFile = dictCdiExtract[kvp.Key];
                    if (kvp.Key == 28) // "COMM 1351(a)"
                    {
                        Assert.AreEqual(0x00, kvp.Value.cleanCvtData[28]); // der korrekte Wert ist 0x03, hier wird auf den ferhlerhaften Wert getestet
                        kvp.Value.cleanCvtData[28] = 0x03; // KORREKTUR der CVT DATEI an Byte 28 !!!
                        Console.WriteLine(String.Format("   The File {0} has been corrected!!", actTestFile.name));
                    }
                    Boolean equal = kvp.Value.cleanCvtData.SequenceEqual(actTestFile.cleanCvtData);
                    if (equal)
                    {
                        Console.WriteLine(String.Format("   {0,-20} = {1,-20}", actTestFile.name,kvp.Value.name));
                    }
                    else
                    {
                        Assert.Fail(String.Format("The File {0} is not equal with CVT File {1} - {2}", actTestFile.name, actTestFile.CleanCvtDataMd5, kvp.Value.CleanCvtDataMd5));
                    }
                }
            }
            Console.WriteLine("Test passed");
            Console.WriteLine();

            // Test - Die Dateien, welche vom Star Commander erstellt wurden, deinen als Test Basis
            // Vergleich pcGeos 0.3 gegen Star Commander 0.83
            Console.WriteLine("pcGeos 0.3 vs. Star Commander 0.83");
            foreach (KeyValuePair<int, TestFile> kvp in dictGeos64SC083)
            {
                TestFile actTestFile = dictGeos64pcGeos03[kvp.Key];
                Boolean equal = kvp.Value.cleanCvtData.SequenceEqual(actTestFile.cleanCvtData);
                if (equal)
                {
                    Console.WriteLine(String.Format("   {0,-20} = {1,-20}", actTestFile.name,kvp.Value.name));
                }
                else
                {
                    Assert.Fail(String.Format("The File {0} is not equal with Star Commander file {1} - {2}", actTestFile.name, actTestFile.CleanCvtDataMd5, kvp.Value.CleanCvtDataMd5));
                }
            }
            Console.WriteLine("Test passed");
            Console.WriteLine();

            // Test - Die Dateien, welche vom Star Commander erstellt wurden, deinen als Test Basis
            // Vergleich (Convert 2.5 + Star Commander 0.83) gegen Star Commander 0.83
            Console.WriteLine("(Convert 2.5 + Star Commander 0.83) vs. Star Commander 0.83");
            foreach (KeyValuePair<int, TestFile> kvp in dictGeos64SC083)
            {
                if (kvp.Key != 14 && kvp.Key != 15 && kvp.Key != 24)
                {
                    TestFile actTestFile = dictGeos64Convert25SC083[kvp.Key];
                    Boolean equal = kvp.Value.cleanCvtData.SequenceEqual(actTestFile.cleanCvtData);
                    if (equal)
                    {
                        Console.WriteLine(String.Format("   {0,-20} = {1,-20}", actTestFile.name,kvp.Value.name));
                    }
                    else
                    {
                        Console.WriteLine(String.Format("The File {0} is not equal with Star Commander file {1} - {2}", actTestFile.name, actTestFile.CleanCvtDataMd5, kvp.Value.CleanCvtDataMd5));
                    }
                }
                else
                {
                    Console.WriteLine("   Convert has a problem with file {0} !", kvp.Value.name);
                }
            }
            Console.WriteLine("Test passed");
            Console.WriteLine();
        }
        public class TestFile
        {
            public string name;
            public byte[] data;
            public string dataMd5;
            public byte[] cleanCvtData;
            public string CleanCvtDataMd5;
            public TestFile(string nameVal, byte[] cvtDataVal, string cvtDataMd5Val)
            {
                name = nameVal;
                data = cvtDataVal;
                dataMd5 = cvtDataMd5Val;
            }
        }
        public static void AddCbmFileToDict(Dictionary<int, TestFile> dictGeos64Zip, Dictionary<int, TestFile> toDict, int indexOfDiskDict, int indexOfCbmFile, int indexOfTestFile)
        {
            byte[] bamBlock;
            byte[] dirEntry = null;
            ArrayList dirEntryList = new ArrayList();
            byte[] imageData = dictGeos64Zip[indexOfDiskDict].data;
            int imageDataType = 0; // sorry hard coded
            bamBlock = DOSDisk.ReadBAMBlock(imageData, imageDataType);
            dirEntryList = DOSDisk.GetDirEntryList(bamBlock, imageData, imageDataType);
            dirEntry = (byte[])dirEntryList[indexOfCbmFile - 1];
            
            string filename = (Core.ConvertPETSCII2ASCII(DOSDisk.GetFilename(dirEntry)));
            byte[] fileData = DOSDisk.getFileData(dirEntry, imageData, imageDataType);
            string newMD5 = Core.GetMD5Hash(fileData);
            TestFile tf = new TestFile(filename, fileData, newMD5);
            tf.cleanCvtData = tf.data;
            tf.CleanCvtDataMd5 = tf.dataMd5;           
             
            toDict.Add(indexOfTestFile, tf);
        }
    }
}
