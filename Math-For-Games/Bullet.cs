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
        private Vector2 _moveDirection;
        
        public Vector2 MoveDirection
        {
            get { return _moveDirection; }
            set { value = _moveDirection; }
        }



        public Bullet(char icon, float x, float y, Color color, float speed, string name) 
            :base(icon, x, y, color, name)
        {
            _speed = speed;
        }

        public Bullet(char icon, Vector2 position, Color color, float speed, string name)
            :base(icon, position, color, name)
        {
            _speed = speed;
        }

        public override void Update(float deltaTime)
        {
            Vector2 moveDirection = new Vector2(1, 0);

            _velocity = moveDirection * _speed * deltaTime;

            Position += _velocity;
        }
    }
}
