using FlaxEngine;
using System.Transactions;

namespace Game
{
    public class PlayerCamera : IDontDestroyOnLoad
    {
        public static PlayerCamera instance = null;
        public PlayerManager player = null;
        public Camera cameraObject = null;

        [Header("Camera Settings")]
        private Vector3 cameraVelocity = Vector3.Zero;
        private float cameraSmoothSpeed = 1.0f;

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
            }
        }

        private void HandleFollowTarget()
        {
            Vector3 targetCameraPosition = Vector3.SmoothDamp(Transform.Translation, player.Transform.Translation, ref cameraVelocity, cameraSmoothSpeed * Time.DeltaTime);
            Actor.Position = targetCameraPosition;
        }
    }
}