using System;
using System.Collections.Generic;
using System.Text;
using Math_Library;
using Raylib_cs;
using System.Diagnostics;

namespace Math_For_Games
{
    class Player : Actor
    {
        private float _speed;
        private Vector2 _velocity;
        private Scene _scene;
        Stopwatch _stopwatch = new Stopwatch();
        private float _currentTime = 0;
        private float _lastTime = 0;
        private int _cooldownTime;



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

        public override void Start()
        {
            base.Start();
            _stopwatch.Start();
        }

        public Player(char icon, float x, float y, float speed, Color color, Scene scene, int cooldownTime, string name = "actor")
            : base(icon, x, y, color)
        {
            _speed = speed;
            _scene = scene;
            _cooldownTime = cooldownTime;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            int xDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_A))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_D));
            int yDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_W))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_S));

            int xDirectionForBullet = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
            + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT));
            int yDirectionForBullet = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_UP))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_DOWN));

            Bullet bullet;

            _currentTime = deltaTime;
            _lastTime = 0;

            if ((xDirectionForBullet != 0 || yDirectionForBullet != 0) && (_currentTime - _lastTime >= 1000 || _lastTime == 0))
            {
                _lastTime = _currentTime;
                bullet = new Bullet('.', Position, Color.GOLD, 2000, "Player Bullet", xDirectionForBullet, yDirectionForBullet);
                _scene.AddActor(bullet);
            }
            
            Vector2 moveDirection = new Vector2(xDirection, yDirection);

            Velocity = moveDirection * Speed * deltaTime;
            Position += Velocity;
        }

        public override void Draw()
        {
            base.Draw();

        }

        public override void OnCollision(Actor actor)
        {
            //if (actor is Enemy)
                //Engine.CloseApplication();
        }
    }
}
