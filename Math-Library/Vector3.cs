using System;
using System.Collections.Generic;
using System.Text;

namespace MathLibrary
{
    public struct Vector3
    {
        public float X;
        public float Y;
        public float Z;

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Gets the length of the vector
        /// </summary>
        public float Magnitude
        {
            get { return (float)Math.Sqrt(X * X + Y * Y + Z * Z); }
        }

        /// <summary>
        /// Property that returns the normalized value of the vector2
        /// </summary>
        public Vector3 Normalized
        {
            get
            {
                Vector3 value = this;
                return value.Normalize();
            }
        }

        /// <summary>
        /// Changes this vector to have a magnitude that is equal to one
        /// </summary>
        /// <returns>The result of the normalization
        /// Returns an empty vector if the magnitude is zero</returns>
        public Vector3 Normalize()
        {
            if (Magnitude == 0)
                return new Vector3();

            else return this /= Magnitude;
        }

        /// <param name="lhs">The left hand side of the operation</param>
        /// <param name="rhs">THe right hand side of the operation</param>
        /// <returns>The dot product of the first vector onto the second</returns>
        public static float DotProduct(Vector3 lhs, Vector3 rhs)
        {
            return (lhs.X * rhs.X) + (lhs.Y * rhs.Y) + (lhs.Z * rhs.Z);
        }

        public static Vector3 CrossProduct(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(
                ((lhs.Y * rhs.Z) - (lhs.Z * rhs.Y)),
                ((lhs.Z * rhs.X) - (lhs.X * rhs.Z)),
                ((lhs.X * rhs.Y) - (lhs.Y * rhs.X)));
        }

        /// <param name="lhs">Left hand side of operation</param>
        /// <param name="rhs">Right hand side of operation</param>
        /// <returns>Returns the distance between two vectors</returns>
        public static float GetDistance(Vector3 lhs, Vector3 rhs)
        {
            return (rhs - lhs).Magnitude;
        }

        



        /// <summary>
        /// Adds the x value and they values of the second vector to the first
        /// </summary>
        /// <param name="lhs">Left hand vector2</param>
        /// <param name="rhs">right hand vector2 that will be added to the left</param>
        /// <returns>a new vector2 with the added X and Y variables</returns>
        public static Vector3 operator +(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3 { X = lhs.X + rhs.X, Y = lhs.Y + rhs.Y, Z = lhs.Z + rhs.Z };
        }

        /// <summary>
        /// Subtracts the x value and they values of the second vector from the first
        /// </summary>
        /// <param name="lhs">Left hand vector2</param>
        /// <param name="rhs">right hand vector2 that will be subtracted from the first</param>
        /// <returns>a new vector2 with the subtracted variables</returns>
        public static Vector3 operator -(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3 { X = lhs.X - rhs.X, Y = lhs.Y - rhs.Y, Z = lhs.Z - rhs.Z };
        }

        /// <summary>
        /// Multiplies the vector's X and Y values by the scalar
        /// </summary>
        /// <param name="vec2">The vector that is being scaled</param>
        /// <param name="scalar">The value that the vector will be scaled by</param>
        /// <returns>A new scaled vector</returns>
        public static Vector3 operator *(Vector3 vec3, float scalar)
        {
            return new Vector3 { X = vec3.X * scalar, Y = vec3.Y * scalar, Z = vec3.Z * scalar };
        }

        /// <summary>
        /// Multiplies the vector's X and Y values by the scalar
        /// </summary>
        /// <param name="vec2">The vector that is being scaled</param>
        /// <param name="scalar">The value that the vector will be scaled by</param>
        /// <returns>A new scaled vector</returns>
        public static Vector3 operator *(float scalar, Vector3 vec3)
        {
            return new Vector3 { X = vec3.X * scalar, Y = vec3.Y * scalar, Z = vec3.Z * scalar };
        }

        /// <summary>
        /// Divides the vector's X and Y values by the scalar
        /// </summary>
        /// <param name="vec2">The vector that is being scaled</param>
        /// <param name="scalar">The value that the vector will be scaled by</param>
        /// <returns>A new scaled vector</returns>
        public static Vector3 operator /(Vector3 vec3, float scalar)
        {
            return new Vector3 { X = vec3.X / scalar, Y = vec3.Y / scalar, Z = vec3.Z / scalar };
        }

        /// <summary>
        /// Divides the vector's X and Y values by the scalar
        /// </summary>
        /// <param name="vec2">The vector that is being scaled</param>
        /// <param name="scalar">The value that the vector will be scaled by</param>
        /// <returns>A new scaled vector</returns>
        public static Vector3 operator /(float scalar, Vector3 vec3)
        {
            return new Vector3 { X = vec3.X / scalar, Y = vec3.Y / scalar, Z = vec3.Z / scalar };
        }

        /// <summary>
        /// Checks to see if two vectors are equal to each other
        /// </summary>
        /// <param name="lhs">The vector on the left hand side</param>
        /// <param name="rhs">The vector on the right hand side</param>
        /// <returns>True if the vectors are equal to each other</returns>
        public static bool operator ==(Vector3 lhs, Vector3 rhs)
        {
            return lhs.X == rhs.X && lhs.Y == rhs.Y && lhs.Z == rhs.Z;
        }

        /// <summary>
        /// Checks to see if two vectors are not equal to each other
        /// </summary>
        /// <param name="lhs">The vector on the left hand side</param>
        /// <param name="rhs">The vector on the right hand side</param>
        /// <returns>True if the vectors are not equal to each other</returns>
        public static bool operator !=(Vector3 lhs, Vector3 rhs)
        {
            return lhs.X != rhs.X || lhs.Y != rhs.Y || lhs.Z != rhs.Z;
        }
        public static Vector3 operator *(Vector3 vec3, Matrix3 mat3)
        {
            return new Vector3(
                ((vec3.X * mat3.M00) + (vec3.Y * mat3.M01) + (vec3.Z * mat3.M02)),
                ((vec3.X * mat3.M10) + (vec3.Y * mat3.M11) + (vec3.Z * mat3.M12)),
                ((vec3.X * mat3.M20) + (vec3.Y * mat3.M21) + (vec3.Z * mat3.M22)));
        }

    
        public static Vector3 operator *(Matrix3 mat3, Vector3 vec3)
        {
            return new Vector3(
                ((vec3.X * mat3.M00) + (vec3.Y * mat3.M01) + (vec3.Z * mat3.M02)),
                ((vec3.X * mat3.M10) + (vec3.Y * mat3.M11) + (vec3.Z * mat3.M12)),
                ((vec3.X * mat3.M20) + (vec3.Y * mat3.M21) + (vec3.Z * mat3.M22)));
        }
    }
}
