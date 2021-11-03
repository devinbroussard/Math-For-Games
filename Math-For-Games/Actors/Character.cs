using System;
using System.Collections.Generic;
using System.Text;
using Math_Library;
using Raylib_cs;

namespace Math_For_Games
{
    class Character : Actor
    {
        private float _speed;
        private Vector2 _velocity;
        private int _health;

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

        public int Health
        {
            get { return _health; }
            set { _health = value; }
        }

        public Character(float x, float y, float speed, int health, string name = "Character", string path = "")
             : base(x, y, name, path)
        {
            _health = health;
            _speed = speed;
            Velocity = new Vector2(0, 0);
        }
    }
}
