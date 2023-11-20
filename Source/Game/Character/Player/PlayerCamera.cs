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
        private float cameraCollisionRadius = 0.2f;
        public LayersMask collideWithLayers;

        [Header("Camera Values")]
        private Vector3 cameraVelocity = Vector3.Zero;
        private Vector3 cameraObjectPosition = Vector3.Zero;
        private float leftAndRightLookAngle = 0.0f;
        private float upAndDownLookAngle = 0.0f;
        private float CameraZPosition = 0.0f;
        private float targetCameraZPosition = 0.0f;

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

            CameraZPosition = cameraObject.LocalTransform.Translation.Z;
        }

        public void HandleAllCameraActions()
        {
            if(player != null)
            {
                HandleFollowTarget();
                HandleRotations();
                HandleCollisions();
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
            cameraPivotTransform.LocalOrientation = targetRotation;
        }

        private void HandleCollisions()
        {
            targetCameraZPosition = CameraZPosition;

            RayCastHit hit;
            Vector3 direction = cameraObject.Position - cameraPivotTransform.Position;
            direction.Normalize();

            if (Physics.SphereCast(cameraPivotTransform.Position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetCameraZPosition), collideWithLayers))
            {
                float distanceFromHitObject = Vector3.Distance(cameraPivotTransform.Position, hit.Point);
                targetCameraZPosition = -(distanceFromHitObject - cameraCollisionRadius);
            }

            if(Mathf.Abs(targetCameraZPosition) < cameraCollisionRadius)
            {
                targetCameraZPosition = -cameraCollisionRadius;
            }

            cameraObjectPosition.Z = Mathf.Lerp(cameraObject.LocalPosition.Z, targetCameraZPosition, 0.2f);
            cameraObject.LocalPosition = cameraObjectPosition;
        }
    }
}