using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace Math_For_Games
{
    class Player : Character
    {
        private float _timeBetweenShots;
        private float _cooldownTime;
        private float _lastHitTime;
        private float _jumpTime = 100;
        private Vector2 mouseOrigin = new Vector2(Raylib.GetMonitorWidth(1)/2, Raylib.GetMonitorHeight(1)/2);

        public float LastHitTime
        {
            get { return _lastHitTime; }
            set { _lastHitTime = value; }
        }

        public Player(float x, float y, float z, float speed, int health, float cooldownTime, Color color, string name = "Player", Shape shape = Shape.CUBE)
            : base(x, y, z, speed, health, color, name, shape)
        {
            Speed = speed;
            _cooldownTime = cooldownTime;
            Tag = ActorTag.PLAYER;
            SetScale(1, 1, 1);
        }

        public override void Start()
        {
            CircleCollider playerCollider = new CircleCollider(1, this);
            base.Start();
        }

        public override void Update(float deltaTime)
        {
            GetTranslationInput(deltaTime);
            GetMouseInput();
            
            _lastHitTime += deltaTime;


          
            base.Update(deltaTime);
        }

        public void GetMouseInput()
        {
            Vector2 mousePosition = new Vector2(Raylib.GetMouseX(), Raylib.GetMouseY());
            Vector2 mouseDelta = mousePosition - mouseOrigin;
            Raylib.SetMousePosition(Raylib.GetMonitorWidth(1) / 2, Raylib.GetMonitorHeight(1) / 2);

            float angle = MathF.Atan2(mouseDelta.Y, mouseDelta.X) * 0.000001f;
            if (angle < 0) angle += 360;

            if (mouseDelta.Magnitude < 0.1f)
            {
                angle = 0;

            }


            base.Rotate(0, angle, angle);

        }

        public void GetTranslationInput(float deltaTime)
        {
            float upDirection = 0;

            //Gets the forward and side inputs of the player
            int forwardDirection = Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_W))
                - Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_S));
            int sideDirection = Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_A))
                - Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_D));

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
                _jumpTime = 0;

            if (_jumpTime < 0.5)
            {
                upDirection = 0.4f;
                _jumpTime += deltaTime;
            }

            Velocity = (forwardDirection * Forward) + (sideDirection * Right) + (upDirection * Upwards) * Speed + Accleration * deltaTime;

            base.Translate(Velocity.X, Velocity.Y, Velocity.Z);
        }

        public void getBulletInput(float deltaTime)
        {
            //Adds deltaTime to time between shots
            _timeBetweenShots += deltaTime;

            int zDirectionForBullet = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_UP))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_DOWN));

            if ((zDirectionForBullet != 0) && (_timeBetweenShots >= _cooldownTime))
            {
                _timeBetweenShots = 0;
                Bullet bullet = new Bullet(LocalPosition, 50, "Player Bullet", Forward, this, Color.YELLOW, Shape.SPHERE, BulletType.COOKIE);
                bullet.SetScale(0.3f, 0.3f, 0.3f);
                //CircleCollider bulletCollider = new CircleCollider(20, bullet);
                AABBCollider bulletCollider = new AABBCollider(0.3f, 0.3f, bullet);
                bullet.Collider = bulletCollider;
                Engine.CurrentScene.AddActor(bullet);
            }

        }



        public void TakeDamage()
        {
            Health--;
        }

        public override void OnCollision(Actor actor)
        {
            Console.WriteLine("Collision");

        }

        public override void Draw()
        {
            base.Draw();
            //Collider.Draw();
        }
    }
}
