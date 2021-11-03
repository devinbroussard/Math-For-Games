using System;
using System.Collections.Generic;
using System.Text;
using Math_Library;
using Raylib_cs;

namespace Math_For_Games
{
    public enum ActorTag
    {
        PLAYER,
        ENEMY,
        BULLET,
        GENERIC
    }
    class Actor
    {
        private string _name;
        private bool _started;
        /// <summary>
        /// The forward facing direction of the actor
        /// </summary>
        private ActorTag _tag;
        private Collider _collider;
        private Matrix3 _globalTransform = Matrix3.Identity;
        private Matrix3 _localTransform = Matrix3.Identity;
        private Matrix3 _translation = Matrix3.Identity;
        private Matrix3 _rotation = Matrix3.Identity;
        private Matrix3 _scale = Matrix3.Identity;
        private Actor[] _children = new Actor[0];
        private Actor _parent;
        private Sprite _sprite;

        //The collider attached to this actor
        public Collider Collider
        {
            get { return _collider; }
            set { _collider = value; }
        }

        public ActorTag Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }

        public Vector2 Forward
        {
            get { return new Vector2(_rotation.M00, _rotation.M10); }
            set 
            {
                Vector2 point = value.Normalized + LocalPosition;
                LookAt(point);
            }
        }

        public float ScaleX
        {
            get { return new Vector2(_scale.M00, _scale.M10).Magnitude; }
        }

        public float ScaleY
        {
            get { return new Vector2(_scale.M01, _scale.M11).Magnitude; }
        }

        public Sprite Sprite
        {
            get { return _sprite; }
            set { _sprite = value; }
        }
        
        /// <summary>
        /// True if the start function has been called for this actor
        /// </summary>
        public bool Started
        {
            get { return _started; }
        }

        public Vector2 LocalPosition
        {
            get { return new Vector2(_translation.M02, _translation.M12); }
            set 
            { SetTranslation(value.X, value.Y); }
        }

        public Vector2 WorldPosition
        {
            get { return new Vector2(_globalTransform.M02, _globalTransform.M12); }
            set
            {
                if (Parent != null)
                {
                    Vector2 offset = value - Parent.WorldPosition;

                    SetTranslation(offset.X / Parent.ScaleX, offset.Y / Parent.ScaleY);
                }
                else
                    SetTranslation(value.X, value.Y);
            }
        }

        public Matrix3 GlobalTransform
        {
            get { return _globalTransform; } 
            //Needs to change local transform in relation to global
            set
            {
                _globalTransform = value;
            }
        }

        public Matrix3 LocalTransform
        {
            get { return _localTransform; } 
            private set { _localTransform = value; }
        }

        public Actor Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        public Actor[] Children
        {
            get { return _children; }
        }


        public Vector2 Size
        {
            get { return new Vector2(_scale.M00, _scale.M11); }
            set { SetScale(value.X, value.Y); }
        }

        public Actor(float x, float y, string name = "Actor", string path = "", ActorTag tag = ActorTag.GENERIC) :
            this(new Vector2 { X = x, Y = y }, name, path, tag)
        {
        }

        public Actor(Vector2 position, string name = "Actor", string path = "", ActorTag tag = ActorTag.GENERIC )
        {
            Forward = new Vector2(1, 0);
            LocalPosition = position;
            _name = name;
            Tag = tag;

            if (path != "")
                _sprite = new Sprite(path);
        }

        public Actor()
        { }

        public void UpdateTransforms()
        {
            _localTransform = _translation * _rotation * _scale;

            if (Parent != null)
                GlobalTransform = Parent.GlobalTransform * LocalTransform;
            else GlobalTransform = LocalTransform;
        }

        public void AddChild(Actor child)
        {
            Actor[] tempArray = new Actor[_children.Length + 1];

            for (int i = 0; i < _children.Length; i++)
            {
                tempArray[i] = _children[i];
            }

            tempArray[_children.Length] = child;
            _children = tempArray;

            child.Parent = this;
        }

        public bool RemoveChild(Actor child)
        {
            bool removedActor = false;
            Actor[] tempArray = new Actor[_children.Length - 1];

            int j = 0;
            for (int i = 0; i < _children.Length; i++)
            {
                if (_children[i] != child)
                {
                    tempArray[j] = _children[i];
                    j++;
                }
                else
                {
                    child.Parent = null;
                    removedActor = true;
                    j++;
                }
            }

            _children = tempArray;

            child.Parent = null;

            return removedActor;
        }

        public virtual void Start() 
        {
            _started = true;
        }

        public virtual void Update(float deltaTime) 
        {
            UpdateTransforms();
        }

        public virtual void Draw() 
        {
            //Raylib.DrawCircleLines((int)Position.X, (int)Position.Y, 20, Color.WHITE);
            if (_sprite != null)
                _sprite.Draw(GlobalTransform);
        }

        public virtual void End()
        { }

        public void DestroySelf()
        {
            Engine.CurrentScene.RemoveActor(this);
        }

        public virtual void OnCollision(Actor actor)
        {

        }

        /// <summary>
        /// Checks if this actor collided with another actor
        /// </summary>
        /// <param name="other"The actor to check for a collision against></param>
        /// <returns>True if a collision has occured</returns>
        public virtual bool CheckForCollision(Actor other)
        {
            //Return false if either actor doesn't have a collider
            if (Collider == null || other.Collider == null)
                return false;

            return Collider.CheckCollision(other);
        }

        /// <summary>
        /// Sets the position of the actor
        /// </summary>
        /// <param name="translationX">The new x position</param>
        /// <param name="translationY">The new y position</param>
        public void SetTranslation(float translationX, float translationY)
        {
            _translation = Matrix3.CreateTranslation(translationX, translationY);
        }

        /// <summary>
        /// Applies the given values to the current translation
        /// </summary>
        /// <param name="translationX">The amount to move the x</param>
        /// <param name="translationY">The amount to move the y</param>
        public void Translate(float translationX, float translationY)
        {
            _translation *= Matrix3.CreateTranslation(translationX, translationY);
        }

        /// <summary>
        /// Set the rotation of the actor.
        /// </summary>
        /// <param name="radians">The angle of the rotation in radians.</param>
        public void SetRotation(float radians)
        {
            _rotation = Matrix3.CreateRotation(radians);
        }

        /// <summary>
        /// Adds a rotation to the current transform's rotation.
        /// </summary>
        /// <param name="radians"></param>
        public void Rotate(float radians)
        {
            _rotation *= Matrix3.CreateRotation(radians);
        }

        /// <summary>
        /// Changes the scale of the actor
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetScale(float x, float y)
        {
            _scale = Matrix3.CreateScale(x, y);
        }

        /// <summary>
        /// Scales the actor by the given amount.
        /// </summary>
        /// <param name="x">The value to scale on the x axis.</param>
        /// <param name="y">The value to scale on the y axis</param>
        public void Scale(float x, float y)
        {
            _scale *= Matrix3.CreateScale(x, y);
        }

        /// <summary>
        /// Changes the actor's forward position to face the given position
        /// </summary>
        /// <param name="position">The position the actor will face</param>
        public void LookAt(Vector2 position)
        {
            //Find the direction that the actor should look in
            Vector2 direction = (position - LocalPosition).Normalized;
            //Use the dot product to find the angle the actor needs to rotate
            float dotProd = Vector2.GetDotProduct(direction, Forward);

            if (dotProd > 1)
                dotProd = 1;

            float angle = (float)Math.Acos(dotProd);

            //Find a perpindicular vector to the direction
            Vector2 perpDirection = new Vector2(direction.Y, -direction.X);
            //Find the dot product of the perpindicular vector2 and the current forward 
            float perpDot = Vector2.GetDotProduct(perpDirection, Forward);

            //If the result is not 0, use it to change the sign of the angle to be either positive or negative
            if (perpDot != 0)
                angle *= -perpDot / Math.Abs(perpDot);

            //Rotates the player by the angle given
            Rotate(angle);
        }
    }
}
