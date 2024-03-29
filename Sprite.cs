﻿using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace SimpleShooter
{
    class Sprite
    {
        private Texture2D _texture;

        //Width of the loaded texture
        public int Width 
        {
            get { return _texture.width; }
            private set { _texture.width = value; }
        }
        //Height of the loaded texture
        public int Height 
        {
            get { return _texture.height; }
            private set { _texture.height = value; }
        }

        //sets the texture with the given folder path.
        public Sprite(string path) 
        {
            _texture = Raylib.LoadTexture(path);
        }

        //Draws the sprite using the rotation, translation and scale of the given transform.
        public void Draw(Matrix3 transform) 
        {
            //Finds the scale of the sprite.
            Width = (int)Math.Round(new Vector2(transform.M00, transform.M10).Magnitude);
            Height = (int)Math.Round(new Vector2(transform.M01, transform.M11).Magnitude);

            //Sets the sprites center to the transform origin
            System.Numerics.Vector2 position = new System.Numerics.Vector2(transform.M02, transform.M12);
            System.Numerics.Vector2 forward = new System.Numerics.Vector2(transform.M00, transform.M10);
            System.Numerics.Vector2 up = new System.Numerics.Vector2(transform.M01, transform.M11);
            position -= System.Numerics.Vector2.Normalize(forward) * Width / 2;
            position -= System.Numerics.Vector2.Normalize(up) * Height / 2;

            //Finds the transform rotation in radians
            float rotation = (float)Math.Atan2(transform.M10, transform.M00);

            //Draw the sprite
            Raylib.DrawTextureEx(_texture, position, (float)(rotation * 180 / Math.PI), 1, Color.WHITE);
        }
    }
}
