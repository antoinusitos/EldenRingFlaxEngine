using FlaxEngine;

namespace Game
{
    public class PlayerManager : CharacterManager
    {
        private PlayerLocomotionManager playerLocomotionManager = null;

        private PlayerNetworkManager playerNetworkManager = null;

        public override void OnAwake()
        {
            base.OnAwake();

            Debug.Log("OnAwake PlayerManager");

            playerLocomotionManager = Actor.GetScript<PlayerLocomotionManager>();
            playerNetworkManager = Actor.GetScript<PlayerNetworkManager>();
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