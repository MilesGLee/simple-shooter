﻿using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace SimpleShooter
{
    class Bullet : Actor
    {
        private float _speed;
        private Vector2 _velocity;
        private Vector2 _direction = new Vector2 { X = 1, Y = 0}; 

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public Vector2 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }


        public Bullet(float x, float y, float speed, Vector2 direction, string name = "Bullet", string path = "")
            : base(x, y, speed, name, path)
        {
            _speed = speed;
            _direction = direction;
        }

        public override void Update(float deltaTime)
        {

            //Create a vector tht stores the move input
            Vector2 moveDirection = new Vector2();
            moveDirection = _direction;

            //caculates the veclocity 
            Velocity = moveDirection.Normalized * Speed * deltaTime;

            base.Translate(Velocity.X, Velocity.Y);
            base.Update(deltaTime);
            


            if (Position.X > 800) //Destroy the bullet if it is off screen.
                Engine._currentScene.RemoveActor(this);
            if (Position.X < 0)
                Engine._currentScene.RemoveActor(this);
            if (Position.Y > 450)
                Engine._currentScene.RemoveActor(this);
            if (Position.Y < 0)
                Engine._currentScene.RemoveActor(this);

            
        }

        public override void OnCollision(Actor actor)
        {
            if (actor is Enemy)
            {
                Engine._currentScene.RemoveActor(actor);
                Engine._currentScene.RemoveActor(this);
            }
        }
    }
}
