using FlaxEngine;

namespace Game
{
    public class PlayerManager : CharacterManager
    {
        private PlayerLocomotionManager playerLocomotionManager = null;

        private PlayerNetworkManager playerNetworkManager = null;

        public bool isLocalPlayer = false;

        public override void OnAwake()
        {
            base.OnAwake();

            playerLocomotionManager = Actor.GetScript<PlayerLocomotionManager>();
            playerNetworkManager = Actor.GetScript<PlayerNetworkManager>();

            if(isLocalPlayer)
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
    }
}