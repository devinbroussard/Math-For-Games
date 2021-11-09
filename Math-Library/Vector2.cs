using System;

namespace MathLibrary
{
    public struct Vector2
    {
        public float X;
        public float Y;

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Gets the length of the vector
        /// </summary>
        public float Magnitude
        {
            get { return (float)Math.Sqrt(X * X + Y * Y); }
        }

        /// <summary>
        /// Property that returns the normalized value of the vector2
        /// </summary>
        public Vector2 Normalized
        {
            get
            {
                Vector2 value = this;
                return value.Normalize();
            }
        }

        /// <summary>
        /// Changes this vector to have a magnitude that is equal to one
        /// </summary>
        /// <returns>The result of the normalization
        /// Returns an empty vector if the magnitude is zero</returns>
        public Vector2 Normalize()
        {
            if (Magnitude == 0)
                return new Vector2();

            else return this /= Magnitude;
        }

        /// <param name="lhs">The left hand side of the operation</param>
        /// <param name="rhs">THe right hand side of the operation</param>
        /// <returns>The dot product of the first vector onto the second</returns>
        public static float GetDotProduct(Vector2 lhs, Vector2 rhs)
        {
            return (lhs.X * rhs.X) + (lhs.Y * rhs.Y);
        }

        /// <param name="lhs">Left hand side of operation</param>
        /// <param name="rhs">Right hand side of operation</param>
        /// <returns>Returns the distance between two vectors</returns>
        public static float GetDistance(Vector2 lhs, Vector2 rhs)
        {
            return (rhs - lhs).Magnitude;
        }

        /// <summary>
        /// Adds the x value and they values of the second vector to the first
        /// </summary>
        /// <param name="lhs">Left hand vector2</param>
        /// <param name="rhs">right hand vector2 that will be added to the left</param>
        /// <returns>a new vector2 with the added X and Y variables</returns>
        public static Vector2 operator +(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2 { X = lhs.X + rhs.X, Y = lhs.Y + rhs.Y };
        }

        /// <summary>
        /// Subtracts the x value and they values of the second vector from the first
        /// </summary>
        /// <param name="lhs">Left hand vector2</param>
        /// <param name="rhs">right hand vector2 that will be subtracted from the first</param>
        /// <returns>a new vector2 with the subtracted variables</returns>
        public static Vector2 operator -(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2 { X = lhs.X - rhs.X, Y = lhs.Y - rhs.Y };
        }

        /// <summary>
        /// Multiplies the vector's X and Y values by the scalar
        /// </summary>
        /// <param name="vec2">The vector that is being scaled</param>
        /// <param name="scalar">The value that the vector will be scaled by</param>
        /// <returns>A new scaled vector</returns>
        public static Vector2 operator *(Vector2 vec2, float scalar)
        {
            return new Vector2 { X = vec2.X * scalar, Y = vec2.Y * scalar };
        }

        /// <summary>
        /// Divides the vector's X and Y values by the scalar
        /// </summary>
        /// <param name="vec2">The vector that is being scaled</param>
        /// <param name="scalar">The value that the vector will be scaled by</param>
        /// <returns>A new scaled vector</returns>
        public static Vector2 operator /(Vector2 vec2, float scalar)
        {
            return new Vector2 { X = vec2.X / scalar, Y = vec2.Y / scalar };
        }

        /// <summary>
        /// Checks to see if two vectors are equal to each other
        /// </summary>
        /// <param name="lhs">The vector on the left hand side</param>
        /// <param name="rhs">The vector on the right hand side</param>
        /// <returns>True if the vectors are equal to each other</returns>
        public static bool operator ==(Vector2 lhs, Vector2 rhs)
        {
            return lhs.X == rhs.X && lhs.Y == rhs.Y;
        }

        /// <summary>
        /// Checks to see if two vectors are not equal to each other
        /// </summary>
        /// <param name="lhs">The vector on the left hand side</param>
        /// <param name="rhs">The vector on the right hand side</param>
        /// <returns>True if the vectors are not equal to each other</returns>
        public static bool operator !=(Vector2 lhs, Vector2 rhs)
        {
            return lhs.X != rhs.X || lhs.Y != rhs.Y;
        }
    }


}
