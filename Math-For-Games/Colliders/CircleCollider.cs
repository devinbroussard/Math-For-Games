using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace Math_For_Games
{
    class CircleCollider : Collider
    {
        private float _collisionRadius;

        public float CollisionRadius
        {
            get { return _collisionRadius; }
            set { _collisionRadius = value; }
        }

        public CircleCollider(float collisionRadius, Actor owner) 
            : base(owner, ColliderType.CIRCLE)
        {
            _collisionRadius = collisionRadius;
            owner.Collider = this;
        }

        /// <summary>
        /// Checks for collision with another circle collider
        /// </summary>
        /// <param name="other">The circle collider that you are checking for a collision with</param>
        /// <returns>True if a collision occured</returns>
        public override bool CheckCollisionCircle(CircleCollider other)
        {
            //If the collider is THIS collider...
            if (other.Owner == Owner)
                //...returns false
                return false;

            //Gets the distance between the two collider owners
            float distance = Vector3.GetDistance(other.Owner.LocalPosition, Owner.LocalPosition);
            //Gets the distance of the combined radii of the two circles
            float combinedRadii = other.CollisionRadius + CollisionRadius;

            //Returns true if the distance between the two circles is less than or equal to the distance of the combined radii
            return distance <= combinedRadii;
        }

        /// <summary>
        /// Checks for collision with an AABB collider
        /// </summary>
        /// <param name="other">The AABB collider to check for a collision with</param>
        /// <returns>True if a collision has occured</returns>
        public override bool CheckCollisionAABB(AABBCollider other)
        {
            //If the other collider's owner is THIS collider's owner...
            if (other.Owner == Owner)
                //...returns false
                return false;

            //Creates a vector that represents the direction of the circle FROM the AABB collider
            Vector3 direction = Owner.LocalPosition - other.Owner.LocalPosition;

            //Clamps the X variable to the farthest x variables of the AABB
            direction.X = Math.Clamp(direction.X, -other.Width / 2, other.Width / 2);
            //Clamps the Y variable to the farthest y variables of the AABB
            direction.Y = Math.Clamp(direction.Y, -other.Height / 2, other.Height / 2);

            //Add the direction vector to the AABB center to get the closest point to the circle on the AABB
            Vector3 closestPoint = other.Owner.LocalPosition + direction;
            
            //Gets the magnitude of the circle's center and the closest point on the AABB
            float distanceFromClosestPoint = Vector3.GetDistance(Owner.LocalPosition, closestPoint);

            //Returns true if the circle's collision radius is greater than or equal to the distance from the closest point
            return distanceFromClosestPoint <= CollisionRadius;
        }

        public override void Draw()
        {
            base.Draw();
            Raylib.DrawCircleLines((int)Owner.LocalPosition.X, (int)Owner.LocalPosition.Y, CollisionRadius, Color.GREEN);
        }
    }
}
