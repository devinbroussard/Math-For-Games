using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;

namespace Math_For_Games
{
    class SceneManager
    {
        public void InitializeActors()
        {
            //Initializing scene one actors
            Player player = new Player(0, 1, 0, 1, 3, 0.5f, Color.SKYBLUE, "Player", Shape.SPHERE);
            Enemy enemy = new Enemy(0, 1, 3, 2, 3, player, 40, 2, Color.MAROON);

            //Initializing Scene one colliders
            CircleCollider playerCollider = new CircleCollider(1, player);
            CircleCollider enemyCollider = new CircleCollider(1, enemy);
        }

        public void InitializeColliders()
        {

        }
    }
}
