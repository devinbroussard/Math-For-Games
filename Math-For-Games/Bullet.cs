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
        private int _xDirection;
        private int _yDirection;

        private Vector2 _moveDirection;
        
        public Vector2 MoveDirection
        {
            get 
            {
                _moveDirection = new Vector2(_xDirection, _yDirection);
                return _moveDirection; 
            }
            set { value = _moveDirection; }
        }

        public Bullet(char icon, Vector2 position, Color color, float speed, string name, int xDirection, int yDirection)
            :base(icon, position, color, name)
        {
            _speed = speed;
            _xDirection = xDirection;
            _yDirection = yDirection;
        }

        public override void Update(float deltaTime)
        {
            _velocity = MoveDirection * _speed * deltaTime;

            Position += _velocity;
        }
    }
}
