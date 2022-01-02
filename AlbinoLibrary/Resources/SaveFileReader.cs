using System;
using System.IO;
using System.Reflection.Metadata;
using System.Text;

namespace AlbinoLibrary.Resources
{
    public class SaveFileReader
    {
        
        private BinaryReader _reader;
        
        public SaveFileReader(byte[] data)
        {
            BinaryWriter dataWriter = new BinaryWriter(new MemoryStream());
            dataWriter.Write(data);
            _reader = new BinaryReader(dataWriter.BaseStream);
            _reader.BaseStream.Position = 0;
        }
        
        public SaveFileReader(FileStream file)
        {
            _reader = new BinaryReader(file);
        }

        public int ReadInt32()
        {
            var bytes = _reader.ReadBytes(4);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }
            return BitConverter.ToInt32(bytes);
        }

        public string ReadString()
        {
            var length = ReadInt32();
            byte[] chars = new byte[length];
            for (var i = 0; i < length; i++)
            {
                chars[i] = _reader.ReadByte();
            }
            return Encoding.Default.GetString(chars);
        }

        public void Close()
        {
            _reader.Close();
        }

    }
}