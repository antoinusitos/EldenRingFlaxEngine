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

            characterController = (CharacterController)Actor;
            characterNetworkManager = Actor.GetScript<CharacterNetworkManager>();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if(characterNetworkManager.isOwner)
            {
                characterNetworkManager.UpdateNetworkPositionAndRotation(Actor.Position, Actor.Orientation);
            }
            else
            {
                Actor.Position = Vector3.SmoothDamp(
                    Actor.Position, 
                    characterNetworkManager.networkPosition, 
                    ref characterNetworkManager.networkPositionVelocity, 
                    characterNetworkManager.networkPositionSmoothTime);

                Actor.Orientation = Quaternion.Slerp(
                    Actor.Orientation, 
                    characterNetworkManager.networkRotation, 
                    characterNetworkManager.networkRotationSmoothTime);
            }
        }
    }
}