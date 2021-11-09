using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;

namespace Math_For_Games
{
    class Scene
    {
        /// <summary>
        /// Array that stores all actors in the scene
        /// </summary>
        private Actor[] _actors;
        /// <summary>
        /// Array that stores UI stuff only
        /// </summary>
        private Actor[] _UIElements;
        public static Actor[] SceneOneActors;

        public Scene()
        {
            _actors = new Actor[0];
            _UIElements = new Actor[0];
        }

        public Actor[] Actors
        {
            get { return _actors; }
        }

        /// <summary>
        /// </summary>
        public virtual void Start()
        { }

        /// <summary>
        /// Calls update for every actor in the scene.
        /// Calls start for the actor if it hasn't already been called
        /// </summary>
        public virtual void Update(float deltaTime)
        {
            for (int i = 0; i < _actors.Length; i++)
            {
                if (!_actors[i].Started)
                    _actors[i].Start();
                _actors[i].Update(deltaTime);

                for (int j = 0; j < _actors.Length; j++)
                {
                    if (i < _actors.Length)
                        if (_actors[i].CheckForCollision(_actors[j]) && j != i)
                        _actors[i].OnCollision(_actors[j]);
                }
            }
        }

        public virtual void UpdateUI(float deltaTime)
        {
            for (int i = 0; i < _UIElements.Length; i++)
            {
                if (!_UIElements[i].Started)
                    _UIElements[i].Start();
                _UIElements[i].Update(deltaTime);
            }
        }

        public virtual void Draw()
        {
            for (int i = 0; i < _actors.Length; i++)
                _actors[i].Draw();
        }

        public virtual void DrawUI()
        {
            for (int i = 0; i < _UIElements.Length; i++)
                _UIElements[i].Draw();
        }

        public virtual void End()
        {
            for (int i = 0; i < _actors.Length; i++)
                _actors[i].DestroySelf();
        }
        public static void InitializeActors()
        {
            //Initializing scene one actors
            Player player = new Player(0, 1, 0, 1, 3, 0.5f, Color.SKYBLUE, "Player", Shape.SPHERE);
            Enemy enemy = new Enemy(0, 1, 3, 2, 3, player, 40, 2, Color.MAROON);
            Engine.Camera = new Camera(player);

            SceneOneActors = new Actor[] { player, enemy, Engine.Camera };
        }

        /// <summary>
        /// Adds an actor the scenes list of actors
        /// </summary>
        /// <param name="actor"></param>
        public void AddActor(Actor actor)
        {
            //Create a temp array larger than the original
            Actor[] tempArray = new Actor[_actors.Length + 1];

            //Copy all values from the original array into the temp array
            for (int i = 0; i < _actors.Length; i++)
            {
                tempArray[i] = _actors[i];
            }
            //Adds the new actor to the end of the new array
            tempArray[_actors.Length] = actor;

            //Set the old array to be the new array;
            _actors = tempArray;
        }

        /// <summary>
        /// Adds an array of actors the scenes list of actors
        /// </summary>
        /// <param name="actor"></param>
        public void AddActor(Actor[] actors)
        {
            //Create a temp array larger than the original
            Actor[] tempArray = new Actor[_actors.Length + actors.Length];

            int j = 0;
            //Copy all values from the original array into the temp array
            for (int i = 0; i < _actors.Length; i++)
            {
                tempArray[i] = _actors[i];
                j++;
            }
            
            for (int i = 0; i < actors.Length; i++)
            {
                tempArray[j] = actors[i];
                j++;
            }

            //Set the old array to be the new array;
            _actors = tempArray;
        }

        /// <summary>
        /// Adds an actor to the scenes list of UI elements.
        /// </summary>
        /// <param name="actor"></param>
        public void AddUIElement(Actor UI)
        {
            //Create a temp array larger than the original
            Actor[] tempArray = new Actor[_UIElements.Length + 1];

            //Copy all values from the original array into the temp array
            for (int i = 0; i < _UIElements.Length; i++)
            {
                tempArray[i] = _UIElements[i];
            }
            //Adds the new actor to the end of the new array
            tempArray[_UIElements.Length] = UI;

            //Set the old array to be the new array;
            _UIElements = tempArray;
        }

        /// <summary>
        /// Removes an actor from the scene list of actors
        /// </summary>
        /// <param name="actor"></param>
        /// <returns></returns>
        public bool RemoveUIElement(UIText actor)
        {
            //Create a variable to store if the removal was successful
            bool actorRemoved = false;
            //Create a new array that is smaller than the original
            Actor[] tempArray = new Actor[_UIElements.Length - 1];

            //Creates a variable to store the index of the temparray
            int j = 0;
            //Copies all of the values except the actor we don't want into the new array
            for (int i = 0; i < _UIElements.Length; i++)
            {
                //If the actor that th eloop is on is no tthe one to remove...
                if (_UIElements[i] != actor)
                {
                    //...add the actor into the new array and increment the temp array counter
                    tempArray[j] = _UIElements[i];
                    j++;
                }

                //Otherwise if this actor is the one to remove...
                else
                {
                    //...Set acorRemoved to true
                    actorRemoved = true;
                }
            }

            //If the actor removal was successful...
            if (actorRemoved)
                //Set the actors array to be the new array
                _UIElements = tempArray;

            //Return if an actor was removed
            return actorRemoved;
        }


        /// <summary>
        /// Removes an actor from the scene list of actors
        /// </summary>
        /// <param name="actor"></param>
        /// <returns></returns>
        public bool RemoveActor(Actor actor)
        {
            //Create a variable to store if the removal was successful
            bool actorRemoved = false;
            //Create a new array that is smaller than the original
            Actor[] tempArray = new Actor[_actors.Length - 1];

            //Creates a variable to store the index of the temparray
            int j = 0;
            //Copies all of the values except the actor we don't want into the new array
            for (int i = 0; i < _actors.Length; i++)
            {
                //If the actor that th eloop is on is no tthe one to remove...
                if (_actors[i] != actor)
                {
                    //...add the actor into the new array and increment the temp array counter
                    tempArray[j] = _actors[i];
                    j++;
                }

                //Otherwise if this actor is the one to remove...
                else
                {
                    //...Set acorRemoved to true
                    actorRemoved = true;
                }
            }

            //If the actor removal was successful...
            if (actorRemoved)
                //Set the actors array to be the new array
                _actors = tempArray;

            //Return if an actor was removed
            return actorRemoved;
        }
    }
}
