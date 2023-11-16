using FlaxEngine;
using System;
using System.Transactions;

namespace Game
{
    public class PlayerCamera : IDontDestroyOnLoad
    {
        public static PlayerCamera instance = null;
        public PlayerManager player = null;
        public Camera cameraObject = null;
        public Actor cameraPivotTransform;

        [Header("Camera Settings")]
        private float cameraSmoothSpeed = 1.0f;
        private float leftAndRightRotationSpeed = 220.0f;
        private float upAndDownRotationSpeed = 220.0f;
        private float minimumPivot = -30.0f;
        private float maximumPicot = 60.0f;

        [Header("Camera Values")]
        private Vector3 cameraVelocity = Vector3.Zero;
        private float leftAndRightLookAngle = 0.0f;
        private float upAndDownLookAngle = 0.0f;

        public override void OnStart()
        {
            base.OnStart();

            if (instance != null)
            {
                Destroy(Actor);
            }
            else
            {
                instance = this;
            }
        }

        public void HandleAllCameraActions()
        {
            if(player != null)
            {
                HandleFollowTarget();
                HandleRotations();
            }
        }

        private void HandleFollowTarget()
        {
            Vector3 targetCameraPosition = Vector3.SmoothDamp(Transform.Translation, player.Transform.Translation, ref cameraVelocity, cameraSmoothSpeed * Time.DeltaTime);
            Actor.Position = targetCameraPosition;
        }

        private void HandleRotations()
        {
            leftAndRightLookAngle += (PlayerInputManager.instance.cameraHorizontalInput * leftAndRightRotationSpeed) * Time.DeltaTime;
            upAndDownLookAngle -= (PlayerInputManager.instance.cameraVerticalInput * upAndDownRotationSpeed) * Time.DeltaTime;
            upAndDownLookAngle = Mathf.Clamp(upAndDownLookAngle, minimumPivot, maximumPicot);

            Vector3 cameraRotation = Vector3.Zero;
            Quaternion targetRotation = Quaternion.Identity;

            cameraRotation.Y = leftAndRightLookAngle;
            targetRotation = Quaternion.Euler(cameraRotation);
            Actor.Orientation = targetRotation;

            cameraRotation = Vector3.Zero;
            cameraRotation.X = upAndDownLookAngle;
            targetRotation = Quaternion.Euler(cameraRotation);
            Transform t = cameraPivotTransform.LocalTransform;
            t.Orientation = targetRotation;
            cameraPivotTransform.LocalTransform = t;
        }
    }
}