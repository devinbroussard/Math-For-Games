using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace Math_For_Games
{
    class Character : Actor
    {
        private float _speed;
        private Vector3 _velocity;
        private Vector3 _acceleration;
        private int _health;

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }
        public Vector3 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        public int Health
        {
            get { return _health; }
            set { _health = value; }
        }

        public Vector3 Accleration
        {
            get 
            {
                if (GlobalTransform.M13 >= 1)
                    _acceleration.Y = -9.81f;
                else
                    _acceleration.Y = 0;
                return _acceleration;
            }
            set { _acceleration = value; }
        }

        public Character(float x, float y, float z, float speed, int health, Color color, string name = "Character", Shape shape = Shape.SPHERE)
             : base(x, y, z, shape, color, name)
        {
            _health = health;
            _speed = speed;
            Velocity = new Vector3(0, 0, 0);
        }
    }
}
