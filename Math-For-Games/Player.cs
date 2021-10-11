﻿using System;
using System.Collections.Generic;
using System.Text;
using Math_Library;

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
            get { return Velocity; }
            set { _velocity = value; }
        }

        public Player(char icon, float x, float y, float speed, string name = "actor", ConsoleColor color = ConsoleColor.White)
            : base(icon, x, y, name, color)
        {
            _speed = speed;
        }

        public override void Update()
        {
            Vector2 moveDirection = new Vector2();

            ConsoleKey keyPressed = Engine.GetNextKey();

            if (keyPressed == ConsoleKey.A)
                moveDirection = new Vector2 { X = -1 };
            if (keyPressed == ConsoleKey.D)
                moveDirection = new Vector2 { X = 1 };
            if (keyPressed == ConsoleKey.W)
                moveDirection = new Vector2 { Y = -1 };
            if (keyPressed == ConsoleKey.S)
                moveDirection = new Vector2 { Y = 1 };

            moveDirection.X *= Speed;
            moveDirection.Y *= Speed;

            Velocity = moveDirection;

            Position = new Vector2 { X = (Position.X + Velocity.X), Y = (Position.Y + Velocity.Y) };
        }
    }
}
