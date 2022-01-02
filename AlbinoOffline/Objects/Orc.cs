using System;
using AlbinoLibraryMonogame.Components;
using AlbinoLibraryMonogame.Components.Rendering;
using AlbinoLibraryMonogame.Objects;
using Microsoft.Xna.Framework;

namespace AlbinoOffline.Objects
{
    public class Orc : UpdateableGameObject
    {
        public Orc(string name = "") : base(name)
        {
            Attach(new Transform(100, 100));
            Attach(new HealthComponent());
            Attach(new SpriteRenderer());
            Attach(new HealthRenderer());
            
            Attach(new PatrolWalker());
            
            Get<PatrolWalker>().AddPatrolPoint(new PatrolPoint() {Destination = new Vector2(new Random().Next(100, 550), new Random().Next(100, 550))});
            Get<PatrolWalker>().AddPatrolPoint(new PatrolPoint() {Destination = new Vector2(new Random().Next(100, 550), new Random().Next(100, 550))});
            Get<PatrolWalker>().AddPatrolPoint(new PatrolPoint() {Destination = new Vector2(new Random().Next(100, 550), new Random().Next(100, 550))});
        }
    }
}