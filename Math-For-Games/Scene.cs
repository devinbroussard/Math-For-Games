﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Math_For_Games
{
    class Scene
    {
        /// <summary>
        /// Array that stores all actors in the scene
        /// </summary>
        private Actor[] _actors;

        public Scene()
        {
            _actors = new Actor[0];
        }

        /// <summary>
        /// Calls start for all actors in the actors array
        /// </summary>
        public virtual void Start()
        {
            for (int i = 0; i < _actors.Length; i++)
                _actors[i].Start();
        }

        /// <summary>
        /// Calls update for every actor in the scene.
        /// Calls start for the actor if it hasn't already been called
        /// </summary>
        public virtual void Update() 
        {
            for (int i = 0; i < _actors.Length; i++)
            {
                if (!_actors[i].Started)
                    _actors[i].Start();
                _actors[i].Update();
            }
        }

        public virtual void Draw()
        {
            for (int i = 0; i < _actors.Length; i++)
                _actors[i].Draw();
        }

        public virtual void End() 
        {
            for (int i = 0; i < _actors.Length; i++)
                _actors[i].End();
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
            for (int i = 0; i < tempArray.Length; i++)
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
