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
        internal const int MIN_TRACK = 1;
        internal const int MAX_TRACK = 35;
        internal const int MIN_SECTOR = 0;
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
        internal const int BAM_TRACK = 18;
        internal const int BAM_SECTOR = 0;
        internal const int DOS_TYPE_POS_IN_BAM_BLOCK = 165;
        internal const int DOS_TYPE_LEN = 2;
        internal const int DISK_ID_POS_IN_BAM_BLOCK = 162;
        internal const int DISK_ID_LEN = 2;
        internal const int DISK_NAME_POS_IN_BAM_BLOCK = 144;
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
        internal readonly static int[] NUM_OF_SECTORS_PER_TRACK = {
            0,
            21,21,21,21,21,21,21,21,21,21,21,21,21,21,21,21,21, // Zone 1
	        19,19,19,19,19,19,19,                               // Zone 2
	        18,18,18,18,18,18,                                  // Zone 3
	        17,17,17,17,17                                      // Zone 4
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
            bool done = false;
            int totalOffset = 0;
            for (int i = 0; i < cvtVLIRRecordBlock.Length; i = i + 2)
            {
                if (!done)
                {
                    if ((cvtVLIRRecordBlock[i] == 0x00) && (cvtVLIRRecordBlock[i + 1] == 0x00))
                    {
                        done = true;
                    }
                    else
                    {
                        if (!((cvtVLIRRecordBlock[i] == 0x00) && (cvtVLIRRecordBlock[i + 1] == 0xFF)))
                        {
                            int blockCount = cvtVLIRRecordBlock[i];
                            int lastBlockIndex = cvtVLIRRecordBlock[i + 1];
                            int currOffset = blockCount * Const.DATA_BLOCK_LEN + lastBlockIndex + lastBlockIndex;
                            // Claen

                            // lastBlockIndex += 
                        }
                    }
                }
            }
            return cvtVlirRecordData;
        }
        public static byte[] CleanCvtVLIRRecordBlock(byte[] cvtVLIRRecordBlock)
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
        public static byte[] GetCleanCvtFromCvt(string cvtPathFilename)
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
            cvtSignatureBlock = GEOSDisk.ReadOneDataBlockFromCvt(cvtPathFilename, 0);
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
            cvtGeosInfoBlock = ReadOneDataBlockFromCvt(cvtPathFilename,1);
            ms.Write(cvtGeosInfoBlock, 0, cvtGeosInfoBlock.Length);
            
            if (GetGEOSFileStructure(dirEntry) == (int)Const.GEOS_FILE_STRUCTURE.SEQ)
            {
                // Data Block
                cvtRecordData = ReadDataBlocksFromCvt(cvtPathFilename,2,true);
                ms.Write(cvtRecordData, 0, cvtRecordData.Length);
            }
            else if (GetGEOSFileStructure(dirEntry) == (int)Const.GEOS_FILE_STRUCTURE.VLIR)
            {
                // Record Block only by VLIR
                cvtVLIRRecordBlock = CleanCvtVLIRRecordBlock(ReadOneDataBlockFromCvt(cvtPathFilename, 2));
                ms.Write(cvtVLIRRecordBlock, 0, cvtVLIRRecordBlock.Length);
                // Data Block
                cvtRecordData = CleanCvtVlirRecordData(ReadDataBlocksFromCvt(cvtPathFilename, 3, true), cvtVLIRRecordBlock);
                ms.Write(cvtRecordData, 0, cvtRecordData.Length);
            }
            return ms.ToArray();
        }
        public static byte[] ReadOneDataBlockFromCvt(string cvtPathFilename, int blockIndex)
        {
            return ReadDataBlocksFromCvt(cvtPathFilename, blockIndex,false);
        }
        public static byte[] ReadDataBlocksFromCvt(string cvtPathFilename, int blockIndex,bool readToEOF)
        {
            byte[] blockData = null;                      
            int offset = blockIndex * Const.DATA_BLOCK_LEN;
            byte[] fileData = File.ReadAllBytes(cvtPathFilename);
            if (readToEOF)
            {
                blockData = fileData.Skip(offset).ToArray();
            }
            else
            {
                blockData = fileData.Skip(offset).Take(Const.DATA_BLOCK_LEN).ToArray();
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
        public static byte[] GetCVTFromGeosFile(byte[] dirEntry, string imagePathFilename)
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
            cvtGeosInfoBlock = GetGeosInfoBlock(dirEntry, imagePathFilename);
            ms.Write(cvtGeosInfoBlock, 2, cvtGeosInfoBlock.Length - 2); // nur die Daten ohne die ersten 2 Byte Spur/Sektor
            if (GetGEOSFileStructure(dirEntry) == (int)Const.GEOS_FILE_STRUCTURE.SEQ)
            {
                cvtRecordData = DOSDisk.ReadBlockChain(
                    dirEntry[Const.DATA_BLOCK_TRACK_POS_IN_DIR_ENTRY],
                    dirEntry[Const.DATA_BLOCK_SECTOR_POS_IN_DIR_ENTRY],
                    imagePathFilename
                );
                ms.Write(cvtRecordData, 0, cvtRecordData.Length);
            }
            else if (GetGEOSFileStructure(dirEntry) == (int)Const.GEOS_FILE_STRUCTURE.VLIR)
            {
                cvtVLIRRecordBlock = GetGeosRecordBlock(dirEntry, imagePathFilename);
                cvtRecordData = GetGeosBlockChains(ref cvtVLIRRecordBlock, imagePathFilename);
                ms.Write(cvtVLIRRecordBlock, 2, cvtVLIRRecordBlock.Length - 2); // nur die Daten ohne die ersten 2 Byte Spur/Sektor
                ms.Write(cvtRecordData, 0, cvtRecordData.Length);
            }
            return ms.ToArray();
        }
        public static byte[] GetGeosBlockChains(ref byte[] geosRecordBlock, string imagePathFilename)
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
                        imagePathFilename,
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
        public static byte[] GetGeosInfoBlock(byte[] dirEntry, string imagePathFilename)
        {
            byte[] blockData = DOSDisk.ReadBlock(
                dirEntry[Const.GEOS_INFO_BLOCK_TRACK_POS_IN_DIR_ENTRY],
                dirEntry[Const.GEOS_INFO_BLOCK_SECTOR_POS_IN_DIR_ENTRY],
                imagePathFilename
                );
            return blockData;
        }
        public static byte[] GetGeosRecordBlock(byte[] dirEntry, string imagePathFilename)
        {
            byte[] blockData = DOSDisk.ReadBlock(
                dirEntry[Const.GEOS_RECORD_BLOCK_TRACK_POS_IN_DIR_ENTRY],
                dirEntry[Const.GEOS_RECORD_BLOCK_SECTOR_POS_IN_DIR_ENTRY],
                imagePathFilename
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
    public static class DOSDisk
    {
        #region [DISK] File
        public static string GetMD5ByFile(byte[] dirEntry, string imagePathFilename)
        {
            byte[] fileData = getFileData(dirEntry, imagePathFilename);
            return Core.GetMD5Hash(fileData);
        }
        public static byte[] getFileData(byte[] dirEntry, string imagePathFilename)
        {
            int track = dirEntry[Const.DATA_BLOCK_TRACK_POS_IN_DIR_ENTRY];
            int sector = dirEntry[Const.DATA_BLOCK_SECTOR_POS_IN_DIR_ENTRY];
            byte[] fileData;
            if (GEOSDisk.IsGeosFile(dirEntry))
            {
                fileData = GEOSDisk.GetCVTFromGeosFile(dirEntry, imagePathFilename);
            }
            else
            {
                fileData = ReadBlockChain(track, sector, imagePathFilename);
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
        public static ArrayList GetDirEntryList(byte[] bamBlock, string imagePathFilename)
        {
            ArrayList dirEntries = new ArrayList();
            int nextDirTrack = bamBlock[Const.NEXT_DIR_TRACK_POS_IN_DIR_ENTRY];
            int nextDirSector = bamBlock[Const.NEXT_DIR_SECTOR_POS_IN_DIR_ENTRY];
            byte[] dirBlock;
            while (nextDirTrack > 0)
            {
                dirBlock = ReadBlock(nextDirTrack, nextDirSector, imagePathFilename);
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
            for (int i = 1; i <= Const.MAX_TRACK; i++) // MAX_TRACK muss hier 35 sein
            {
                if (i != Const.BAM_TRACK) // Die freien Blöcke auf Track 18 nicht mit zählen
                {
                    freeBlocks += bamBlock[4 * i];
                }
            }
            return freeBlocks;
        }
        public static byte[] GetDOSType(byte[] bamBlock)
        {
            return bamBlock.Skip(Const.DOS_TYPE_POS_IN_BAM_BLOCK).Take(Const.DOS_TYPE_LEN).ToArray();
        }
        public static byte[] GetDiskID(byte[] bamBlock)
        {
            return bamBlock.Skip(Const.DISK_ID_POS_IN_BAM_BLOCK).Take(Const.DISK_ID_LEN).ToArray();
        }
        public static byte[] GetDiskName(byte[] bamBlock)
        {
            return bamBlock.Skip(Const.DISK_NAME_POS_IN_BAM_BLOCK).Take(Const.DISK_NAME_LEN).ToArray();
        }
        #endregion

        #region [DISK] BLOCK / BAM-BLOCK
        public static byte[] ReadBAMBlock(string imagePathFilename)
        {
            return ReadBlock(Const.BAM_TRACK, Const.BAM_SECTOR, imagePathFilename);
        }
        #endregion

        #region [DISK] BLOCK
        public static byte[] ReadBlockChain(int track, int sector, string imagePathFilename)
        {
            int sectorCount = 0;
            int lastSectorIndex = 0;
            return ReadBlockChain2(track, sector, imagePathFilename, ref sectorCount,ref lastSectorIndex);
        }
        public static byte[] ReadBlockChain2(int track, int sector, string imagePathFilename, ref int sectorCount, ref int lastSectorIndex)
        {
            byte[] blockData;
            sectorCount = 0;
            MemoryStream ms = new MemoryStream();
            while (track > 0)
            {
                blockData = ReadBlock(track, sector, imagePathFilename);
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
        public static byte[] ReadBlock(int track, int sector, string imagePathFilename)
        {
            byte[] blockData = null;
            string FileExt = Path.GetExtension(imagePathFilename).ToUpper();
            switch (FileExt)
            {
                case ".D64":
                    blockData = DiskImageFile.ReadBlockD64(track, sector, imagePathFilename);
                    break;
                default:
                    throw new Exception(String.Format("File Extension {0} is not supported!", FileExt));
            }
            return blockData;
        }
        #endregion
    }
    public static class DiskImageFile
    {
        #region [D64 Image] (file access)
        public static byte[] ReadBlockD64(int track, int sector, string imagePathFilename)
        {
            byte[] blockData = null;
            using (BinaryReader b = new BinaryReader(File.Open(imagePathFilename, FileMode.Open, FileAccess.Read)))
            {
                // convert track/sector to byte offset in file
                long offset = GetD64Offset(track, sector);
                if (offset >= 0)
                {
                    b.BaseStream.Seek(offset, SeekOrigin.Begin);
                    blockData = b.ReadBytes(Const.BLOCK_LEN);
                }
            }
            return blockData;
        }
        public static long GetD64Offset(int track, int sector)
        {
            if ((track < Const.MIN_TRACK) || (track > Const.MAX_TRACK) || (sector < Const.MIN_SECTOR) || (sector >= Const.NUM_OF_SECTORS_PER_TRACK[track]))
            {
                return -1;
            }
            int SumOfSectorsToTrack = Const.NUM_OF_SECTORS_PER_TRACK.Take(track).Sum(); // used System.Linq;
            return (SumOfSectorsToTrack + sector) * Const.BLOCK_LEN;
        }
        #endregion

        #region Write (file access)
        public static void WriteFileBlockChain(int track, int sector, string imagePathFilename, string outPathFilename)
        {
            byte[] blocksData = DOSDisk.ReadBlockChain(track, sector, imagePathFilename);
            WriteFile(blocksData, outPathFilename);
        }
        public static void WriteCVTFile(byte[] dirEntry, string imagePathFilename, string outPathFilename)
        {
            byte[] cvtData = GEOSDisk.GetCVTFromGeosFile(dirEntry, imagePathFilename);
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
}
