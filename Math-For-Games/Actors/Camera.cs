using System;
using System.Collections.Generic;
using System.Text;
using Math_Library;
using Raylib_cs;

namespace Math_For_Games.Actors
{
    class Camera : Actor
    {
        public Vector3 position;
        public Vector3 target;
        public Vector3 up;
        public float fovy;
        public CameraProjection projection;
        private Camera3D _camera;
        private Actor _actorToTarget;

        public Camera(Camera3D camera, Actor actorToTarget)
        {

        }
    }
}
