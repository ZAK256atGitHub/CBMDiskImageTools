using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Collections;
using System.Security.Cryptography;

namespace ZAK256.CBMDiskImageTools.Logic.Core
{
    public static class Const
    {
        internal enum IMAGE_DATA_TYPE : int
        {
            D64 = 0,
            G64 = 1, // G64
            D71 = 3, // D71
            D81 = 4, // D81
            unknown = -1
        };
        internal static readonly string D64_IMAGE_FILE_EXTENSION = ".D64";
        internal static readonly string D71_IMAGE_FILE_EXTENSION = ".D71"; // D71
        internal static readonly string D81_IMAGE_FILE_EXTENSION = ".D81"; // D81
        internal static readonly string G64_IMAGE_FILE_EXTENSION = ".G64"; // G64
        internal static readonly string CVT_FILE_EXTENSION = ".CVT";
        internal const int MIN_TRACK = 1;
        internal const int MAX_TRACK_D64 = 35;
        internal const int MAX_TRACK_D71 = 70; // D71
        internal const int MAX_TRACK_D81 = 80; // D81
        internal const int MAX_TRACK_G64 = 80; // G64
        internal const int MIN_SECTOR = 0;
        internal const int MAX_SECTOR_D81 = 40; // D81
        internal const int BLOCK_LEN = 256;
        internal const int DATA_BLOCK_LEN = 254;
        internal const char LOCK_FLAG_SIGN = '<';
        internal static readonly string LOCK_FLAG_SIGN_EMPTY = " ";
        internal const char SPLAT_FILE_SIGN = '*';
        internal static readonly string SPLAT_FILE_SIGN_EMPTY = " ";
        internal const int DATA_BLOCK_TRACK_POS_IN_DIR_ENTRY = 1;
        internal const int DATA_BLOCK_SECTOR_POS_IN_DIR_ENTRY = 2;
        internal const int USED_BLOCKS_LOW_POS_IN_DIR_ENTRY = 28;
        internal const int USED_BLOCKS_HIGH_POS_IN_DIR_ENTRY = 29;
        internal const int NEXT_DIR_TRACK_POS_IN_DIR_ENTRY = 0;
        internal const int NEXT_DIR_SECTOR_POS_IN_DIR_ENTRY = 1;
        internal const int DIR_ENTRY_LEN = 30;
        internal const int FIRST_DIR_ENTRY_POS_IN_DIR_BLOCK = 2;
        internal const int NUM_OF_FILL_BYTES_BETWEEN_DIR_ENTRIES = 2;
        internal const int NUM_OF_DIR_ENTRIES_IN_DIR_BLOCK = 8;
        internal const int BAM_TRACK_D64 = 18;
        internal const int BAM_SECTOR_D64 = 0;
        internal const int BAM_TRACK_D71 = 18; // D71
        internal const int BAM_SECTOR_D71 = 0; // D71
        internal const int BAM_TRACK_D81 = 40; // D81
        internal const int BAM_SECTOR_D81 = 0; // D81
        internal const int BAM_TRACK_G64 = 18; // G64
        internal const int BAM_SECTOR_G64 = 0; // G64
        internal const int DOS_TYPE_POS_IN_BAM_BLOCK_D64 = 165;
        internal const int DOS_TYPE_POS_IN_BAM_BLOCK_D71 = 165; // D71
        internal const int DOS_TYPE_POS_IN_BAM_BLOCK_D81 = 25; // D81
        internal const int DOS_TYPE_POS_IN_BAM_BLOCK_G64 = 165; // G64
        internal const int DOS_TYPE_LEN = 2;
        internal const int DISK_ID_POS_IN_BAM_BLOCK_D64 = 162;
        internal const int DISK_ID_POS_IN_BAM_BLOCK_D71 = 162; // D71
        internal const int DISK_ID_POS_IN_BAM_BLOCK_D81 = 22; // D81
        internal const int DISK_ID_POS_IN_BAM_BLOCK_G64 = 162; // G64
        internal const int DISK_ID_LEN = 2;
        internal const int DISK_NAME_POS_IN_BAM_BLOCK_D64 = 144;
        internal const int DISK_NAME_POS_IN_BAM_BLOCK_D71 = 144; // D71
        internal const int DISK_NAME_POS_IN_BAM_BLOCK_D81 = 4; // D81
        internal const int DISK_NAME_POS_IN_BAM_BLOCK_G64 = 144; // G64
        internal const int DISK_NAME_LEN = 16;
        internal const int FILENAME_POS_IN_DIR_ENTRY = 3;
        internal const int FILENAME_LEN = 16;
        internal const byte TERMINATE_BYTE = 0xA0;
        internal const byte LOCK_FLAG_BIT_MASK = 0x40; // 0x40 = 0b01000000
        internal const byte SPLAT_FILE_BIT_MASK = 0x80; // 0x80 = 0b10000000
        internal const byte FILE_TYPE_BIT_MASK = 0x07; // 0x07 = 0b00000111
        internal static readonly string DOS_FILE_TYPE_EXT_UNKNOWN = "???";
        internal enum DOS_FILE_TYPE : int
        {
            Deleted = 0,
            Sequential = 1,
            Program = 2,
            User = 3,
            Relative = 4
        };
        internal readonly static string[] DOS_FILE_TYPE_EXT = {
            "DEL",  // 0 - DEL
            "SEQ",  // 1 - SEQ                                         
            "PRG",  // 2 - PRG
            "USR",  // 3 - USR
            "REL",  // 4 - REL
                    // Values 5-15 are illegal
        };
        internal readonly static int[] NUM_OF_SECTORS_PER_TRACK_D64 = {
            0,
            21,21,21,21,21,21,21,21,21,21,21,21,21,21,21,21,21, // Zone 1
	        19,19,19,19,19,19,19,                               // Zone 2
	        18,18,18,18,18,18,                                  // Zone 3
	        17,17,17,17,17                                      // Zone 4
        };
        // G64
        internal readonly static int[] NUM_OF_SECTORS_PER_TRACK_G64 = {
            0,
            21,21,21,21,21,21,21,21,21,21,21,21,21,21,21,21,21, // Zone 1
	        19,19,19,19,19,19,19,                               // Zone 2
	        18,18,18,18,18,18,                                  // Zone 3
	        17,17,17,17,17                                      // Zone 4
        };
        // D71
        internal readonly static int[] NUM_OF_SECTORS_PER_TRACK_D71 = {
            0,
            21,21,21,21,21,21,21,21,21,21,21,21,21,21,21,21,21, // Zone 1
	        19,19,19,19,19,19,19,                               // Zone 2
	        18,18,18,18,18,18,                                  // Zone 3
	        17,17,17,17,17,                                     // Zone 4
            21,21,21,21,21,21,21,21,21,21,21,21,21,21,21,21,21, // Zone 1 Seite 2
	        19,19,19,19,19,19,19,                               // Zone 2 Seite 2
	        18,18,18,18,18,18,                                  // Zone 3 Seite 2
	        17,17,17,17,17                                      // Zone 4 Seite 2
        };
        internal const int GEOS_INFO_BLOCK_TRACK_POS_IN_DIR_ENTRY = 19;
        internal const int GEOS_INFO_BLOCK_SECTOR_POS_IN_DIR_ENTRY = 20;
        internal const int GEOS_RECORD_BLOCK_TRACK_POS_IN_DIR_ENTRY = 1;
        internal const int GEOS_RECORD_BLOCK_SECTOR_POS_IN_DIR_ENTRY = 2;
        internal const int GEOS_FILE_STRUCTURE_POS_IN_DIR_ENTRY = 21;
        internal static readonly string GEOS_FILETYPE_NAME_UNKNOWN = "???";
        internal static readonly string GEOS_FILE_STRUCTURE_NAME_UNKNOWN = "???";
        internal const int GEOS_FILETYPE_POS_IN_DIR_ENTRY = 22;
        internal enum GEOS_FILE_STRUCTURE : int
        {
            SEQ = 0,
            VLIR = 1
        };
        internal readonly static string[] GEOS_FILE_STRUCTURE_NAMES = {
            "SEQ ",
            "VLIR"
        };
        internal enum GEOS_FILE_TYPE : int
        {
            NonGEOS = 0,
            BASIC = 1,
            Assembler = 2,
            Datafile = 3,
            SystemFile = 4,
            DeskAccessory = 5,
            Application = 6,
            ApplicationData = 7,
            FontFile = 8,
            PrinterDriver = 9,
            InputDriver = 10,
            DiskDriver = 11,
            SystemBootFile = 12,
            Temporary = 13,
            AutoExecuteFile = 14,
        };
        internal readonly static string[] GEOS_FILE_TYPE_NAMES = {
            "   ",  // $00 - Non-GEOS (normal C64 file)
            "BAS",  // $01 - BASIC
            "ASM",  // $02 - Assembler
            "DAT",  // $03 - Data file
            "SYS",  // $04 - System File
            "ACC",  // $05 - Desk Accessory
            "APP",  // $06 - Application
            "DOC",  // $07 - Application Data (user-created documents)
            "FNT",  // $08 - Font File
            "PRN",  // $09 - Printer Driver
            "INP",  // $0A - Input Driver
            "DSK",  // $0B - Disk Driver (or Disk Device)
            "BOT",  // $0C - System Boot File
            "TMP",  // $0D - Temporary
            "AUT"  // $0E - Auto-Execute File
                   // $0F-$FF - Undefined
        };
        //  internal static readonly string CVT_FILE_SIGNATURE_SEQ = "SEQ formatted GEOS file V1.0"; // erstmal immer PRG verwenden
        internal static readonly string CVT_FILE_SIGNATURE_PRG = "PRG formatted GEOS file V1.0";
        internal const int CVT_DIR_BLOCK_CLEAR_FROM_POS = 58;
    }
    public static class Core
    {
        public static string byteArrayToString(byte[] byteArray)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Address  00 01 02 03 04 05 06 07 08 09 0A 0B 0C 0D 0E 0F");

            int lineCount = 0;
            foreach (byte b in byteArray)
            {
                lineCount++;
                sb.Append(b.ToString("X2"));
                sb.Append(" ");
                if (lineCount >= 16)
                {
                    lineCount = 0;
                    sb.AppendLine();
                }
            }
            return sb.ToString();
        }
        #region Checksum
        public static string GetMD5Hash(byte[] data)
        {
            MD5 md5Hash = MD5.Create();
            byte[] hash = md5Hash.ComputeHash(data);
            return string.Concat(hash.Select(x => x.ToString("X2")));
        }
        #endregion

        #region [CBM System]
        public static string ConvertPETSCII2ASCII(byte[] petscii)
        {
            for (int i = 0; i < petscii.Length; i++)
            {
                if (petscii[i] == 255)
                {
                    petscii[i] = 126;
                }
                else if (petscii[i] >= 224)
                {
                    petscii[i] = (byte)(petscii[i] - 64);
                }
                else if (petscii[i] >= 192)
                {
                    petscii[i] = (byte)(petscii[i] - 96);
                }
            }
            return Encoding.Default.GetString(petscii);
        }
        #endregion
    }

    public static class GEOSDisk
    {
        #region[GEOS-DISK] File
        public static byte[] CleanCvtVlirRecordData(byte[] cvtVlirRecordData, byte[] cvtVLIRRecordBlock)
        {
            List<byte[]> blockCountLastBlockIndexList = new List<byte[]>();
            for (int i = 0; i < cvtVLIRRecordBlock.Length; i += 2)
            {
                byte[] blockCountLastBlockIndex = cvtVLIRRecordBlock.Skip(i).Take(2).ToArray();
                if ((blockCountLastBlockIndex[0] == 0x00) && (blockCountLastBlockIndex[1] == 0x00))
                {
                    break;
                }
                if (!((blockCountLastBlockIndex[0] == 0x00) && (blockCountLastBlockIndex[1] == 0xFF))) // ignore deleted records
                {
                    blockCountLastBlockIndexList.Add(blockCountLastBlockIndex);
                }
            }
            int totalOffset = 0;
            foreach (byte[] blockCountLastBlockIndex in blockCountLastBlockIndexList)
            {
                if (blockCountLastBlockIndex != blockCountLastBlockIndexList.Last())
                {
                    int cleanFrom = (blockCountLastBlockIndex[0] - 1) * Const.DATA_BLOCK_LEN + blockCountLastBlockIndex[1] + totalOffset -1; // Hmm ?!
                    int cleanTo = blockCountLastBlockIndex[0]  * Const.DATA_BLOCK_LEN  + totalOffset;
                    for (int i = cleanFrom; i < cleanTo; i++)
                    {
                        cvtVlirRecordData[i] = 0x00;
                    }
                    totalOffset = cleanTo;
                }
                else
                {
                    // last
                    int len = (blockCountLastBlockIndex[0] - 1) * Const.DATA_BLOCK_LEN + blockCountLastBlockIndex[1] + totalOffset - 1;
                    if (cvtVlirRecordData.Length != len)
                    {
                        throw new Exception(string.Format("VLIR record blog length error! ({0} <> {1})", cvtVlirRecordData.Length,len));
                    }
                }
            }
            return cvtVlirRecordData;
        }
        public static byte[] CleanCvtVlirRecordBlock(byte[] cvtVLIRRecordBlock)
        {
            bool firstNull = false;
            bool secondNull = false;
            for (int i = 0; i < cvtVLIRRecordBlock.Length; i++)
            {
                if (firstNull && secondNull)
                {
                    cvtVLIRRecordBlock[i] = 0x00;
                }
                else
                {
                    if (cvtVLIRRecordBlock[i] == 0)
                    {
                        if (firstNull)
                        {
                            secondNull = true;
                        }
                        else
                        {
                            firstNull = true;
                        }
                    }
                    else
                    {
                        firstNull = false;
                    }                    
                }
            }
            return cvtVLIRRecordBlock;
        }
        public static byte[] GetCleanCvtFromCvt(byte[] cvtData)
        {
            // CVT
            // Block 0 = Signature Block
            // Block 1 = Info Block
            // Block 2 = Record Block by VLIR | First Data Block by SEQ
            // Block 3 = First Data Block by VLIR
            // Block n = Data Block n

            byte[] cvtSignatureBlock;
            byte[] cvtGeosInfoBlock;
            byte[] cvtVLIRRecordBlock;
            byte[] cvtRecordData;
            MemoryStream ms = new MemoryStream();

            // Signature Block
            cvtSignatureBlock = GEOSDisk.ReadOneDataBlockFromCvt(cvtData, 0);
            byte[] dirEntry = cvtSignatureBlock.Take(Const.DIR_ENTRY_LEN).ToArray();
            if (IsGeosFile(dirEntry) == false)
            {
                return null;
            }            
            string fileSignature = System.Text.Encoding.ASCII.GetString(cvtSignatureBlock.Skip(Const.DIR_ENTRY_LEN).Take(Const.CVT_FILE_SIGNATURE_PRG.Length).ToArray());
            if (fileSignature != Const.CVT_FILE_SIGNATURE_PRG)
            {
                throw new Exception(String.Format("The file signature is {0} but {1}!", fileSignature, Const.CVT_FILE_SIGNATURE_PRG));
            }
            cvtSignatureBlock = ClearCvtSignatureBlock(cvtSignatureBlock);
            ms.Write(cvtSignatureBlock, 0, cvtSignatureBlock.Length);

            // Info Block
            cvtGeosInfoBlock = ReadOneDataBlockFromCvt(cvtData, 1);
            ms.Write(cvtGeosInfoBlock, 0, cvtGeosInfoBlock.Length);
            
            if (GetGEOSFileStructure(dirEntry) == (int)Const.GEOS_FILE_STRUCTURE.SEQ)
            {
                // Data Block
                cvtRecordData = ReadDataBlocksFromCvt(cvtData, 2,true);
                ms.Write(cvtRecordData, 0, cvtRecordData.Length);
            }
            else if (GetGEOSFileStructure(dirEntry) == (int)Const.GEOS_FILE_STRUCTURE.VLIR)
            {
                // Record Block only by VLIR
                cvtVLIRRecordBlock = CleanCvtVlirRecordBlock(ReadOneDataBlockFromCvt(cvtData, 2));
                ms.Write(cvtVLIRRecordBlock, 0, cvtVLIRRecordBlock.Length);
                // Data Block
                cvtRecordData = CleanCvtVlirRecordData(ReadDataBlocksFromCvt(cvtData, 3, true), cvtVLIRRecordBlock);
                ms.Write(cvtRecordData, 0, cvtRecordData.Length);
            }
            return ms.ToArray();
        }
        public static byte[] ReadOneDataBlockFromCvt(byte[] cvtData, int blockIndex)
        {
            return ReadDataBlocksFromCvt(cvtData, blockIndex,false);
        }
        public static byte[] ReadDataBlocksFromCvt(byte[] cvtData, int blockIndex,bool readToEOF)
        {
            byte[] blockData = null;                      
            int offset = blockIndex * Const.DATA_BLOCK_LEN;
            if (readToEOF)
            {
                blockData = cvtData.Skip(offset).ToArray();
            }
            else
            {
                blockData = cvtData.Skip(offset).Take(Const.DATA_BLOCK_LEN).ToArray();
            }
            return blockData;
        }
        public static byte[] ClearCvtSignatureBlock(byte[] cvtSignatureBlock)
        {
            cvtSignatureBlock[Const.DATA_BLOCK_TRACK_POS_IN_DIR_ENTRY] = 0x00;
            cvtSignatureBlock[Const.DATA_BLOCK_SECTOR_POS_IN_DIR_ENTRY] = 0x00;
            cvtSignatureBlock[Const.GEOS_INFO_BLOCK_TRACK_POS_IN_DIR_ENTRY] = 0x00;
            cvtSignatureBlock[Const.GEOS_INFO_BLOCK_SECTOR_POS_IN_DIR_ENTRY] = 0x00;

            for (int i = Const.CVT_DIR_BLOCK_CLEAR_FROM_POS; i < Const.DATA_BLOCK_LEN; i++)
            {
                cvtSignatureBlock[i] = 0x00;
            }
            return cvtSignatureBlock;
        }
        public static byte[] GetClearCvtSignatureBlock(byte[] dirEntry)
        {
            MemoryStream ms = new MemoryStream();
            ms.Write(dirEntry, 0, dirEntry.Length);
            Encoding e = Encoding.ASCII;
            byte[] prgSignature = e.GetBytes(Const.CVT_FILE_SIGNATURE_PRG).ToArray();
            ms.Write(prgSignature, 0, prgSignature.Length);
            byte[] fillByte = new byte[Const.DATA_BLOCK_LEN - ms.Length];
            ms.Write(fillByte, 0, fillByte.Length);
            return ClearCvtSignatureBlock(ms.ToArray());
        }
        public static byte[] GetCVTFromGeosFile(byte[] dirEntry, byte[] imageData, int imageDataType)
        {
            MemoryStream ms = new MemoryStream();
            if (IsGeosFile(dirEntry) == false)
            {
                return null;
            }
            byte[] cvtSignatureBlock;
            byte[] cvtGeosInfoBlock;
            byte[] cvtVLIRRecordBlock;
            byte[] cvtRecordData;

            cvtSignatureBlock = GetClearCvtSignatureBlock(dirEntry);
            ms.Write(cvtSignatureBlock, 0, cvtSignatureBlock.Length);
            cvtGeosInfoBlock = GetGeosInfoBlock(dirEntry, imageData, imageDataType);
            ms.Write(cvtGeosInfoBlock, 2, cvtGeosInfoBlock.Length - 2); // nur die Daten ohne die ersten 2 Byte Spur/Sektor
            if (GetGEOSFileStructure(dirEntry) == (int)Const.GEOS_FILE_STRUCTURE.SEQ)
            {
                cvtRecordData = DOSDisk.ReadBlockChain(
                    dirEntry[Const.DATA_BLOCK_TRACK_POS_IN_DIR_ENTRY],
                    dirEntry[Const.DATA_BLOCK_SECTOR_POS_IN_DIR_ENTRY],
                    imageData, imageDataType
                );
                ms.Write(cvtRecordData, 0, cvtRecordData.Length);
            }
            else if (GetGEOSFileStructure(dirEntry) == (int)Const.GEOS_FILE_STRUCTURE.VLIR)
            {
                cvtVLIRRecordBlock = GetGeosRecordBlock(dirEntry, imageData, imageDataType);
                cvtRecordData = GetGeosBlockChains(ref cvtVLIRRecordBlock, imageData,  imageDataType);
                ms.Write(cvtVLIRRecordBlock, 2, cvtVLIRRecordBlock.Length - 2); // nur die Daten ohne die ersten 2 Byte Spur/Sektor
                ms.Write(cvtRecordData, 0, cvtRecordData.Length);
            }
            return ms.ToArray();
        }
        public static byte[] GetGeosBlockChains(ref byte[] geosRecordBlock, byte[] imageData, int imageDataType)
        {
            int sectorCount = 0;
            int lastSectorIndex = 0;
            MemoryStream ms = new MemoryStream();
            int index = 2;
            int track = geosRecordBlock[index];
            int sector = geosRecordBlock[index + 1];
            while (track != 0 || sector != 0)
            {
                if (sector != 0xFF)
                {
                    if (lastSectorIndex > 0)
                    {
                        byte[] b = new byte[Const.BLOCK_LEN - lastSectorIndex - 1];
                        ms.Write(b, 0, b.Length);
                    }
                    byte[] cvtRecordData = DOSDisk.ReadBlockChain2(
                        track,
                        sector,
                        imageData,  imageDataType,
                        ref sectorCount,
                        ref lastSectorIndex
                    );
                    ms.Write(cvtRecordData, 0, cvtRecordData.Length);
                    geosRecordBlock[index] = (byte)sectorCount;
                    geosRecordBlock[index + 1] = (byte)lastSectorIndex;
                }

                index += 2;
                if (index + 1 < Const.BLOCK_LEN)
                {
                    track = geosRecordBlock[index];
                    sector = geosRecordBlock[index + 1];
                }
                else
                {
                    track = 0;
                    sector = 0;
                }
                
            }
            return ms.ToArray();
        }
        public static byte[] GetGeosInfoBlock(byte[] dirEntry, byte[] imageData,int imageDataType)
        {
            byte[] blockData = DOSDisk.ReadBlock(
                dirEntry[Const.GEOS_INFO_BLOCK_TRACK_POS_IN_DIR_ENTRY],
                dirEntry[Const.GEOS_INFO_BLOCK_SECTOR_POS_IN_DIR_ENTRY],
                imageData,
                imageDataType
                );
            return blockData;
        }
        public static byte[] GetGeosRecordBlock(byte[] dirEntry, byte[] imageData, int imageDataType)
        {
            byte[] blockData = DOSDisk.ReadBlock(
                dirEntry[Const.GEOS_RECORD_BLOCK_TRACK_POS_IN_DIR_ENTRY],
                dirEntry[Const.GEOS_RECORD_BLOCK_SECTOR_POS_IN_DIR_ENTRY],
                imageData,
                imageDataType
                );
            return blockData;
        }
        #endregion

        #region [GEOS-DISK] GEOS DIR-Entry
        public static bool IsGeosFile(byte[] dirEntry)
        {
            int FileType = DOSDisk.GetFileType(dirEntry);
            int GEOSFiletype = GetGEOSFiletype(dirEntry);
            int GEOSFileStructure = GetGEOSFileStructure(dirEntry);
            if (
                 ((FileType == (int)(Const.DOS_FILE_TYPE.Sequential)) || (FileType == (int)Const.DOS_FILE_TYPE.Program) || (FileType == (int)Const.DOS_FILE_TYPE.User)) // REL files are not allowed
                 &&
                 (GEOSFiletype > (int)(Const.GEOS_FILE_TYPE.NonGEOS)) // $00 - Non-GEOS (normal C64 file)
                 &&
                 ((GEOSFileStructure == (int)(Const.GEOS_FILE_STRUCTURE.SEQ)) || (GEOSFileStructure == (int)(Const.GEOS_FILE_STRUCTURE.VLIR)))  // $00 - Sequential, $01 - VLIR file
               )
            {
                return true;
            }
            return false;
        }
        public static string GetGEOSFileStructureName(byte[] dirEntry)
        {
            int geosFileStructure = GetGEOSFileStructure(dirEntry);
            if (geosFileStructure < Const.GEOS_FILE_STRUCTURE_NAMES.Length)
            {
                return Const.GEOS_FILE_STRUCTURE_NAMES[geosFileStructure];
            }
            else
            {
                return Const.GEOS_FILE_STRUCTURE_NAME_UNKNOWN; // "???"
            }
        }
        public static int GetGEOSFileStructure(byte[] dirEntry)
        {
            return dirEntry[Const.GEOS_FILE_STRUCTURE_POS_IN_DIR_ENTRY];
        }
        public static string GetGEOSFiletypeName(byte[] dirEntry)
        {
            int geosFiletype = GetGEOSFiletype(dirEntry);
            if (geosFiletype < Const.GEOS_FILE_TYPE_NAMES.Length)
            {
                return Const.GEOS_FILE_TYPE_NAMES[geosFiletype];
            }
            else
            {
                return Const.GEOS_FILETYPE_NAME_UNKNOWN; // "???"
            }
        }
        public static int GetGEOSFiletype(byte[] dirEntry)
        {
            return dirEntry[Const.GEOS_FILETYPE_POS_IN_DIR_ENTRY];
        }
        #endregion
    }
    public static class G64
    {
        public static byte[] ReadBlockG64(int track, int sector, byte[] imageData)
        {
            byte[] blockData = null;

            int trackOffset;
            int trackLen;
            GetG64TrackOffsetAndLenghts(track, imageData, out trackOffset, out trackLen);            
            blockData = ReadSectorG64(ref imageData, sector, trackOffset, trackLen);
            return blockData;
        }
        public static void GetG64TrackOffsetAndLenghts(int track, byte[] imageData, out int offset, out int len)
        {
            offset = 0;
            len = 0;

            // Addr  00 01 02 03 04 05 06 07 08 09 0A 0B 0C 0D 0E 0F        ASCII
            // ----  -----------------------------------------------   ----------------
            // 0000: 47 43 52 2D 31 35 34 31 00 54 F8 1E .. .. .. ..   GCR-1541?T°?....
            // 
            //   Bytes: $0000-0007: File signature "GCR-1541"
            //                0008: G64 version (presently only $00 defined)
            //                0009: Number of tracks in image (usually $54, decimal 84)
            //           000A-000B: Size of each stored track in bytes (usually  7928,  or
            //                      $1EF8 in LO/HI format.

            // Check 'File signature'
            byte[] first8Byte = imageData.Take(8).ToArray();
            if (Encoding.ASCII.GetString(first8Byte) != "GCR-1541")
            {
                throw new Exception("file signature error");
            }

            // Check Number of tracks in image
            int numberOfTracks = imageData[9];
            if (track > numberOfTracks)
            {
                throw new Exception("number of track error");
            }
            //       00 01 02 03 04 05 06 07 08 09 0A 0B 0C 0D 0E 0F        ASCII
            //       -----------------------------------------------   ----------------
            // 0000: .. .. .. .. .. .. .. .. .. .. .. .. AC 02 00 00   ............¬úúú
            // 0010: 00 00 00 00 A6 21 00 00 00 00 00 00 A0 40 00 00   úúúú¦!úúúúúú @úú
            // 0020: 00 00 00 00 9A 5F 00 00 00 00 00 00 94 7E 00 00   úúúúš_úúúúúú”~úú
            // 0030: 00 00 00 00 8E 9D 00 00 00 00 00 00 88 BC 00 00   úúúúŽúúúúúúˆ¼úú
            // 0040: 00 00 00 00 82 DB 00 00 00 00 00 00 7C FA 00 00   úúúú‚Ûúúúúúú|úúú
            // 0050: 00 00 00 00 76 19 01 00 00 00 00 00 70 38 01 00   úúúúvúúúúúúúp8úú
            // 0060: 00 00 00 00 6A 57 01 00 00 00 00 00 64 76 01 00   úúúújWúúúúúúdvúú
            // 0070: 00 00 00 00 5E 95 01 00 00 00 00 00 58 B4 01 00   úúúú^•úúúúúúX´úú
            // 0080: 00 00 00 00 52 D3 01 00 00 00 00 00 4C F2 01 00   úúúúRÓúúúúúúLòúú
            // 0090: 00 00 00 00 46 11 02 00 00 00 00 00 40 30 02 00   úúúúFúúúúúúú@0úú
            // 00A0: 00 00 00 00 3A 4F 02 00 00 00 00 00 34 6E 02 00   úúúú:Oúúúúúú4núú
            // 00B0: 00 00 00 00 2E 8D 02 00 00 00 00 00 28 AC 02 00   úúúú.úúúúúú(¬úú
            // 00C0: 00 00 00 00 22 CB 02 00 00 00 00 00 1C EA 02 00   úúúú"Ëúúúúúúúêúú
            // 00D0: 00 00 00 00 16 09 03 00 00 00 00 00 10 28 03 00   úúúúúúúúúúúúú(úú
            // 00E0: 00 00 00 00 0A 47 03 00 00 00 00 00 04 66 03 00   úúúúúGúúúúúúúfúú
            // 00F0: 00 00 00 00 FE 84 03 00 00 00 00 00 F8 A3 03 00   úúúúþ„úúúúúúø£úú
            // 0100: 00 00 00 00 F2 C2 03 00 00 00 00 00 EC E1 03 00   úúúúòÂúúúúúúìáúú
            // 0110: 00 00 00 00 E6 00 04 00 00 00 00 00 E0 1F 04 00   úúúúæúúúúúúúàúúú
            // 0120: 00 00 00 00 DA 3E 04 00 00 00 00 00 D4 5D 04 00   úúúúÚ>úúúúúúÔ]úú
            // 0130: 00 00 00 00 CE 7C 04 00 00 00 00 00 C8 9B 04 00   úúúúÎ|úúúúúúÈ›úú
            // 0140: 00 00 00 00 C2 BA 04 00 00 00 00 00 BC D9 04 00   úúúúÂºúúúúúú¼Ùúú
            // 0150: 00 00 00 00 B6 F8 04 00 00 00 00 00 .. .. .. ..   úúúú¶øúúúúú.....
            // 
            //   Bytes: $000C-000F: Offset  to  stored  track  1.0  ($000002AC,  in  LO/HI
            //                      format, see below for more)
            //           0010-0013: Offset to stored track 1.5 ($00000000)
            //           0014-0017: Offset to stored track 2.0 ($000021A6)
            //              ...
            //           0154-0157: Offset to stored track 42.0 ($0004F8B6)
            //           0158-015B: Offset to stored track 42.5 ($00000000)

            // Get Offset
            byte[] offsetToTrackArray = imageData.Skip((track - 1) * 8 + 0x0c).Take(4).ToArray();
            // If the system architecture is little-endian (that is, little end first),
            // reverse the byte array.
            if (!BitConverter.IsLittleEndian)
            {
                Array.Reverse(offsetToTrackArray);
            }
            offset = BitConverter.ToInt32(offsetToTrackArray, 0) + 2;
            byte[] offsetToTrackLenArray = imageData.Skip(offset - 2).Take(2).ToArray();
            if (!BitConverter.IsLittleEndian)
            {
                Array.Reverse(offsetToTrackLenArray);
            }
            len = BitConverter.ToInt16(offsetToTrackLenArray, 0);

            // From the track 1.0 entry we see it is set for $000002AC.  Going  to  that
            // file offset, here is what we see...
            // 
            //       00 01 02 03 04 05 06 07 08 09 0A 0B 0C 0D 0E 0F        ASCII
            //       -----------------------------------------------   ----------------
            // 02A0: .. .. .. .. .. .. .. .. .. .. .. .. 0C 1E FF FF   ............úúúú
            // 02B0: FF FF FF 52 54 B5 29 4B 7A 5E 95 55 55 55 55 55   úúúRTµ)Kz^•UUUUU
            // 02C0: 55 55 55 55 55 55 FF FF FF FF FF 55 D4 A5 29 4A   UUUUUUúúúúúUÔ¥)J
            // 02D0: 52 94 A5 29 4A 52 94 A5 29 4A 52 94 A5 29 4A 52   R”¥)JR”¥)JR”¥)JR
            // 
            //   Bytes: $02AC-02AD: Actual size of stored track (7692 or $1E0C,  in  LO/HI
            //                      format)
            //           02AE-02AE+$1E0C: Track data
        }
        public static int FindNextSync(ref byte[] imageData, int trackOffset, int trackLen, int startPosBit)
        {
            if (startPosBit > trackLen * 8)
                return (0);

            int bitCount = 0;
            for (int i = startPosBit; i < (trackLen * 8) - 1; i++)
            {
                if (bitByByteArray(ref imageData, trackOffset, trackLen, i))
                {
                    bitCount++;
                }
                else
                {
                    if (bitCount >= 10)
                    {
                        return (i);
                    }
                    bitCount = 0;
                }
            }
            return 0;
        }
        public static byte[] ReadSectorG64(ref byte[] imageData, int sector, int trackOffset, int trackLen)
        {
            byte[] sectorkData = null;
            byte[] headerDataGCR;
            byte[] sectorDataGCR;
            int nextSyncBit = FindNextSync(ref imageData, trackOffset, trackLen, 0);
            while (nextSyncBit > 0)
            {
                byte idByte = GetByte(ref imageData, trackOffset, trackLen, nextSyncBit);
                if (idByte == 0x52)
                {
                    headerDataGCR = GetBytes(ref imageData, trackOffset, trackLen, nextSyncBit, 10);
                    byte[] headerData = convertBytesFromByteGCR(headerDataGCR);
                    // MessageBox.Show(ByteArrayToString(headerData));
                    if (headerData[2] == sector)
                    {
                        nextSyncBit = FindNextSync(ref imageData, trackOffset, trackLen, nextSyncBit);
                        byte idDataByte = GetByte(ref imageData, trackOffset, trackLen, nextSyncBit);
                        if (idDataByte == 0x55)
                        {
                            sectorDataGCR = GetBytes(ref imageData, trackOffset, trackLen, nextSyncBit, 325);
                            byte[] sectorData = convertBytesFromByteGCR(sectorDataGCR);
                            sectorkData = sectorData.Skip(1).Take(256).ToArray();
                            return sectorkData;
                        }
                        else
                        {
                            // Fehler
                        }
                    }
                    else
                    {
                        // weitermachen
                    }
                }
                //MessageBox.Show(idByte.ToString());
                nextSyncBit = FindNextSync(ref imageData, trackOffset, trackLen, nextSyncBit);
            }
            return sectorkData;
        }
        public static byte[] convertBytesFromByteGCR(byte[] gcr)
        {
            byte[] fiveBytes;
            byte[] fourBytes;
            if (gcr.Length % 5 != 0) // muss durch 5 Teilbar sein
            {
                throw new Exception("convertBytesFromByteGCR error");
            }
            int loops = gcr.Length / 5;
            byte[] retBytes = new byte[loops * 4];
            int y = 0;
            for (int i = 0; i < loops; i++)
            {
                fiveBytes = gcr.Skip(i * 5).Take(5).ToArray();
                fourBytes = convert4BytesFrom5ByteGCR(fiveBytes);
                retBytes[y] = fourBytes[0];
                retBytes[y + 1] = fourBytes[1];
                retBytes[y + 2] = fourBytes[2];
                retBytes[y + 3] = fourBytes[3];
                y += 4;
            }
            return retBytes;
        }
        public static byte[] convert4BytesFrom5ByteGCR(byte[] gcr)
        {
            byte[] retBytes = new byte[4];
            /* GCR-to-Nibble conversion tables */
            byte[] GCR_decode_high = {
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
                0xff, 0x80, 0x00, 0x10, 0xff, 0xc0, 0x40, 0x50,
                0xff, 0xff, 0x20, 0x30, 0xff, 0xf0, 0x60, 0x70,
                0xff, 0x90, 0xa0, 0xb0, 0xff, 0xd0, 0xe0, 0xff
            };

            byte[] GCR_decode_low = {
                0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
                0xff, 0x08, 0x00, 0x01, 0xff, 0x0c, 0x04, 0x05,
                0xff, 0xff, 0x02, 0x03, 0xff, 0x0f, 0x06, 0x07,
                0xff, 0x09, 0x0a, 0x0b, 0xff, 0x0d, 0x0e, 0xff
            };
            byte hnibble, lnibble;
            int badGCR;
            // int nConverted;

            badGCR = 0;

            hnibble = GCR_decode_high[gcr[0] >> 3];
            lnibble = GCR_decode_low[((gcr[0] << 2) | (gcr[1] >> 6)) & 0x1f];
            if ((hnibble == 0xff || lnibble == 0xff) && (badGCR == 0)) // !badGCR -> (badGCR==0)
                badGCR = 1;
            retBytes[0] = (byte)(hnibble | lnibble);

            hnibble = GCR_decode_high[(gcr[1] >> 1) & 0x1f];
            lnibble = GCR_decode_low[((gcr[1] << 4) | (gcr[2] >> 4)) & 0x1f];
            if ((hnibble == 0xff || lnibble == 0xff) && (badGCR == 0))
                badGCR = 2;
            retBytes[1] = (byte)(hnibble | lnibble);

            hnibble = GCR_decode_high[((gcr[2] << 1) | (gcr[3] >> 7)) & 0x1f];
            lnibble = GCR_decode_low[(gcr[3] >> 2) & 0x1f];
            if ((hnibble == 0xff || lnibble == 0xff) && (badGCR == 0))
                badGCR = 3;
            retBytes[2] = (byte)(hnibble | lnibble);

            hnibble = GCR_decode_high[((gcr[3] << 3) | (gcr[4] >> 5)) & 0x1f];
            lnibble = GCR_decode_low[gcr[4] & 0x1f];
            if ((hnibble == 0xff || lnibble == 0xff) && (badGCR == 0))
                badGCR = 4;
            retBytes[3] = (byte)(hnibble | lnibble);

            // nConverted = (badGCR == 0) ? 4 : (badGCR - 1);

            return (retBytes);
        }
        public static Boolean bitByByteArray(ref byte[] ba, int offset, int len, int bitIndex)
        {
            int pos = bitIndex / 8;
            if (pos > offset + len)
            {
                throw new Exception("Bit Array error");
            }
            int pos2 = bitIndex - (pos * 8);
            byte b = ba[pos + offset];
            return GetBit(b, pos2);
        }
        static bool GetBit(byte b, int bitNumber)
        {
            //return (b & (1 << bitNumber)) > 0;
            return (b & (1 << 7 - bitNumber)) > 0;
        }
        static byte GetByte(ref byte[] ba, int offset, int len, int bitIndex)
        {
            if (bitIndex + 7 > len * 8)
            {
                throw new Exception("GetByte error");
            }
            byte b = 0; // Clear all Bits
            for (int pos = 0; pos <= 7; pos++)
            {
                if (bitByByteArray(ref ba, offset, len, bitIndex + pos))
                {
                    b = (byte)(b | (1 << 7 - pos));
                }
            }
            return b;
        }
        static byte[] GetBytes(ref byte[] ba, int offset, int len, int bitIndex, int byteCount)
        {
            byte[] ret = new byte[byteCount];
            for (int i = 0; i < byteCount; i++)
            {
                ret[i] = GetByte(ref ba, offset, len, bitIndex + (i * 8));
            }
            return ret;
        }
    }
    public static class DOSDisk
    {
        #region [DISK] File
        public static string GetMD5ByCBMFile(byte[] dirEntry, byte[] imageData, int imageDataType)
        {
            byte[] fileData = getFileData(dirEntry, imageData,  imageDataType);
            return Core.GetMD5Hash(fileData);
        }
        // BUILD of MegaPatch V3.0 File
        public static string GetInfoTextByMP3File(byte[] dirEntry, byte[] imageData, int imageDataType)
        {
            int pos1 = -1;
            int pos2 = -1;
            string text = "";
            byte[] fileData = getFileData(dirEntry, imageData, imageDataType);
            String textStr = Encoding.ASCII.GetString(fileData);
            pos1 = textStr.IndexOf("BUILD:");
            if (pos1 > 0)
            {
                pos2 = textStr.IndexOf("\u0000", pos1);
            }
            if ((pos1 >= 0) && (pos2 >= 0))
            {
                text = textStr.Substring(pos1, pos2 - pos1);
            }
            if (text.Length > 0)
            {
                string lang = GetInfoTextLANGUAGEByMP3File(dirEntry, imageData, imageDataType);
                if (lang.Length > 0)
                    text = text + " " + lang;
                string c64c128 = GetInfoTextC64C128ByMP3File(dirEntry, imageData, imageDataType);
                if (c64c128.Length > 0)
                    text = text + " " + c64c128;

            }
            return text;
        }
        // BUILD of MegaPatch V3.0 File
        public static string GetInfoTextLANGUAGEByMP3File(byte[] dirEntry, byte[] imageData, int imageDataType)
        {
            int pos1 = -1;
            int pos2 = -1;            
            byte[] fileData = getFileData(dirEntry, imageData, imageDataType);
            String textStr = Encoding.ASCII.GetString(fileData);
            pos1 = textStr.IndexOf("Ziel");
            pos2 = textStr.IndexOf("Target");
            if ((pos1 >= 0) && (pos2 >= 0))
            {
                return "???";
            }
            if (pos1 > 0)
            {
                return "Deutsch";
            }
            if (pos2 > 0)
            {
                return "Englisch";
            }
            return "---";
        }
        public static string GetInfoTextC64C128ByMP3File(byte[] dirEntry, byte[] imageData, int imageDataType)
        {
            int pos1 = -1;
            int pos2 = -1;
            byte[] fileData = getFileData(dirEntry, imageData, imageDataType);
            String textStr = Encoding.ASCII.GetString(fileData);
            pos1 = textStr.IndexOf("StartMP3_128"); // StartMP3_128 MegaPatch128
            pos2 = textStr.IndexOf("StartMP3_64"); // StartMP3_64 MegaPatch64
            if ((pos1 >= 0) && (pos2 >= 0))
            {
                return "???";
            }
            if (pos1 > 0)
            {
                return "C128";
            }
            if (pos2 > 0)
            {
                return "C64";
            }
            return "---";
        }
        public static byte[] getFileData(byte[] dirEntry, byte[] imageData, int imageDataType)
        {
            int track = dirEntry[Const.DATA_BLOCK_TRACK_POS_IN_DIR_ENTRY];
            int sector = dirEntry[Const.DATA_BLOCK_SECTOR_POS_IN_DIR_ENTRY];
            byte[] fileData;
            if (GEOSDisk.IsGeosFile(dirEntry))
            {
                fileData = GEOSDisk.GetCVTFromGeosFile(dirEntry, imageData,  imageDataType);
            }
            else
            {
                fileData = ReadBlockChain(track, sector, imageData,  imageDataType);
            }
            return fileData;
        }
        #endregion

        #region [DISK] DIR-Entry
        public static string GetLockFlagSign(byte[] dirEntry)
        {
            if (IsLockFlag(dirEntry))
            {
                return Const.LOCK_FLAG_SIGN.ToString();
            }
            else
            {
                return Const.LOCK_FLAG_SIGN_EMPTY; // " "
            }
        }
        public static bool IsLockFlag(byte[] DirEntry)
        {
            return ((byte)(DirEntry[0] & Const.LOCK_FLAG_BIT_MASK) != 0); // 0x40 = 0b01000000
        }
        public static string GetSplatFileSign(byte[] dirEntry)
        {
            if (IsSplatFile(dirEntry))
            {
                return Const.SPLAT_FILE_SIGN.ToString();
            }
            else
            {
                return Const.SPLAT_FILE_SIGN_EMPTY; // " "
            }
        }
        public static bool IsSplatFile(byte[] dirEntry)
        {
            return ((byte)(dirEntry[0] & Const.SPLAT_FILE_BIT_MASK) == 0); // 0x80 = 0b10000000
        }
        public static int GetFileSizeInBlocks(byte[] dirEntry)
        {
            return dirEntry[Const.USED_BLOCKS_HIGH_POS_IN_DIR_ENTRY] * Const.BLOCK_LEN + dirEntry[Const.USED_BLOCKS_LOW_POS_IN_DIR_ENTRY];
        }
        public static string GetFileTypeExt(byte[] dirEntry)
        {
            int fileType = GetFileType(dirEntry);
            if (fileType <= Const.DOS_FILE_TYPE_EXT.Length)
            {
                return Const.DOS_FILE_TYPE_EXT[fileType];
            }
            else
            {
                return Const.DOS_FILE_TYPE_EXT_UNKNOWN; // "???"
            }
        }
        public static int GetFileType(byte[] dirEntry)
        {
            //Bit 0-3: The actual filetype
            //  000 (0) - DEL
            //  001 (1) - SEQ
            //  010 (2) - PRG
            //  011 (3) - USR
            //  100 (4) - REL
            //  Values 5-15 are illegal, but if used will produce
            //  very strange results. The 1541 is inconsistent in
            //  how it treats these bits. Some routines use all 4
            //  bits, others ignore bit 3,  resulting  in  values
            //  from 0-7.
            return (int)(dirEntry[0] & Const.FILE_TYPE_BIT_MASK); // 0x07 = 0b00000111
        }
        public static bool IsDirEntryEmpty(byte[] dirEntry)
        {
            if (dirEntry[0] == 0)
            {
                return true;
            }
            return false;
        }
        public static byte[] GetFilename(byte[] dirEntry)
        {
            byte[] fullFilename = GetFullFilename(dirEntry);
            int termIndex = Array.IndexOf(fullFilename, Const.TERMINATE_BYTE);
            if (termIndex >= 0)
            {
                return fullFilename.Take(termIndex).ToArray();
            }
            else
            {
                return fullFilename;
            }
        }
        public static byte[] GetPartAfterFilename(byte[] dirEntry)
        {
            byte[] fullFilename = GetFullFilename(dirEntry);
            int termIndex = Array.IndexOf(fullFilename, Const.TERMINATE_BYTE);
            if ((termIndex >= 0) && (termIndex < fullFilename.Length))
            {
                return fullFilename.Skip(termIndex + 1).Concat(new byte[1] { 32 }).ToArray(); // 32 = SPACE                
            }
            else
            {
                return new byte[0];
            }
        }
        public static byte[] GetFullFilename(byte[] dirEntry)
        {
            return dirEntry.Skip(Const.FILENAME_POS_IN_DIR_ENTRY).Take(Const.FILENAME_LEN).ToArray();
        }
        #endregion

        #region [DISK] BLOCK / BAM-BLOCK / DIR-Entry
        public static ArrayList GetDirEntryList(byte[] bamBlock, byte[] imageData, int imageDataType)
        {
            ArrayList dirEntries = new ArrayList();
            int nextDirTrack = bamBlock[Const.NEXT_DIR_TRACK_POS_IN_DIR_ENTRY];
            int nextDirSector = bamBlock[Const.NEXT_DIR_SECTOR_POS_IN_DIR_ENTRY];
            byte[] dirBlock;
            while (nextDirTrack > 0)
            {
                dirBlock = ReadBlock(nextDirTrack, nextDirSector, imageData, imageDataType);
                AddDirEntriesToDirEntryList(ref dirEntries, dirBlock);
                nextDirTrack = dirBlock[Const.NEXT_DIR_TRACK_POS_IN_DIR_ENTRY];
                nextDirSector = dirBlock[Const.NEXT_DIR_SECTOR_POS_IN_DIR_ENTRY];
            }
            return dirEntries;
        }
        public static void AddDirEntriesToDirEntryList(ref ArrayList dirEntryList, byte[] dirBlock)
        {
            for (int i = 0; i <= Const.NUM_OF_DIR_ENTRIES_IN_DIR_BLOCK - 1; i++)
            {
                byte[] dirEntry = new byte[Const.DIR_ENTRY_LEN];
                dirEntry = dirBlock.Skip(i * (Const.DIR_ENTRY_LEN + Const.NUM_OF_FILL_BYTES_BETWEEN_DIR_ENTRIES) + Const.FIRST_DIR_ENTRY_POS_IN_DIR_BLOCK).Take(Const.DIR_ENTRY_LEN).ToArray();
                dirEntryList.Add(dirEntry);
            }
        }
        #endregion

        #region [DISK] BAM-Block
        public static int GetFreeBlocks(byte[] bamBlock)
        {
            // only for single sided d64 images
            int freeBlocks = 0;
            for (int i = 1; i <= Const.MAX_TRACK_D64; i++) // MAX_TRACK muss hier 35 sein
            {
                if (i != Const.BAM_TRACK_D64) // Die freien Blöcke auf Track 18 nicht mit zählen
                {
                    freeBlocks += bamBlock[4 * i];
                }
            }
            return freeBlocks;
        }
        public static byte[] GetDOSType(byte[] bamBlock, int imageDataType)
        {
            switch (imageDataType)
            {
                case (int)Const.IMAGE_DATA_TYPE.D64:
                    return bamBlock.Skip(Const.DOS_TYPE_POS_IN_BAM_BLOCK_D64).Take(Const.DOS_TYPE_LEN).ToArray();
                // D71
                case (int)Const.IMAGE_DATA_TYPE.D71:
                    return bamBlock.Skip(Const.DOS_TYPE_POS_IN_BAM_BLOCK_D71).Take(Const.DOS_TYPE_LEN).ToArray();
                // D81
                case (int)Const.IMAGE_DATA_TYPE.D81:
                    return bamBlock.Skip(Const.DOS_TYPE_POS_IN_BAM_BLOCK_D81).Take(Const.DOS_TYPE_LEN).ToArray();
                // G64
                case (int)Const.IMAGE_DATA_TYPE.G64:
                    return bamBlock.Skip(Const.DOS_TYPE_POS_IN_BAM_BLOCK_G64).Take(Const.DOS_TYPE_LEN).ToArray();
                default:
                    throw new Exception(String.Format("Image data type {0} is not supported!", imageDataType.ToString()));
            }
            
        }
        public static byte[] GetDiskID(byte[] bamBlock, int imageDataType)
        {
            switch (imageDataType)
            {
                case (int)Const.IMAGE_DATA_TYPE.D64:
                    return bamBlock.Skip(Const.DISK_ID_POS_IN_BAM_BLOCK_D64).Take(Const.DISK_ID_LEN).ToArray();
                // D71
                case (int)Const.IMAGE_DATA_TYPE.D71:
                    return bamBlock.Skip(Const.DISK_ID_POS_IN_BAM_BLOCK_D71).Take(Const.DISK_ID_LEN).ToArray();
                // D81
                case (int)Const.IMAGE_DATA_TYPE.D81:
                    return bamBlock.Skip(Const.DISK_ID_POS_IN_BAM_BLOCK_D81).Take(Const.DISK_ID_LEN).ToArray();
                // G64
                case (int)Const.IMAGE_DATA_TYPE.G64:
                    return bamBlock.Skip(Const.DISK_ID_POS_IN_BAM_BLOCK_G64).Take(Const.DISK_ID_LEN).ToArray();
                default:
                    throw new Exception(String.Format("Image data type {0} is not supported!", imageDataType.ToString()));
            }
            
        }
        public static byte[] GetDiskName(byte[] bamBlock, int imageDataType)
        {
            switch (imageDataType)
            {
                case (int)Const.IMAGE_DATA_TYPE.D64:
                    return bamBlock.Skip(Const.DISK_NAME_POS_IN_BAM_BLOCK_D64).Take(Const.DISK_NAME_LEN).ToArray();
                // D71
                case (int)Const.IMAGE_DATA_TYPE.D71:
                    return bamBlock.Skip(Const.DISK_NAME_POS_IN_BAM_BLOCK_D71).Take(Const.DISK_NAME_LEN).ToArray();
                // D81
                case (int)Const.IMAGE_DATA_TYPE.D81:
                    return bamBlock.Skip(Const.DISK_NAME_POS_IN_BAM_BLOCK_D81).Take(Const.DISK_NAME_LEN).ToArray();
                // G64
                case (int)Const.IMAGE_DATA_TYPE.G64:
                    return bamBlock.Skip(Const.DISK_NAME_POS_IN_BAM_BLOCK_G64).Take(Const.DISK_NAME_LEN).ToArray();
                default:
                    throw new Exception(String.Format("Image data type {0} is not supported!", imageDataType.ToString()));
            }
           
        }
        #endregion

        #region [DISK] BLOCK / BAM-BLOCK
        public static byte[] ReadBAMBlock(byte[] imageData, int imageDataType)
        {
            switch (imageDataType)
            {
                case (int)Const.IMAGE_DATA_TYPE.D64:
                    return ReadBlock(Const.BAM_TRACK_D64, Const.BAM_SECTOR_D64, imageData, imageDataType);
                // D71
                case (int)Const.IMAGE_DATA_TYPE.D71:
                    return ReadBlock(Const.BAM_TRACK_D71, Const.BAM_SECTOR_D71, imageData, imageDataType);
                // D81
                case (int)Const.IMAGE_DATA_TYPE.D81:
                    return ReadBlock(Const.BAM_TRACK_D81, Const.BAM_SECTOR_D81, imageData, imageDataType);
                // G64
                case (int)Const.IMAGE_DATA_TYPE.G64:
                    return ReadBlock(Const.BAM_TRACK_G64, Const.BAM_SECTOR_G64, imageData, imageDataType);
                default:
                    throw new Exception(String.Format("Image data type {0} is not supported!", imageDataType.ToString()));
            }            
        }
        #endregion

        #region [DISK] BLOCK
        public static byte[] ReadBlockChain(int track, int sector, byte[] imageData, int imageDataType)
        {
            int sectorCount = 0;
            int lastSectorIndex = 0;
            return ReadBlockChain2(track, sector, imageData,  imageDataType, ref sectorCount,ref lastSectorIndex);
        }
        public static byte[] ReadBlockChain2(int track, int sector, byte[] imageData, int imageDataType, ref int sectorCount, ref int lastSectorIndex)
        {
            byte[] blockData;
            sectorCount = 0;
            MemoryStream ms = new MemoryStream();
            while (track > 0)
            {
                blockData = ReadBlock(track, sector, imageData, imageDataType);
                track = blockData[0];
                sector = blockData[1];
                if (track > 0)
                {
                    ms.Write(blockData, 2, blockData.Length - 2);                    
                }
                else
                {
                    ms.Write(blockData, 2, sector - 1); // last sector 
                    lastSectorIndex = sector;
                }
                sectorCount++;
            }
            return ms.ToArray();
        }
        public static byte[] ReadBlock(int track, int sector, byte[] imageData, int imageDataType)
        {
            byte[] blockData = null;
            switch (imageDataType)
            {
                case (int)Const.IMAGE_DATA_TYPE.D64:
                    blockData = DiskImageFile.ReadBlockD64(track, sector, imageData);
                    break;
                // D71
                case (int)Const.IMAGE_DATA_TYPE.D71:
                    blockData = DiskImageFile.ReadBlockD71(track, sector, imageData);
                    break;
                // D81
                case (int)Const.IMAGE_DATA_TYPE.D81:
                    blockData = DiskImageFile.ReadBlockD81(track, sector, imageData);
                    break;
                // G64
                case (int)Const.IMAGE_DATA_TYPE.G64:
                    blockData = DiskImageFile.ReadBlockG64(track, sector, imageData);
                    break;
                default:
                    throw new Exception(String.Format("Image data type {0} is not supported!", imageDataType.ToString()));
            }
            return blockData;
        }
        #endregion
    }
    public static class DiskImageFile
    {
        #region with file access
        public static byte[] ReadFile(string imagePathFilename)
        {
            return File.ReadAllBytes(imagePathFilename);
        }
        public static byte[] ReadCvtFile(string imagePathFilename)
        {
            string fileExt = Path.GetExtension(imagePathFilename).ToUpper();
            if (fileExt != Const.CVT_FILE_EXTENSION)
            {
                throw new Exception("The file must be CVT file!");
            }
            return File.ReadAllBytes(imagePathFilename);
        }
        public static int GetImageDataType(string imagePathFilename)
        {
            string fileExt = Path.GetExtension(imagePathFilename).ToUpper();
            if (fileExt == Const.D64_IMAGE_FILE_EXTENSION)
            {
                return (int)Const.IMAGE_DATA_TYPE.D64;
            }
            // G64
            if (fileExt == Const.G64_IMAGE_FILE_EXTENSION)
            { 
                return (int)Const.IMAGE_DATA_TYPE.G64;
            }
            // D71
            if (fileExt == Const.D71_IMAGE_FILE_EXTENSION)
            {
                return (int)Const.IMAGE_DATA_TYPE.D71;
            }
            // D81
            if (fileExt == Const.D81_IMAGE_FILE_EXTENSION)
            {
                return (int)Const.IMAGE_DATA_TYPE.D81;
            }
            return (int)Const.IMAGE_DATA_TYPE.unknown;
        }
        #endregion
        #region [D64 Image]
        public static byte[] ReadBlockD64(int track, int sector, byte[] imageData)
        {
            byte[] blockData = null;
            // convert track/sector to byte offset in file
            int offset = GetD64Offset(track, sector);
            if (offset >= 0)
            {
                blockData = imageData.Skip(offset).Take(Const.BLOCK_LEN).ToArray();
            }
            return blockData;
        }
        // D71
        public static byte[] ReadBlockD71(int track, int sector, byte[] imageData)
        {
            byte[] blockData = null;
            // convert track/sector to byte offset in file
            int offset = GetD71Offset(track, sector);
            if (offset >= 0)
            {
                blockData = imageData.Skip(offset).Take(Const.BLOCK_LEN).ToArray();
            }
            return blockData;
        }
        // D81
        public static byte[] ReadBlockD81(int track, int sector, byte[] imageData)
        {
            byte[] blockData = null;
            // convert track/sector to byte offset in file
            int offset = GetD81Offset(track, sector);
            if (offset >= 0)
            {
                blockData = imageData.Skip(offset).Take(Const.BLOCK_LEN).ToArray();
            }
            return blockData;
        }
        // G64
        public static byte[] ReadBlockG64(int track, int sector, byte[] imageData)
        {            
            return G64.ReadBlockG64(track, sector, imageData);             
        }
        public static int GetD64Offset(int track, int sector)
        {
            if ((track < Const.MIN_TRACK) || (track > Const.MAX_TRACK_D64) || (sector < Const.MIN_SECTOR) || (sector >= Const.NUM_OF_SECTORS_PER_TRACK_D64[track]))
            {
                return -1;
            }
            int SumOfSectorsToTrack = Const.NUM_OF_SECTORS_PER_TRACK_D64.Take(track).Sum(); // used System.Linq;
            return (SumOfSectorsToTrack + sector) * Const.BLOCK_LEN;
        }
        // D71
        public static int GetD71Offset(int track, int sector)
        {
            if ((track < Const.MIN_TRACK) || (track > Const.MAX_TRACK_D71) || (sector < Const.MIN_SECTOR) || (sector >= Const.NUM_OF_SECTORS_PER_TRACK_D71[track]))
            {
                return -1;
            }
            int SumOfSectorsToTrack = Const.NUM_OF_SECTORS_PER_TRACK_D71.Take(track).Sum(); // used System.Linq;
            return (SumOfSectorsToTrack + sector) * Const.BLOCK_LEN;
        }
        // D81
        public static int GetD81Offset(int track, int sector)
        {
            if ((track < Const.MIN_TRACK) || (track > Const.MAX_TRACK_D81) || (sector < Const.MIN_SECTOR) || (sector >= Const.MAX_SECTOR_D81)) // D81
            {
                return -1;
            }
            return ((track -1 ) * Const.MAX_SECTOR_D81 * Const.BLOCK_LEN) + (sector * Const.BLOCK_LEN);
        }

        #endregion

        #region Write (file access)
        public static void WriteFileBlockChain(int track, int sector, byte[] imageData, int imageDataType, string outPathFilename)
        {
            byte[] blocksData = DOSDisk.ReadBlockChain(track, sector, imageData, imageDataType);
            WriteFile(blocksData, outPathFilename);
        }
        public static void WriteCVTFile(byte[] dirEntry, byte[] imageData, int imageDataType, string outPathFilename)
        {
            byte[] cvtData = GEOSDisk.GetCVTFromGeosFile(dirEntry, imageData, imageDataType);
            WriteFile(cvtData, outPathFilename);
        }
        public static void WriteFile(byte[] fileData, string outPathFilename)
        {
            File.WriteAllBytes(outPathFilename, fileData);
        }
        #endregion
    }
    public static class CmdLineArgsParser
    {
        public static Dictionary<String, String> Parse(string[] args,Dictionary<String,int[]> arguments,out string firstArgValue)
        {
            // arguments
            //   Key   ... argument
            //   Value ... propertys od argument as int[]
            //       index 0 .. fix first position (0 = false, 1 = true)
            //       index 1 .. is required (0 = false, 1 = true)
            //       index 2 .. is value required (0 = false, 1 = true)
            //       index 3 .. group of or- operators (0 = no goup, 1..n = group no.)
            bool firstArgIsFix = false;
            bool oneArgIsRequired = false;
            Dictionary<String, String> outDict = new Dictionary<string, string>();
            Dictionary<String, int[]> optionList = new Dictionary<string, int[]>();
            firstArgValue = "";
            int firstOptionIndex = 0;
            string currOption = "";
            foreach (var item in arguments)
            {
                if (item.Value[0] != 0) // 0 .. fix first position (0 = false, 1 = true)
                {
                    firstArgIsFix = true;
                    oneArgIsRequired = true;
                }
                else
                {
                    optionList.Add(item.Key, item.Value);
                    if (item.Value[1] != 0)  // 1.. is required (0 = false, 1 = true)
                    {
                        oneArgIsRequired = true;
                    }
                }
            }
            if (!oneArgIsRequired)
            {
                return outDict; // return empty
            }
            if (args.Length <= 0)
            {
                throw new Exception("Parameters are required!");
            }
            if (firstArgIsFix)
            {
                // check is first arg a option?
                if (optionList.ContainsKey(args[0]))
                {
                    throw new Exception("The first argument may not be an option!");
                }
                else
                {
                    firstArgValue = args[0];
                }
                firstOptionIndex = 1;
            }
            for (int optionIndex = firstOptionIndex; optionIndex < args.Length; optionIndex++)
            {
                if (optionList.ContainsKey(args[optionIndex])) // is option or value?
                {
                    // option
                    currOption = args[optionIndex];
                    if (outDict.ContainsKey(currOption))
                    {
                        throw new Exception(String.Format("The Option {0} occurs several times!", currOption));
                    }
                    else
                    {
                        outDict.Add(currOption, "");
                    }
                }
                else
                {
                    // value
                    if (currOption != "")
                    {
                        outDict.Remove(currOption);
                        outDict.Add(currOption, args[optionIndex]);
                        currOption = "";
                    }
                    else
                    {
                        throw new Exception(String.Format("The value {0} can not be assigned to any option!", args[optionIndex]));
                    }
                }
            }
            // check is required and is value required
            ArrayList requiredGroupList = new ArrayList();
            foreach (var item in optionList)
            {
                if (item.Value[3] > 0) // 3 .. group of or- operators (0 = no goup, 1..n = group no.)
                {
                    // group
                    if (item.Value[1] != 0) // 1 .. is required (0 = false, 1 = true)
                    {
                        if (!requiredGroupList.Contains(item.Value[3]))
                        {
                            requiredGroupList.Add(item.Value[3]);
                        }
                    }
                }
                else
                {
                    // not group
                    if (item.Value[1] != 0) // 1 .. is required (0 = false, 1 = true)
                    {
                        if (!outDict.ContainsKey(item.Key))
                        {
                            // Option is required
                            throw new Exception(String.Format("The Option {0} is required!",item.Key));
                        }
                    }
                }
            }
            Dictionary<int,String> existingGroupList = new Dictionary<int, string>();
            foreach (var item in outDict)
            {
                int groupNo = optionList[item.Key][3]; // 3 .. group of or- operators (0 = no goup, 1..n = group no.)
                if (groupNo > 0)  
                {
                    // group
                    requiredGroupList.Remove(groupNo);
                    if (existingGroupList.ContainsKey(groupNo))
                    {
                         throw new Exception(String.Format("The options {0} and {1} can not be used at the same time!",
                             existingGroupList[groupNo],item.Key));
                    }
                    else
                    {
                        existingGroupList.Add(groupNo,item.Key);
                    }
                }
                if (optionList[item.Key][2] !=0 ) // 2 .. is value required (0 = false, 1 = true)
                {
                    // value is required
                    if (item.Value.Length == 0)
                    {
                        throw new Exception(String.Format("The option {0} require a value!",item.Key));
                    }
                }
            }
            if (requiredGroupList.Count > 0)
            {
                int groupNo = (int)requiredGroupList[0];
                StringBuilder excepStrB = new StringBuilder();
                foreach (var item2 in optionList)
                {
                    if (item2.Value[3] == groupNo)
                    {
                        if (excepStrB.Length > 0)
                        {
                            excepStrB.Append(" or ");
                        }
                        excepStrB.Append(item2.Key);
                    }
                }
                throw new Exception(String.Format("The Option {0} is required!", excepStrB));
            }
            return outDict;
        }
        public static void AddArgument(ref Dictionary<String, int[]> arguments, string argKey, bool fixFirstPosition, bool isRequired, bool isValueRequired, int groupOfOrOperators)
        {
            int[] newValue = new int[4];
            newValue[0] = (fixFirstPosition ? 1 : 0);
            newValue[1] = (isRequired ? 1 : 0);
            newValue[2] = (isValueRequired ? 1 : 0);
            newValue[3] = groupOfOrOperators;
            arguments.Add(argKey, newValue);
        }
    }

    /////////////////////////////////////////////////////////////////////////
    //public static class 
}
