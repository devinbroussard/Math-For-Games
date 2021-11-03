using System;
using System.Collections.Generic;
using System.Text;
using Math_Library;
using Raylib_cs;

namespace Math_For_Games
{
    class Enemy : Character
    {
        private Actor _actorToChase;
        private float _maxFov;
        public static int EnemyCount;
        private float _timeBetweenShots;
        private float _cooldownTime;

        public Enemy(float x, float y, float speed, int health, Actor actor, float maxFov, float cooldownTime, string name = "Enemy", string path = "Sprites/cookie.png")
            : base(x, y, speed, health, name, path)
        {
            _actorToChase = actor;
            _maxFov = maxFov;
            EnemyCount++;
            Tag = ActorTag.ENEMY;
            _cooldownTime = cooldownTime;
        }

        public override void Update(float deltaTime)
        {
            _timeBetweenShots += deltaTime;

            //The Enemy runs towards the player's position
            if (_actorToChase == null)
                return;
            Vector2 moveDirection = _actorToChase.LocalPosition - LocalPosition;

            //The enemy runs away from the player's position
            //Vector2 moveDirection = Position - _actorToChase.Position;

            Velocity = moveDirection.Normalized * Speed * deltaTime;

            if (0 < Velocity.X)
                Sprite = new Sprite("Sprites/hungry-man-right.png");
            else if (Velocity.X < 0)
                Sprite = new Sprite("Sprites/hungry-man-left.png");

            if (IsTargetInSight())
                Translate(Velocity.X, Velocity.Y);
            else
            {
                base.Translate(Velocity.X * 0.5f, Velocity.Y * 0.5f);
            }
            if (_timeBetweenShots >= 1 && !IsTargetInSight())
            {
                Vector2 directionOfBullet = (_actorToChase.LocalPosition - LocalPosition).Normalized;

                _timeBetweenShots = 0;
                Bullet bullet = new Bullet(LocalPosition, 500, "Enemy Bullet", directionOfBullet.X, directionOfBullet.Y, this);
                bullet.SetScale(30, 30);
                //CircleCollider bulletCollider = new CircleCollider(20, bullet);
                AABBCollider bulletCollider = new AABBCollider(30, 30, bullet);
                bullet.Collider = bulletCollider;
                Engine.CurrentScene.AddActor(bullet);
            }

            base.Update(deltaTime);
            
        }

        public bool IsTargetInSight()
        {
            Vector2 directionOfTarget = (_actorToChase.LocalPosition - LocalPosition).Normalized;
            float distanceOfTarget = Vector2.GetDistance(_actorToChase.LocalPosition, LocalPosition);

            return (Math.Acos(Vector2.GetDotProduct(directionOfTarget, Forward)) * 180/Math.PI) < _maxFov
                && distanceOfTarget < 200;
        }

        public void TakeDamage()
        {
            Health--;
        }

        public override void OnCollision(Actor actor)
        { }

        public override void Draw()
        {
            base.Draw();
            //if (Collider != null)
                //Collider.Draw();
        }
    }
}
