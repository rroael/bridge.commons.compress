using System.IO;
using MsgPack.Serialization;

namespace Bridge.Commons.Compress
{
    /// <summary>
    ///     Msg Pack Util
    /// </summary>
    public class MsgPackUtil
    {
        /// <summary>
        ///     Serializa objetos genéricos em array de bytes
        /// </summary>
        /// <typeparam name="T">Objeto genérico</typeparam>
        /// <param name="obj">Instância do objeto</param>
        /// <returns>Array de bytes</returns>
        public static byte[] Serialize<T>(T obj) where T : class
        {
            if (obj == null)
                return null;

            var serializer = SerializationContext.Default.GetSerializer<T>();

            using (var ms = new MemoryStream())
            {
                serializer.Pack(ms, obj);
                return ms.ToArray();
            }
        }

        /// <summary>
        ///     Serializa array de objetos genéricos em array de bytes
        /// </summary>
        /// <param name="obj">Array de objetos genéricos</param>
        /// <typeparam name="T">Tipo genérico</typeparam>
        /// <returns>Array de bytes</returns>
        public static byte[] Serialize<T>(T[] obj) where T : class
        {
            if (obj == null)
                return null;

            var serializer = SerializationContext.Default.GetSerializer<T>();

            using (var ms = new MemoryStream())
            {
                serializer.Pack(ms, obj);
                return ms.ToArray();
            }
        }

        /// <summary>
        ///     Desserializa array de bytes em objeto genérico
        /// </summary>
        /// <typeparam name="T">Objeto genérico</typeparam>
        /// <param name="buffer">Array de bytes</param>
        /// <returns>Objeto genérico</returns>
        public static T Deserialize<T>(byte[] buffer) where T : class
        {
            if (buffer == null)
                return null;

            var serializer = SerializationContext.Default.GetSerializer<T>();

            using (var ms = new MemoryStream(buffer))
            {
                return serializer.Unpack(ms);
            }
        }
    }
}