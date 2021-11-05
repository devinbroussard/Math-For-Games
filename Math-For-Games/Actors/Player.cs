using System;
using System.Collections.Generic;
using System.Text;
using Math_Library;
using Raylib_cs;

namespace Math_For_Games
{
    class Player : Character
    {
        private float _timeBetweenShots;
        private float _cooldownTime;
        private float _lastHitTime;

        public float LastHitTime
        {
            get { return _lastHitTime; }
            set { _lastHitTime = value; }
        }

        public Player(float x, float y, float z, float speed, int health, float cooldownTime, string name = "Player", Shape shape = Shape.CUBE)
            : base(x, y, z, speed, health, name, shape)
        {
            Speed = speed;
            _cooldownTime = cooldownTime;
            Tag = ActorTag.PLAYER;
        }

        public override void Update(float deltaTime)
        {
            Rotate(1, 1, 1);

            _lastHitTime += deltaTime;
            //Adds deltaTime to time between shots
            _timeBetweenShots += deltaTime;

            //Gets the xDirection and yDirection of the players input
            int xDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_A))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_D));
            int yDirection = Convert.ToInt32(Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE)) * 50;
            int zDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_W))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_S));

            Vector3 moveDirection = new Vector3(xDirection, yDirection, zDirection);

            int xRotation = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
           + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT));
            int zDirectionForBullet = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_UP))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_DOWN));

            if (( zDirectionForBullet != 0) && (_timeBetweenShots >= _cooldownTime))
            {
                _timeBetweenShots = 0;
                Bullet bullet = new Bullet(LocalPosition, 30, "Player Bullet", 0, zDirectionForBullet, this, Shape.CUBE, BulletType.COOKIE);
                bullet.SetScale(1, 1, 1);
                //CircleCollider bulletCollider = new CircleCollider(20, bullet);
                AABBCollider bulletCollider = new AABBCollider(30, 30, bullet);
                bullet.Collider = bulletCollider;
                Engine.CurrentScene.AddActor(bullet);
            }

            Velocity = moveDirection.Normalized * Speed * deltaTime;

            
            //base.Translate(Velocity.X, Velocity.Y * 20, Velocity.Z);
            base.Rotate(xRotation, 0, 0);

            if (WorldPosition.Y > 0.5)
                base.Translate(0, -0.5f, 0);

            base.Update(deltaTime);
        }

        public void TakeDamage()
        {
            Health--;
        }

        public override void OnCollision(Actor actor)
        {
            if (actor.Tag == ActorTag.ENEMY)
            {
                if (Health > 0 && _lastHitTime > 1)
                {
                    _lastHitTime = 0;
                    Health--;
                }
                if (Health <= 0)
                {
                    DestroySelf();
                    UIText loseText = new UIText(300, 75, 10, "Lose Text", Color.WHITE, 200, 200, 50, "You lose!");
                    Engine.CurrentScene.AddActor(loseText);
                }
            }
        }

        public override void Draw()
        {
            base.Draw();
            //Collider.Draw();
        }
    }
}
