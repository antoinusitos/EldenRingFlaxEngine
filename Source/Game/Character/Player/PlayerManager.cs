using FlaxEngine;

namespace Game
{
    public class PlayerManager : CharacterManager
    {
        private PlayerLocomotionManager playerLocomotionManager = null;

        public override void OnAwake()
        {
            base.OnAwake();

            playerLocomotionManager = Actor.GetScript<PlayerLocomotionManager>();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            playerLocomotionManager.HandleAllMovement();
        }
    }
}