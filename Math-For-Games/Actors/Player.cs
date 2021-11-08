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

        public Player(float x, float y, float z, float speed, int health, float cooldownTime, Color color, string name = "Player", Shape shape = Shape.CUBE)
            : base(x, y, z, speed, health, color, name, shape)
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
            int sideDirection = Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_A))
                - Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_D));
            int yDirection = Convert.ToInt32(Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE));
            int forwardDirection = Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_W))
                - Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_S));

            if (WorldPosition.Y >= 1)
                yDirection = 0;
            
            Vector3 moveDirection = new Vector3(sideDirection, yDirection, forwardDirection);

            int zRotation = Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
           - Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT));
            int zDirectionForBullet = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_UP))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_DOWN));



            Velocity =  moveDirection.Normalized * Speed + Accleration * deltaTime;

            if (( zDirectionForBullet != 0) && (_timeBetweenShots >= _cooldownTime))
            {
                _timeBetweenShots = 0;
                Bullet bullet = new Bullet(LocalPosition, 50, "Player Bullet", Forward, this, Color.LIME, Shape.CUBE, BulletType.COOKIE);
                bullet.SetScale(0.7f, 0.7f, 0.7f);
                //CircleCollider bulletCollider = new CircleCollider(20, bullet);
                AABBCollider bulletCollider = new AABBCollider(30, 30, bullet);
                bullet.Collider = bulletCollider;
                Engine.CurrentScene.AddActor(bullet);
            }



            base.Rotate(0, zRotation * 0.05f, 0);
            base.Translate(Velocity.X, Velocity.Y * 5, Velocity.Z);

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
                    UIText loseText = new UIText(300, 75, 10, Shape.CUBE,"Lose Text", Color.WHITE, 200, 200, 50, "You lose!");
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
