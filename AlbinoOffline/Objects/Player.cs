using System;
using AlbinoLibraryMonogame.Components;
using AlbinoLibraryMonogame.Components.Rendering;
using Microsoft.Xna.Framework;

namespace AlbinoOffline.Objects
{
    public class Player : Human
    {
        public Player() : base("player")
        {
            Get<HealthComponent>().Health = 250;
            Get<SpriteRenderer>().TextureKey = "human_sprites";
            AnimationController controller = new AnimationController();

            Animation walk = new Animation();
            walk.Name = "walk";

            AnimationStep walk_step_0 = new AnimationStep();
            walk_step_0.Duration = 0.25;
            walk_step_0.Index = 0;
            
            AnimationStep walk_step_1 = new AnimationStep();
            walk_step_1.Duration = 0.25;
            walk_step_1.Index = 1;
            
            AnimationStep walk_step_2 = new AnimationStep();
            walk_step_2.Duration = 0.25;
            walk_step_2.Index = 2;
            
            AnimationStep walk_step_3 = new AnimationStep();
            walk_step_3.Duration = 0.25;
            walk_step_3.Index = 1;
            
            walk.Add(walk_step_0);
            walk.Add(walk_step_1);
            walk.Add(walk_step_2);
            walk.Add(walk_step_3);

            Animation idle = new Animation();
            idle.Name = "idle";
            
            AnimationStep idle_step_0 = new AnimationStep();
            idle_step_0.Duration = 1;
            idle_step_0.Index = 1;
            
            idle.Add(idle_step_0);
            controller.Animations.Add(walk);
            controller.Animations.Add(idle);
            
            controller.Animation = idle;

            Attach(controller);

            Attach(new ControllerComponent());

            controller.Play("walk");
        }
    }
}