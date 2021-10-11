using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Math_For_Games
{
    class Engine
    {
        private static bool _applicationShouldClose = false;
        private static int _currentSceneIndex;
        private Scene[] _scenes = new Scene[0];

        /// <summary>
        /// Called to begin the application
        /// </summary>
        public void Run()
        {
            //Call start for the entire application
            Start();

            //Loops until the application is told to close
            while (!_applicationShouldClose)
            {
                Update();
                Draw();
                Thread.Sleep(150);
            }

            //Called when the application closes
            End();
        }

        /// <summary>
        /// Called when the application starts
        /// </summary>
        private void Start()
        {
            Scene scene = new Scene();
            Actor actor = new Actor('P', new Math_Library.Vector2 { X = 0, Y = 0 });

            scene.AddActor(actor);

            _currentSceneIndex = AddScene(scene);

            _scenes[_currentSceneIndex].Start();
        }

        /// <summary>
        /// Called everytime the game loops
        /// </summary>
        private void Update()
        {
           _scenes[_currentSceneIndex].Update();
        }

        /// <summary>
        /// Called every time the game loops to update visuals
        /// </summary>
        private void Draw() 
        {
            Console.Clear();
            _scenes[_currentSceneIndex].Draw();
        }

        /// <summary>
        /// Called when the application exits
        /// </summary>
        private void End() 
        {
            //_scenes[_currentSceneIndex].End();
        }

        /// <summary>
        /// Adds a scene to the engine's scene array
        /// </summary>
        /// <param name="scene">The scene that will be added to the scene array</param>
        /// <returns>The index where the new scene is located</returns>
        public int AddScene(Scene scene)
        {
            //Create a new temporary array
            Scene[] tempArray = new Scene[_scenes.Length + 1];

            //Copy all the values from the old array into the new array
            for (int i = 0; i < _scenes.Length; i++)
                tempArray[i] = _scenes[i];

            //Set the last index to be the new scene
            tempArray[_scenes.Length] = scene;

            //Set the old array to be the new array
            _scenes = tempArray;

            //Return the last index
            return _scenes.Length - 1;
        }
    }
}
