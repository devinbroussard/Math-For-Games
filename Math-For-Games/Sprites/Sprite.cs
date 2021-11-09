using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;

namespace Math_For_Games
{
    class Sprite
    {
        private Texture2D _texture;

        /// <summary>
        /// Gets the width of the texture used
        /// </summary>
        public int Width
        {
            get { return _texture.width; }
            //Can only be set inside of this class
            private set { _texture.width = value; }
        }
        
        /// <summary>
        /// Gets the height of the texture used
        /// </summary>
        public int Height
        {
            get { return _texture.height; }
            //Can only be set inside of this class
            private set { _texture.height = value; }
        }


        /// <summary>
        /// Loads the texture of the path given
        /// </summary>
        /// <param name="path">The file path of the image to use as the texture</param>
        public Sprite(string path)
        {
            _texture = Raylib.LoadTexture(path);
        }

        /// <summary>
        /// Draws the sprite using the rotation, translation, and scale of the given transform
        /// </summary>
        /// <param name="transform">The transform of the actor that owns the sprite</param>
        public void Draw(Matrix3 transform)
        {
            //Finds the scale of the sprite
            Width = (int)Math.Round(new Vector2(transform.M00, transform.M10).Magnitude);
            Height = (int)Math.Round(new Vector2(transform.M01, transform.M11).Magnitude);

            //Sets the sprites center to the fransform origin
            System.Numerics.Vector2 position = new System.Numerics.Vector2(transform.M02, transform.M12);
            System.Numerics.Vector2 forward = new System.Numerics.Vector2(transform.M00, transform.M10);
            System.Numerics.Vector2 upwards = new System.Numerics.Vector2(transform.M01, transform.M11);
            position -= System.Numerics.Vector2.Normalize(forward) * Width / 2;
            position -= System.Numerics.Vector2.Normalize(upwards) * Height / 2;

            //Find the transform rotation in radians
            float rotation = (float)Math.Atan2(transform.M10, transform.M00);

            Raylib.DrawTextureEx(_texture, position, (float)(rotation * 180 / Math.PI), 1, Color.WHITE);
        }
    }
}
