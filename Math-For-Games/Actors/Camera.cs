using System;
using System.Collections.Generic;
using System.Text;
using Math_Library;
using Raylib_cs;

namespace Math_For_Games
{
    class Camera : Actor
    {
        public Vector3 position;
        public Vector3 target;
        public Vector3 up;
        public float fovy;
        public CameraProjection projection;
        private Camera3D _camera3D;
        private Actor _targetActor;

        public Camera3D Camera3D
        {
            get { return _camera3D; }
            set { _camera3D = value; }
        }

        public Camera(Actor targetActor)
            :base()
        {
            _camera3D = new Camera3D();
            _targetActor = targetActor;
        }

        public override void Start()
        {
            // Camera position
            _camera3D.up = new System.Numerics.Vector3(0, 1, 0); //Camera up vector (rotation towards target)
            _camera3D.fovy = 45; // Camera field of view Y
            _camera3D.projection = CameraProjection.CAMERA_PERSPECTIVE; //Camera mode type

            SetTranslation(0, 4, -10);
        }

        public override void Update(float deltaTime)
        {
            // Camera position
            _camera3D.position = new System.Numerics.Vector3(WorldPosition.X, WorldPosition.Y, WorldPosition.Z);
            // Point the camera is focused on
            _camera3D.target = new System.Numerics.Vector3(_targetActor.WorldPosition.X, _targetActor.WorldPosition.Y, _targetActor.WorldPosition.Z);

            base.Update(deltaTime);
        }
    }
}
