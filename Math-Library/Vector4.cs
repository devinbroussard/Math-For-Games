using System;
using System.Collections.Generic;
using System.Text;

namespace MathLibrary
{
    public struct Vector4
    {
        public float X;
        public float Y;
        public float Z;
        public float W;

        public Vector4(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        /// <summary>
        /// Gets the length of the vector
        /// </summary>
        public float Magnitude
        {
            get { return (float)Math.Sqrt(X * X + Y * Y + Z * Z + W * W); }
        }

        /// <summary>
        /// Property that returns the normalized value of the vector2
        /// </summary>
        public Vector4 Normalized
        {
            get
            {
                Vector4 value = this;
                return value.Normalize();
            }
        }

        /// <summary>
        /// Changes this vector to have a magnitude that is equal to one
        /// </summary>
        /// <returns>The result of the normalization
        /// Returns an empty vector if the magnitude is zero</returns>
        public Vector4 Normalize()
        {
            if (Magnitude == 0)
                return new Vector4();

            else return this /= Magnitude;
        }

        public static Vector4 CrossProduct(Vector4 lhs, Vector4 rhs)
        {
            return new Vector4(
                ((lhs.Y * rhs.Z) - (lhs.Z * rhs.Y)),
                ((lhs.Z * rhs.X) - (lhs.X * rhs.Z)),
                ((lhs.X * rhs.Y) - (lhs.Y * rhs.X)),
                0);
        }

        /// <param name="lhs">The left hand side of the operation</param>
        /// <param name="rhs">THe right hand side of the operation</param>
        /// <returns>The dot product of the first vector onto the second</returns>
        public static float DotProduct(Vector4 lhs, Vector4 rhs)
        {
            return (lhs.X * rhs.X) + (lhs.Y * rhs.Y) + (lhs.Z * rhs.Z) + (lhs.W * rhs.W);
        }

        /// <param name="lhs">Left hand side of operation</param>
        /// <param name="rhs">Right hand side of operation</param>
        /// <returns>Returns the distance between two vectors</returns>
        public static float GetDistance(Vector4 lhs, Vector4 rhs)
        {
            return (rhs - lhs).Magnitude;
        }

        /// <summary>
        /// Adds the x value and they values of the second vector to the first
        /// </summary>
        /// <param name="lhs">Left hand vector2</param>
        /// <param name="rhs">right hand vector2 that will be added to the left</param>
        /// <returns>a new vector2 with the added X and Y variables</returns>
        public static Vector4 operator +(Vector4 lhs, Vector4 rhs)
        {
            return new Vector4 { X = lhs.X + rhs.X, Y = lhs.Y + rhs.Y, Z = lhs.Z + rhs.Z, W = lhs.W + rhs.W};
        }

        /// <summary>
        /// Subtracts the x value and they values of the second vector from the first
        /// </summary>
        /// <param name="lhs">Left hand vector2</param>
        /// <param name="rhs">right hand vector2 that will be subtracted from the first</param>
        /// <returns>a new vector2 with the subtracted variables</returns>
        public static Vector4 operator -(Vector4 lhs, Vector4 rhs)
        {
            return new Vector4 { X = lhs.X - rhs.X, Y = lhs.Y - rhs.Y, Z = lhs.Z - rhs.Z, W = lhs.W - rhs.W};
        }

        /// <summary>
        /// Multiplies the vector's X and Y values by the scalar
        /// </summary>
        /// <param name="vec2">The vector that is being scaled</param>
        /// <param name="scalar">The value that the vector will be scaled by</param>
        /// <returns>A new scaled vector</returns>
        public static Vector4 operator *(Vector4 vec4, float scalar)
        {
            return new Vector4 { X = vec4.X * scalar, Y = vec4.Y * scalar, Z = vec4.Z * scalar, W = vec4.W * scalar };
        }

        /// <summary>
        /// Multiplies the vector's X and Y values by the scalar
        /// </summary>
        /// <param name="vec2">The vector that is being scaled</param>
        /// <param name="scalar">The value that the vector will be scaled by</param>
        /// <returns>A new scaled vector</returns>
        public static Vector4 operator *(float scalar, Vector4 vec4)
        {
            return new Vector4 { X = vec4.X * scalar, Y = vec4.Y * scalar, Z = vec4.Z * scalar, W = vec4.W * scalar };
        }

        public static Vector4 operator *(Vector4 vec4, Matrix4 mat4)
        {
            return new Vector4(
                ((vec4.X * mat4.M00) + (vec4.Y * mat4.M01) + (vec4.Z * mat4.M02) + (vec4.W * mat4.M03)),
                ((vec4.X * mat4.M10) + (vec4.Y * mat4.M11) + (vec4.Z * mat4.M12) + (vec4.W * mat4.M13)),
                ((vec4.X * mat4.M20) + (vec4.Y * mat4.M21) + (vec4.Z * mat4.M22) + (vec4.W * mat4.M23)),
                ((vec4.X * mat4.M30) + (vec4.Y * mat4.M31) + (vec4.Z * mat4.M32) + (vec4.W * mat4.M33)));
        }

        public static Vector4 operator *(Matrix4 mat4, Vector4 vec4)
        {
            return new Vector4(
                ((vec4.X * mat4.M00) + (vec4.Y * mat4.M01) + (vec4.Z * mat4.M02) + (vec4.W * mat4.M03)),
                ((vec4.X * mat4.M10) + (vec4.Y * mat4.M11) + (vec4.Z * mat4.M12) + (vec4.W * mat4.M13)),
                ((vec4.X * mat4.M20) + (vec4.Y * mat4.M21) + (vec4.Z * mat4.M22) + (vec4.W * mat4.M23)),
                ((vec4.X * mat4.M30) + (vec4.Y * mat4.M31) + (vec4.Z * mat4.M32) + (vec4.W * mat4.M33)));
        }

        /// <summary>
        /// Divides the vector's X and Y values by the scalar
        /// </summary>
        /// <param name="vec2">The vector that is being scaled</param>
        /// <param name="scalar">The value that the vector will be scaled by</param>
        /// <returns>A new scaled vector</returns>
        public static Vector4 operator /(Vector4 vec4, float scalar)
        {
            return new Vector4 { X = vec4.X / scalar, Y = vec4.Y / scalar, Z = vec4.Z / scalar, W = vec4.W / scalar };
        }

        /// <summary>
        /// Divides the vector's X and Y values by the scalar
        /// </summary>
        /// <param name="vec2">The vector that is being scaled</param>
        /// <param name="scalar">The value that the vector will be scaled by</param>
        /// <returns>A new scaled vector</returns>
        public static Vector4 operator /(float scalar, Vector4 vec4)
        {
            return new Vector4 { X = vec4.X / scalar, Y = vec4.Y / scalar, Z = vec4.Z / scalar, W = vec4.W / scalar };
        }

        /// <summary>
        /// Checks to see if two vectors are equal to each other
        /// </summary>
        /// <param name="lhs">The vector on the left hand side</param>
        /// <param name="rhs">The vector on the right hand side</param>
        /// <returns>True if the vectors are equal to each other</returns>
        public static bool operator ==(Vector4 lhs, Vector4 rhs)
        {
            return lhs.X == rhs.X && lhs.Y == rhs.Y && lhs.Z == rhs.Z && lhs.W == rhs.W;
        }

        /// <summary>
        /// Checks to see if two vectors are not equal to each other
        /// </summary>
        /// <param name="lhs">The vector on the left hand side</param>
        /// <param name="rhs">The vector on the right hand side</param>
        /// <returns>True if the vectors are not equal to each other</returns>
        public static bool operator !=(Vector4 lhs, Vector4 rhs)
        {
            return lhs.X != rhs.X || lhs.Y != rhs.Y || lhs.Z != rhs.Z || lhs.W != rhs.W;
        }
    }
}
