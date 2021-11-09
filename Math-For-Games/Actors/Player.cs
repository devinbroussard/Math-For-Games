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
        private Vector2 _lastMousePosition;

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
            Vector2 mouseOffset;
            System.Numerics.Vector2 systemMousePosition = Raylib.GetMousePosition();
            Vector2 mousePosition = new Vector2(systemMousePosition.X, systemMousePosition.Y);

            if (_lastMousePosition != null)
            {
                mouseOffset = _lastMousePosition - mousePosition;
            }

            

            _lastHitTime += deltaTime;
            //Adds deltaTime to time between shots
            _timeBetweenShots += deltaTime;



            //Gets the xDirection and yDirection of the players input
            int sideDirection = Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_A))
                - Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_D));

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
            {
                _jumpTime = 0;
            }

            int forwardDirection = Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_W))
                - Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_S));

            //if (WorldPosition.Y >= 1)
                //yDirection = 0;

            int zRotation = Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
           - Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT));
            int zDirectionForBullet = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_UP))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_DOWN));

            float yDirection = 0;
            if (_jumpTime < 0.5)
            {
                yDirection = 0.2f;
                _jumpTime += deltaTime;
            }

            

            Velocity = (forwardDirection * Forward.Normalized) + (sideDirection * Right.Normalized) + (yDirection * Upwards.Normalized) * Speed + Accleration * deltaTime;

            if (( zDirectionForBullet != 0) && (_timeBetweenShots >= _cooldownTime))
            {
                _timeBetweenShots = 0;
                Bullet bullet = new Bullet(LocalPosition, 50, "Player Bullet", Forward, this, Color.YELLOW, Shape.SPHERE, BulletType.COOKIE);
                bullet.SetScale(0.3f, 0.3f, 0.3f);
                //CircleCollider bulletCollider = new CircleCollider(20, bullet);
                AABBCollider bulletCollider = new AABBCollider(30, 30, bullet);
                bullet.Collider = bulletCollider;
                Engine.CurrentScene.AddActor(bullet);
            }

            

            base.Rotate(0, zRotation * 0.05f, 0);
            base.Translate(Velocity.X, Velocity.Y, Velocity.Z);

            base.Update(deltaTime);
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
