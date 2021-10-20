using System;
using System.Collections.Generic;
using System.Text;
using Math_Library;
using Raylib_cs;

namespace Math_For_Games
{
    class Enemy : Actor
    {
        private float _speed;
        private Vector2 _velocity;
        private Actor _actorToChase;

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

        public Enemy(char icon, float x, float y, float speed, Color color, Actor actor, string name = "actor")
            : base(icon, x, y, color)
        {
            _speed = speed;
            _actorToChase = actor;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            //The Enemy runs towards the player's position
            Vector2 moveDirection = _actorToChase.Position - Position;

            //The enemy runs away from the player's position
            //Vector2 moveDirection = Position - _actorToChase.Position;

            Velocity = moveDirection.Normalized * Speed * deltaTime;
            Position += Velocity;
        }

        public override void Draw()
        {
            base.Draw();

        }

        public override void OnCollision(Actor actor)
        {
            if (actor is Bullet)
            {
                Icon = new Icon { Color = Color.BLACK, Symbol = '\0' };
                _speed = 0;
            }
        }

    }
}