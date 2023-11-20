using FlaxEngine;

namespace Game
{
    public class PlayerLocomotionManager : CharacterLocomotionManager
    {
        private PlayerManager player = null;

        public float verticalMovement = 0f;
        public float horizontalMovement = 0f;
        public float moveAmount = 0f;

        private Vector3 moveDirection = Vector3.Zero;
        private Vector3 targetRotationDirection = Vector3.Zero;
        public float walkingSpeed = 2f;
        public float runningSpeed = 5f;
        public float rotationSpeed = 15.0f;

        public override void OnAwake()
        {
            base.OnAwake();

            player = Actor.GetScript<PlayerManager>();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if(player.isLocalPlayer)
            {
                player.characterNetworkManager.UpdateNetworkAnimatorValues(horizontalMovement, verticalMovement, moveAmount);
            }
            else
            {
                verticalMovement = player.characterNetworkManager.verticalMovement;
                horizontalMovement = player.characterNetworkManager.horizontalMovement;
                moveAmount = player.characterNetworkManager.moveAmount;

                player.playerAnimatorManager.UpdateAnimatorMovement(0, moveAmount);
            }
        }

        public void HandleAllMovement()
        {
            HandleGroundedMovement();
            HandleRotation();
        }

        private void GetMovementValues()
        {
            verticalMovement = PlayerInputManager.instance.verticalInput;
            horizontalMovement = PlayerInputManager.instance.horizontalInput;
            moveAmount = PlayerInputManager.instance.moveAmount;
        }

        private void HandleGroundedMovement()
        {
            GetMovementValues();

            moveDirection = PlayerCamera.instance.Transform.Forward * verticalMovement;
            moveDirection += PlayerCamera.instance.Transform.Right * horizontalMovement;

            moveDirection.Normalize();
            moveDirection.Y = 0f;

            if(PlayerInputManager.instance.moveAmount > 0.5f)
            {
                player.characterController.Move(moveDirection * runningSpeed * Time.DeltaTime);
            }
            else if (PlayerInputManager.instance.moveAmount <= 0.5f)
            {
                player.characterController.Move(moveDirection * walkingSpeed * Time.DeltaTime);
            }
        }

        private void HandleRotation()
        {
            targetRotationDirection = Vector3.Zero;
            targetRotationDirection = PlayerCamera.instance.cameraObject.Transform.Forward * verticalMovement;
            targetRotationDirection += PlayerCamera.instance.cameraObject.Transform.Right * horizontalMovement;
            targetRotationDirection.Normalize();
            targetRotationDirection.Y = 0f;

            if (targetRotationDirection == Vector3.Zero)
            {
                targetRotationDirection = Transform.Forward;
            }

            Quaternion newRotation = Quaternion.LookRotation(targetRotationDirection);
            Quaternion targetRotation = Quaternion.Slerp(Transform.Orientation, newRotation, rotationSpeed * Time.DeltaTime);

            Actor.Orientation = targetRotation;
        }
    }
}