using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using Math_Library;

namespace Math_For_Games
{
    class Bullet : Actor
    {
        private float _speed;
        private Vector2 _velocity;

        public Bullet(char icon, Vector2 position, Color color, float speed, string name) 
            :base(icon, position, color, name)
        {
            _speed = speed;
        }

        public void OnCollision(Actor actor)
        {

        }
    }
}
