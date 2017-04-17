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
        public void TestFiesOf4Images()
        {
            Dictionary<int, TestFile> dictGeos64Zip = new Dictionary<int, TestFile>();
            dictGeos64Zip.Add(1, new TestFile("APPS64.D64", Resources.cbmfiles_com_GEOS64_ZIP.APPS64_D64, "8EB414AB37B23A1D1D348D456896A1B0"));
            dictGeos64Zip.Add(2, new TestFile("GEOS64.D64", Resources.cbmfiles_com_GEOS64_ZIP.GEOS64_D64, "F004B907634A30C21D4DF39E362C0789"));
            dictGeos64Zip.Add(3, new TestFile("SPELL64.D64", Resources.cbmfiles_com_GEOS64_ZIP.SPELL64_D64, "05425BE1824E99534CC30E60EDFF49C7"));
            dictGeos64Zip.Add(4, new TestFile("WRUTIL64.D64", Resources.cbmfiles_com_GEOS64_ZIP.WRUTIL64_D64, "3B52C7A91F794C0E5599AF804A515878"));
            // Test 1
            foreach (var testFile in dictGeos64Zip.Values)
            {
                string newMD5 = Core.GetMD5Hash(testFile.data);
                Assert.AreEqual(testFile.md5, newMD5);
            }

            Dictionary<int, TestFile> dictGeos64_SC083 = new Dictionary<int, TestFile>();

        }
        public class TestFile
        {
            public string name;
            public byte[] data;
            public string md5;
            public TestFile(string nameVal, byte[] dataVal, string md5Val)
            {
                name = nameVal;
                data = dataVal;
                md5 = md5Val;
            }
        }
    }
}
