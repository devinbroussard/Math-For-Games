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

        public Player(float x, float y, float speed, int health, float cooldownTime, string name = "Player", string path = "Sprites/cookie.png")
            : base(x, y, speed, health, name, path)
        {
            Speed = speed;
            _cooldownTime = cooldownTime;
            Tag = ActorTag.PLAYER;
        }

        public override void Update(float deltaTime)
        {
            _lastHitTime += deltaTime;
            //Adds deltaTime to time between shots
            _timeBetweenShots += deltaTime;

            //Gets the xDirection and yDirection of the players input
            int xDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_A))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_D));
            int yDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_W))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_S));

            Vector2 moveDirection = new Vector2(xDirection, yDirection);

            int xDirectionForBullet = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
           + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT));
            int yDirectionForBullet = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_UP))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_DOWN));

            if ((xDirectionForBullet != 0 || yDirectionForBullet != 0) && (_timeBetweenShots >= _cooldownTime))
            {
                _timeBetweenShots = 0;
                Bullet bullet = new Bullet(LocalPosition, 200, "Player Bullet", xDirectionForBullet, yDirectionForBullet, this, "Sprites/cookie.png", BulletType.COOKIE);
                bullet.SetScale(40, 40);
                //CircleCollider bulletCollider = new CircleCollider(20, bullet);
                AABBCollider bulletCollider = new AABBCollider(30, 30, bullet);
                bullet.Collider = bulletCollider;
                Engine.CurrentScene.AddActor(bullet);
            }

            Velocity = moveDirection.Normalized * Speed * deltaTime;

            if (0 < Velocity.X)
                Sprite = new Sprite("Sprites/lodis-right.png");
            else if (Velocity.X < 0)
                Sprite = new Sprite("Sprites/lodis-left.png");

            base.Translate(Velocity.X, Velocity.Y);

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
                    UIText loseText = new UIText(300, 75, "Lose Text", Color.WHITE, 200, 200, 50, "You lose!");
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
