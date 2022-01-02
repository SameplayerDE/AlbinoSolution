using System;
using System.Collections.Generic;
using System.IO;
using AlbinoLibrary.ComponentSystem;
using Microsoft.Xna.Framework;

namespace AlbinoLibraryMonogame.Components.Rendering
{
    public class Animation
    {
        public string Name;
        public double Duration; //in seconds
        public double TotalTime = 0; //current time
        public double Time = 0; //current time
        public int Index = 0;
        public int StepIndex = 0;
        public List<AnimationStep> AnimationSteps = new List<AnimationStep>();
        
        public void Add(AnimationStep step)
        {
            AnimationSteps.Add(step);
            Duration += step.Duration;
        }

        public void Reset()
        {
            StepIndex = 0;
            Time = 0;
            TotalTime = 0;
        }
        
        public void Update(GameTime gameTime)
        {
            if (StepIndex >= AnimationSteps.Count)
            {
                StepIndex = 0;
                TotalTime = 0;
            }
            
            Index = AnimationSteps[StepIndex].Index;   
            var curr = AnimationSteps[StepIndex];

            if (Time >= curr.Duration)
            {
                StepIndex += 1;
                Time = 0;
            }

            Time += gameTime.ElapsedGameTime.TotalSeconds;
            TotalTime += gameTime.ElapsedGameTime.TotalSeconds;
        }
        
    }

    public class AnimationStep
    {
        public double Duration; //in seconds
        public int Index;
        //public AnimationStep Next;
    }
    
    public class AnimationController : AlbinoComponent, IUpdateable
    {
        public Animation Animation;
        public List<Animation> Animations = new List<Animation>();

        public void Play(string animation)
        {
            if (Animation.Name == animation)
            {
                return;
            }
            foreach (var anim in Animations)
            {
                if (anim.Name == animation)
                {
                    anim.Reset();
                    Animation = anim;
                    return;
                }
            }
        }

        public void Stop()
        {
            
        }
        
        public void Update(GameTime gameTime)
        {
            Animation?.Update(gameTime);
        }

        public bool Enabled { get; }
        public int UpdateOrder { get; }
        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateOrderChanged;
        public override byte[] GetBytes()
        {
            return new byte[] {0x00};
        }

        public override void FromBytes(byte[] data)
        {
            throw new NotImplementedException();
        }
    }
}