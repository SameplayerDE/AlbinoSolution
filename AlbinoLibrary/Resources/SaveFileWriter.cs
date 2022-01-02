using System;
using System.IO;
using System.Reflection.Metadata;

namespace AlbinoLibrary.Resources
{
    public class SaveFileWriter
    {
        
        private BinaryWriter _writer;
        
        public SaveFileWriter(byte[] data)
        {
            BinaryWriter dataWriter = new BinaryWriter(new MemoryStream());
            dataWriter.Write(data);
        }
        
        public SaveFileWriter(FileStream file)
        {
            _writer = new BinaryWriter(file);
        }

        public void WriteInt32(int x)
        {
            var bytes = BitConverter.GetBytes(x);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }
            _writer.Write(bytes);
        }
        
        public void WriteString(string x)
        {
            WriteInt32(x.Length);
            foreach (var @char in x)
            {
                _writer.Write(Convert.ToByte(@char));
            }
        }

        public void Flush()
        {
            _writer.Flush();
        }
        
        public void Close()
        {
            _writer.Close();
        }
    }
}