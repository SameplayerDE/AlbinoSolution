using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Text;
using AlbinoLibrary.ComponentSystem;
using AlbinoLibrary.Resources;
using Microsoft.Xna.Framework;

namespace AlbinoLibraryMonogame.Components
{
    [Serializable]
    public sealed class Transform : AlbinoComponent
    {
        public Vector3 Position;
        public Vector3 Scale = Vector3.One;
        public Quaternion Rotation = Quaternion.Identity;

        public Vector2 PositionVector2 => new Vector2(Position.X, Position.Y);
        public Vector2 ScaleVector2 => new Vector2(Scale.X, Scale.Y);

        public float X
        {
            get => Position.X;
            set => Position.X = value;
        }
        public float Y
        {
            get => Position.Y;
            set => Position.Y = value;
        }
        public float Z
        {
            get => Position.Z;
            set => Position.Z = value;
        }

        public Transform(float x = 0f, float y = 0f, float z = 0f)
        {
            Position = new Vector3(x, y, z);
        }
        
        public void Teleport(float x, float y, float z)
        {
            Position.X = x;
            Position.Y = y;
            Position.Z = z;
        }
        
        public void Teleport(Vector3 v)
        {
            Teleport(v.X, v.Y, v.Z);
        }
        
        public void Teleport(float x, float y)
        {
            Teleport(x, y, Position.Z);
        }
        
        public void Teleport(Vector2 v)
        {
            Teleport(v.X, v.Y);
        }

        public void Move(float x, float y, float z)
        {
            Position.X += x;
            Position.Y += y;
            Position.Z += z;
        }
        
        public void Move(Vector3 v)
        {
            Move(v.X, v.Y, v.Z);
        }
        
        public void Move(float x, float y)
        {
            Move(x, y, 0);
        }
        
        public void Move(Vector2 v)
        {
            Move(v.X, v.Y);
        }
        
        public void MoveTo(float x, float y, float z, float steps)
        {
            var destination = new Vector3(x, y, z);
            var stepVector = Vector3.Subtract(destination, Position) / steps;
            Move(stepVector);
        }
        
        public void MoveTo(Vector3 v, float steps)
        {
            MoveTo(v.X, v.Y, v.Z, steps);
        }
        
        public void MoveTo(Vector2 v, float steps)
        {
            MoveTo(v.X, v.Y, Position.Z, steps);
        }

        public override byte[] GetBytes()
        { 
            var writer = new SaveFileStream();

            writer.WriteString(GetType().FullName ?? GetType().Name);

            writer.WriteFloat(X); 
            writer.WriteFloat(Y); 
            writer.WriteFloat(Z);

            writer.WriteFloat(Scale.X); 
            writer.WriteFloat(Scale.Y); 
            writer.WriteFloat(Scale.Z);

            writer.WriteFloat(Rotation.X); 
            writer.WriteFloat(Rotation.Y); 
            writer.WriteFloat(Rotation.Z);
            writer.WriteFloat(Rotation.W);

            return writer.ToFinalArray();
        }

        public override void FromBytes(byte[] data)
        {
            var reader = new SaveFileStream(new MemoryStream(data));

            X = reader.ReadFloat();
            Y = reader.ReadFloat();
            Z = reader.ReadFloat();
            
            Scale.X = reader.ReadFloat();
            Scale.Y = reader.ReadFloat();
            Scale.Z = reader.ReadFloat();
            
            Rotation.X = reader.ReadFloat();
            Rotation.Y = reader.ReadFloat();
            Rotation.Z = reader.ReadFloat();
            Rotation.W = reader.ReadFloat();
        }
    }
}