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
        public float walkingSpeed = 2f;
        public float runningSpeed = 5f;

        public override void OnAwake()
        {
            base.OnAwake();

            player = Actor.GetScript<PlayerManager>();
        }

        public void HandleAllMovement()
        {
            HandleGroundedMovement();
        }

        private void GetVerticalAndHorizontalInputs()
        {
            verticalMovement = PlayerInputManager.instance.verticalInput;
            horizontalMovement = PlayerInputManager.instance.horizontalInput;
        }

        private void HandleGroundedMovement()
        {
            GetVerticalAndHorizontalInputs();

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
    }
}