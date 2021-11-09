using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace Math_For_Games
{
    class AABBCollider : Collider
    {
        private float _width;
        private float _height;

        /// <summary>
        /// The size of the collider on the x axis
        /// </summary>
        public float Width
        {
            get { return _width; }
            set { _width = value; }
        }

        /// <summary>
        /// The size of the collider on the y axis
        /// </summary>
        public float Height
        {
            get { return _height; }
            set { _height = value; }
        }

        /// <summary>
        /// The farthest left x position of this collider
        /// </summary>
        public float Left
        {
            get { return Owner.LocalPosition.X - Width / 2; }
        }

        /// <summary>
        /// The farthest right x position of this collider
        /// </summary>
        public float Right
        {
            get { return Owner.LocalPosition.X + Width / 2; }
        }

        /// <summary>
        /// The highest y position of this collider
        /// </summary>
        public float Top
        {
            get { return Owner.LocalPosition.Y - Height / 2; }
        }

        /// <summary>
        /// The lowest y position downwards of this collider
        /// </summary>
        public float Bottom
        {
            get { return Owner.LocalPosition.Y + Height / 2; }
        }

        public AABBCollider(float width, float height, Actor owner)
            : base(owner, ColliderType.AABB)
        {
            _width = width;
            _height = height;
            owner.Collider = this;
        }

        /// <summary>
        /// Checks for collision with an AABB collider
        /// </summary>
        /// <param name="other">The other AABB collider</param>
        /// <returns>True if a collision occured</returns>
        public override bool CheckCollisionAABB(AABBCollider other)
        {
            //If the other collider is THIS collider...
            if (other.Owner == Owner)
                //...return false
                return false;

            //Return true if there is an overlap between colliders
            return other.Left <= Right &&
                other.Top <= Bottom && 
                Left <= other.Right && 
                Top <= other.Bottom;
        }

        public override bool CheckCollisionCircle(CircleCollider other)
        {
            return other.CheckCollisionAABB(this);
        }

        public override void Draw()
        {
            Raylib.DrawRectangleLines((int)Left, (int)Top, (int)Width, (int)Height, Color.PURPLE);
        }
    }
}
