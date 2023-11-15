using FlaxEngine;

namespace Game
{
    public class CharacterManager : IDontDestroyOnLoad
    {
        public CharacterController characterController = null;

        private CharacterNetworkManager characterNetworkManager = null;

        public override void OnAwake()
        {
            base.OnAwake();

            Debug.Log("OnAwake CharacterManager");

            characterController = (CharacterController)Actor;
            characterNetworkManager = Actor.GetScript<CharacterNetworkManager>();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if(characterNetworkManager.isOwner)
            {
                characterNetworkManager.UpdateNetworkPosition(Actor.Position);
            }
            else
            {
                Actor.Position = Vector3.SmoothDamp(
                    Actor.Position, 
                    characterNetworkManager.networkPosition, 
                    ref characterNetworkManager.networkPositionVelocity, 
                    characterNetworkManager.networkPositionSmoothTime);
            }
        }
    }
}