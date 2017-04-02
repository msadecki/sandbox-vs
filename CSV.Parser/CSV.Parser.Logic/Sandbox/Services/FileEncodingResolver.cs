using System.IO;
using System.Text;
using CSV.Parser.Logic.Abstractions.Interfaces.Sandbox.Services;

namespace CSV.Parser.Logic.Sandbox.Services
{
    /// <summary>
    /// http://stackoverflow.com/questions/3825390/effective-way-to-find-any-files-encoding
    /// </summary>
    public class FileEncodingResolver : IFileEncodingResolver
    {
        public Encoding GetEncoding(string filename, Encoding defaultEncoding)
        {
            return AnalyzePreamble(ReadPreamble(filename)) ?? defaultEncoding;
        }

        /// <summary>
        /// Read the BOM
        /// </summary>
        private byte[] ReadPreamble(string filename)
        {
            var bom = new byte[4];

            using (var stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                stream.Read(bom, 0, 4);
            }

            return bom;
        }

        /// <summary>
        /// Analyze the BOM
        /// </summary>
        private Encoding AnalyzePreamble(byte[] bom)
        {
            if (bom[0] == 0x2b &&
                bom[1] == 0x2f &&
                bom[2] == 0x76)
            {
                return Encoding.UTF7;
            }

            if (bom[0] == 0xef &&
                bom[1] == 0xbb &&
                bom[2] == 0xbf)
            {
                return Encoding.UTF8;
            }

            if (bom[0] == 0xff &&
                bom[1] == 0xfe)
            {
                //UTF-16LE
                return Encoding.Unicode;
            }

            if (bom[0] == 0xfe &&
                bom[1] == 0xff)
            {
                //UTF-16BE
                return Encoding.BigEndianUnicode;
            }

            if (bom[0] == 0 &&
                bom[1] == 0 &&
                bom[2] == 0xfe &&
                bom[3] == 0xff)
            {
                return Encoding.UTF32;
            }

            return null;
        }
    }
}