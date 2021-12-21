using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Bridge.Commons.Compress
{
    /// <summary>
    ///     Gzip Util
    /// </summary>
    public class GzipUtil
    {
        #region Privates

        /// <summary>
        ///     Converte dados em stream
        /// </summary>
        /// <param name="data">string de dados</param>
        /// <returns></returns>
        private static Stream GetStream(string data)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(data);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        #endregion

        #region Compress

        /// <summary>
        ///     Comprime para memory stream
        /// </summary>
        /// <param name="data">Array de bytes</param>
        /// <returns></returns>
        public static MemoryStream CompressToMemoryStream(byte[] data)
        {
            var memory = new MemoryStream();
            using (var gzip = new GZipStream(memory, CompressionMode.Compress, false))
            {
                gzip.Write(data, 0, data.Length);
            }

            return memory;
        }

        /// <summary>
        ///     Comprime para memory stream
        /// </summary>
        /// <param name="data">string de dados</param>
        /// <returns></returns>
        public static MemoryStream CompressToMemoryStream(string data)
        {
            return CompressToMemoryStream(Encoding.UTF8.GetBytes(data));
        }

        /// <summary>
        ///     Comprime para array de bytes
        /// </summary>
        /// <param name="data">Array de bytes</param>
        /// <returns></returns>
        public static byte[] Compress(byte[] data)
        {
            using (var memory = CompressToMemoryStream(data))
            {
                return memory.ToArray();
            }
        }

        /// <summary>
        ///     Comprime para array de bytes
        /// </summary>
        /// <param name="data">string de dados</param>
        /// <returns></returns>
        public static byte[] Compress(string data)
        {
            return Compress(Encoding.UTF8.GetBytes(data));
        }

        /// <summary>
        ///     Comprime para string
        /// </summary>
        /// <param name="data">string de dados</param>
        /// <returns></returns>
        public static string CompressToString(string data)
        {
            var binary = Compress(data);
            var result = Convert.ToBase64String(binary);
            return result;
        }

        #endregion

        #region Decompress

        /// <summary>
        ///     Descomprime para array de bytes
        /// </summary>
        /// <param name="data">string de dados comprimidos</param>
        /// <returns></returns>
        public static byte[] Decompress(string data)
        {
            byte[] result = null;
            var binary = Convert.FromBase64String(data);

            result = Decompress(binary);

            return result;
        }

        /// <summary>
        ///     Descomprime para array de bytes
        /// </summary>
        /// <param name="data">Array de bytes comprimidos</param>
        /// <returns></returns>
        public static byte[] Decompress(byte[] data)
        {
            return DecompressMemoryStream(new MemoryStream(data));
        }

        /// <summary>
        ///     Descomprime para array de bytes
        /// </summary>
        /// <param name="stream">MemoryStream de dados comprimidos</param>
        /// <returns></returns>
        public static byte[] DecompressMemoryStream(MemoryStream stream)
        {
            byte[] result = null;

            using (var gz = new GZipStream(stream, CompressionMode.Decompress, false))
            {
                using (var resultStream = new MemoryStream())
                {
                    gz.CopyTo(resultStream);
                    result = resultStream.ToArray();
                }
            }

            return result;
        }

        /// <summary>
        ///     Descomprime para string
        /// </summary>
        /// <param name="stream">MemoryStream de dados comprimidos</param>
        /// <returns></returns>
        public static string DecompressMemoryStreamToString(MemoryStream stream)
        {
            return Encoding.UTF8.GetString(DecompressMemoryStream(stream));
        }

        /// <summary>
        ///     Descomprime para string
        /// </summary>
        /// <param name="data">string de dados comprimidos</param>
        /// <returns></returns>
        public static string DecompressToString(string data)
        {
            return Encoding.UTF8.GetString(Decompress(data));
        }

        #endregion
    }
}