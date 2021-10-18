using System;
using System.Collections.Generic;
using System.Text;
using Math_Library;
using Raylib_cs;

namespace Math_For_Games
{
    class Player : Actor
    {
        private float _speed;
        private Vector2 _velocity;

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

        public Player(char icon, float x, float y, float speed, Color color, string name = "actor")
            : base(icon, x, y, color)
        {
            _speed = speed;
        }

        public override void Update(float deltaTime)
        {
            int xDireciton = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_A))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_D));
            int yDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_W))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_S));

            Vector2 moveDirection = new Vector2(xDireciton, yDirection);

            Velocity = moveDirection * Speed * deltaTime;
            Position += Velocity;
        }

        public override void Draw()
        {
            base.Draw();

        }

        public void ShootBullet()
        {

        }

        public override void OnCollision(Actor actor)
        {
            Engine.CloseApplication();
        }
    }
}
