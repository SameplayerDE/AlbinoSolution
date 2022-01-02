using System;
using System.IO;
using System.Text;
using System.Threading;

namespace AlbinoLibrary.Resources
{
    public class SaveFileStream : Stream
    {
        public Stream Stream { get; set; }
        public CancellationTokenSource CancelationToken { get; private set; }

        public SaveFileStream() : this(new MemoryStream()) { }

        public SaveFileStream(Stream stream)
        {
            Stream = stream;
            CancelationToken = new CancellationTokenSource();
        }
        
        public override bool CanRead => Stream.CanRead;

        public override bool CanSeek => Stream.CanSeek;

        public override bool CanWrite => Stream.CanWrite;

        public override long Length => Stream.Length;

        public override long Position { get => Stream.Position; set => Stream.Position = value; }
        
        public override void Flush()
        {
            Stream.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return Stream.Read(buffer, offset, count);
        }
        
        public override long Seek(long offset, SeekOrigin origin)
        {
            if (!Stream.CanSeek)
            {
                throw new NotSupportedException();
            }
            return Stream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            Stream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            Stream.Write(buffer, offset, count);
        }
        
        public byte[] Read(int count)
        {
            if (Stream is MemoryStream)
            {
                byte[] data = new byte[count];
                Read(data, 0, count);
                return data;
            }

            int read = 0;

            byte[] buffer = new byte[count];
            while (read < buffer.Length && !CancelationToken.IsCancellationRequested)
            {
                int readBytes = Read(buffer, read, count - read);
                if (readBytes < 0) //No data read?
                {
                    break;
                }

                read += readBytes;

                if (CancelationToken.IsCancellationRequested)
                {
                    throw new ObjectDisposedException("");
                }
            }

            return buffer;
        }
        
        public void WriteFloat(float x)
        {
            var bytes = BitConverter.GetBytes(x);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }
            Write(bytes);
        }
        
        public float ReadFloat()
        {
            var bytes = Read(4);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }
            return BitConverter.ToSingle(bytes);
        }
        
        public int ReadInt32()
        {
            var bytes = Read(4);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }
            return BitConverter.ToInt32(bytes);
        }

        public sbyte ReadSByte()
        {
            sbyte value = (sbyte)Stream.ReadByte();
            return value;
        }

        public byte ReadUByte()
        {
            byte value = (byte)Stream.ReadByte();
            return value;
        }
        
        public string ReadString()
        {
            var length = ReadInt32();
            byte[] chars = new byte[length];
            for (var i = 0; i < length; i++)
            {
                chars[i] = ReadUByte();
            }
            return Encoding.Default.GetString(chars);
        }
        
        public void Write(byte[] value)
        {
            foreach (byte @byte in value)
            {
                Stream.WriteByte(@byte);
            }
        }
        
        public void WriteInt32(int x)
        {
            var bytes = BitConverter.GetBytes(x);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }
            Write(bytes);
        }
        
        public void WriteString(string x)
        {
            WriteInt32(x.Length);
            foreach (var @char in x)
            {
                WriteByte(Convert.ToByte(@char));
            }
        }
        
        public void WriteGuid(Guid guid)
        {
            Write(guid.ToByteArray());
        }
        
        public Guid ReadGuid()
        {
            var bytes = Read(16);
            return new Guid(bytes);
        }
        
        public byte[] GetBytes()
        {
            return ReadFully(Stream);
        }
        
        public static byte[] ReadFully(Stream input)
        {
            if (input is MemoryStream)
            {
                return ((MemoryStream)input).ToArray();
            }
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }
        
        public byte[] ToFinalArray()
        {
            if (Stream is MemoryStream)
            {
                Stream.Position = 0;
                MemoryStream stream = Stream as MemoryStream;
                byte[] data = stream.ToArray();
                int count = data.Length;
                WriteInt32(count); // Write the length
                Write(data, 0, count);
                return stream.ToArray();
            }
            return null;
        }
        
    }
}