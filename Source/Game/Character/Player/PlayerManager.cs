using FlaxEngine;
using FlaxEngine.Networking;

namespace Game
{
    public class PlayerManager : CharacterManager
    {
        [HideInEditor]
        public PlayerAnimatorManager playerAnimatorManager = null;

        public PlayerLocomotionManager playerLocomotionManager = null;

        private PlayerNetworkManager playerNetworkManager = null;

        public bool isLocalPlayer = false;

        public override void OnAwake()
        {
            base.OnAwake();

            playerLocomotionManager = Actor.GetScript<PlayerLocomotionManager>();
            playerNetworkManager = Actor.GetScript<PlayerNetworkManager>();
            playerAnimatorManager = Actor.GetScript<PlayerAnimatorManager>();

            if (isLocalPlayer)
            {
                GameSession.Instance.LocalPlayer.playerNetworkManager = playerNetworkManager;
            }
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if(!playerNetworkManager.isOwner)
            {
                return;
            }

            playerLocomotionManager.HandleAllMovement();
        }

        public override void OnEnable()
        {
            if(isLocalPlayer && PlayerCamera.instance != null)
            {
                PlayerCamera.instance.player = this;
                PlayerInputManager.instance.player = this;
            }
        }

        public override void OnLateUpdate()
        {
            if (!isLocalPlayer)
            {
                return;
            }

            base.OnLateUpdate();

            PlayerCamera.instance.HandleAllCameraActions();
        }
    }
}