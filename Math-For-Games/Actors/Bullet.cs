using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;

namespace Math_For_Games
{
    public enum BulletType
    {
        COOKIE,
        GUN 
    }
    class Bullet : Actor
    {
        private float _speed;
        private Vector3 _velocity;
        /// <summary>
        /// Variable used to track the time that the bullet has been alive in the scene
        /// </summary>
        private float _timeAlive;
        private Vector3 _moveDirection;
        private BulletType _bulletType;
        private Actor _owner;

        public Actor Owner
        {
            get { return _owner; }
        }

        public Vector3 MoveDirection
        {
            get { return _moveDirection; }
            set { _moveDirection = value; }
        }


        public Bullet(Vector3 position, float speed, string name, Vector3 forwardDirection, Actor owner, 
            Color color, Shape shape, BulletType type = BulletType.GUN)
            : base(position, shape, color, name)
        {
            _speed = speed;
            MoveDirection = forwardDirection;
            _owner = owner;
            _bulletType = type;
        }

        /// <summary>
        /// Called every frame to update the bullet
        /// </summary>
        /// <param name="deltaTime"></param>
        public override void Update(float deltaTime)
        {
            if (_bulletType == BulletType.COOKIE)
                Rotate(2 * deltaTime, 2 * deltaTime, 2 * deltaTime);

            //Adds the delta time to the time that the bullet has been alive in the scene
            _timeAlive += deltaTime;

            //If the time that the bullet has been alive reaches or goes over one second...
            if (_timeAlive >= 3)
            {
                //...destroy the bullet...
                base.DestroySelf();
                //...and leave the function
                return;
            }

            _velocity = MoveDirection.Normalized * _speed * deltaTime;


            base.Translate(_velocity.X, _velocity.Y, _velocity.Z);

            base.Update(deltaTime);
        }

        /// <summary>
        /// Gets called on collision with an actor
        /// </summary>
        /// <param name="actor">The actor that a collision occured with</param>
        public override void OnCollision(Actor actor)
        {
            //If the actor is an enemy and is not THIS actor...
            if (actor.Tag == ActorTag.ENEMY && Owner.Tag != ActorTag.ENEMY)
            {
                //...cast the actor as an enemy...
                Enemy enemy = (Enemy)actor;

                //...If the enemy's health is above 0...
                if (enemy.Health > 0)
                    //...Tell the enemy to take damage
                    enemy.TakeDamage();

                //...If the enemy's health is 0...
                if (enemy.Health == 0)
                {
                    //Decrement the static enemy count...
                    Enemy.EnemyCount--;

                    //If the enemy count is 0...
                    if (Enemy.EnemyCount <= 0)
                    {
                        //Create winText UI showing the player that they beat the game...
                        UIText winText = new UIText(300, 75, 5, Shape.CUBE, "Win Text", Color.WHITE, 200, 200, 50, "You won!");
                        //...and add the UI to the scene
                        Engine.CurrentScene.AddActor(winText);
                    }
                    //...and destroy the enemy;
                    enemy.DestroySelf();
                }

                DestroySelf();
            }
            else if (actor.Tag == ActorTag.PLAYER && Owner.Tag != ActorTag.PLAYER)
            {
                Player player = (Player)actor;

                if (player.Health > 0 && player.LastHitTime > 1)
                {
                    player.LastHitTime = 0;
                    player.Health--;
                }
                if (player.Health <= 0)
                {
                    actor.DestroySelf();
                    UIText loseText = new UIText(300, 75, 10, Shape.CUBE, "Lose Text", Color.WHITE, 200, 200, 50, "You lose!");
                    Engine.CurrentScene.AddActor(loseText);
                }

                DestroySelf();
            }
            else if (actor.Tag == ActorTag.BULLET && Owner.Tag != actor.Tag)
            {
                DestroySelf();
                actor.DestroySelf();
            }
        }

        //public override void Draw()
        //{
        //    base.Draw();
        //    Collider.Draw();
        //}
    }
}