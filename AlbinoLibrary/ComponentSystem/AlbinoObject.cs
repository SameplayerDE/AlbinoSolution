using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using AlbinoLibrary.Resources;

namespace AlbinoLibrary.ComponentSystem
{
    
    public class AlbinoObject
    {
        public Guid Guid = Guid.NewGuid();
        public string Name;
        public string Tag = "Untagged";

        public AlbinoObject? Parent = null;
        
        public AlbinoObject(string name = "")
        {
            Name = name == "" ? Guid.ToString() : name;
        }
        
        private readonly HashSet<AlbinoComponent> _components = new HashSet<AlbinoComponent>();

        public HashSet<AlbinoComponent> Components
        {
            get { return _components; }
        }


        public bool Attach(AlbinoComponent component)
        {
            component.Object = this;
            return _components.Add(component);
        }
        
        public void Attach(HashSet<AlbinoComponent> components)
        {
            foreach (var component in components)
            {
                component.Object = this;
                _components.Add(component);
            }
            
        }

        public T? Get<T>() where T : AlbinoComponent
        {
            foreach (AlbinoComponent component in this._components)
            {
                if (component is T obj)
                    return obj;
            }
            return null;
            //return default (T);
        }

        public bool Has<T>() where T : AlbinoComponent
        {
            foreach (AlbinoComponent component in this._components)
            {
                if (component is T obj)
                    return true;
            }
            return false;
            //return default (T);
        }
        
        public void Detach(AlbinoComponent component)
        {
            this._components.Remove(component);
        }

        public byte[] GetBytes()
        {
            
            
            var writer = new SaveFileStream();
            
            writer.WriteGuid(Guid);
            writer.WriteString(Name);
            writer.WriteString(Tag);
            
            /*List<byte> data = new List<byte>();
            var guidBytes = Guid.ToByteArray();
            var nameBytes = Encoding.ASCII.GetBytes(Name);
            var tagBytes = Encoding.ASCII.GetBytes(Tag);
            
            //GUID
            data.AddRange(BitConverter.GetBytes(guidBytes.Length));
            data.AddRange(guidBytes);
           
            
            //Name
            data.AddRange(BitConverter.GetBytes(nameBytes.Length));
            data.AddRange(nameBytes);
            
            //Tag
            data.AddRange(BitConverter.GetBytes(tagBytes.Length));
            data.AddRange(tagBytes);
*/
            lock (_components)
            {
                //Component Count
                writer.WriteInt32(_components.Count);
                
                foreach (var component in _components)
                {
                    writer.Write(component.GetBytes());
                }
            }
            return writer.ToFinalArray();
        }
        
        public void FromBytes(byte[] data)
        {
            /*BinaryWriter dataWriter = new BinaryWriter(new MemoryStream());
            dataWriter.Write(data);
            BinaryReader reader = new BinaryReader(dataWriter.BaseStream);
            reader.BaseStream.Position = 0;
            var byteAmount = BitConverter.ToInt32(reader.ReadBytes(4), 0);
            Console.Write(byteAmount);
            //byte[] data = new byte[byteAmount];
            
            /*for (int i = 0; i < byteAmount; i++)
            {
                data[i] = (byte) stream.ReadByte();
            }


            //GUID
            var guidLenght = BitConverter.ToInt32(reader.ReadBytes(4), 0);
            var guidBytes = reader.ReadBytes(guidLenght);

            //Name
            var nameLenght = BitConverter.ToInt32(reader.ReadBytes(4), 0);
            var nameBytes = reader.ReadBytes(nameLenght);
            
            //Tag
            var tagLenght = BitConverter.ToInt32(reader.ReadBytes(4), 0);
            var tagBytes = reader.ReadBytes(tagLenght);

            //Comp Count
            var compAmount = BitConverter.ToInt32(reader.ReadBytes(4), 0);

            var readComps = new Dictionary<byte[], byte[]>();

            for (int i = 0; i < compAmount; i++)
            {
                //Byte Count
                var compDataByteLenght = BitConverter.ToInt32(reader.ReadBytes(4), 0);
                if (compDataByteLenght == 0x00) continue;
                
                //Name
                var compNameByteLenght = BitConverter.ToInt32(reader.ReadBytes(4), 0);
                var comNameBytes = reader.ReadBytes(compNameByteLenght);

                
                
                //Not Final, just to read all bytes
                var bytesLeft = compDataByteLenght - (compNameByteLenght + 1);
                var rest = reader.ReadBytes(bytesLeft);
                
                readComps.Add(comNameBytes, rest);
                
            }
            
            dataWriter.Close();
            reader.Close();

            var s = BitConverter.ToString(guidBytes);
            Guid = new Guid(guidBytes);
            Name = Convert.ToString(nameBytes) ?? Guid.ToString();
            Tag = Convert.ToString(tagBytes) ?? Tag;

            foreach (var comp in readComps)
            {
                //var className = BitConverter.ToString(comp.Key);
                //Type type = Type.GetType(BitConverter.ToString(comp.Key)); //target type
               // object o = Activator.CreateInstance(type); // an instance of target type
               // AlbinoComponent your = (AlbinoComponent)o;
                
               // BinaryWriter binWriter = new BinaryWriter(new MemoryStream());
               // //binWriter.Write(comp.Value);
               // BinaryReader compReader = new BinaryReader(binWriter.BaseStream);
              //  compReader.BaseStream.Position = 0;
               // your.FromBytes(compReader); // Load Data
               // binWriter.Close();
               // compReader.Close();
            }*/

            var reader = new SaveFileStream(new MemoryStream(data));
            
            Guid = reader.ReadGuid();
            Name = reader.ReadString() ?? Guid.ToString();
            Tag = reader.ReadString() ?? Tag;
            
            //Comp Count
            var compAmount = reader.ReadInt32();
            var readComps = new Dictionary<string, byte[]>();
            
            for (int i = 0; i < compAmount; i++)
            {
                var byteCount = reader.ReadInt32();
                
                if (byteCount == 0x00) continue;
                
                var compName = reader.ReadString();
                Console.WriteLine(compName);

                //Not Final, just to read all bytes
                //var bytesLeft = byteCount - compName.Length;
                //var rest = reader.Read(bytesLeft);
                //readComps.Add(compName, rest);
            }
            
        }
        
    }
}