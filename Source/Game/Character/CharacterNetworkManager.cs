using FlaxEngine;
using FlaxEngine.Networking;

namespace Game
{
    public class CharacterNetworkManager : Script
    {
        [Header("Network Settings")]
        public bool isOwner = true;

        [Header("Position")]
        public Vector3 networkPosition = Vector3.Zero;
        public Vector3 networkPositionVelocity = Vector3.Zero;
        public float networkPositionSmoothTime = 0.1f;

        [Header("Rotation")]
        public Quaternion networkRotation = Quaternion.Identity;
        public float networkRotationSmoothTime = 0.1f;

        [Header("Animator")]
        public float horizontalMovement = 0.0f;
        public float verticalMovement = 0.0f;
        public float moveAmount = 0.0f;

        public void UpdateNetworkPositionAndRotation(Vector3 newPosition, Quaternion newRotation)
        {
            networkPosition = newPosition;
            networkRotation = newRotation;

            if (!NetworkSession.Instance.IsServer)
            {
                PlayerTransformPacket ptp = new PlayerTransformPacket
                {
                    Position = networkPosition,
                    Rotation = networkRotation
                };
                NetworkSession.Instance.Send(ptp, NetworkChannelType.UnreliableOrdered);
            }
            else
            {
                var te = new PlayersTransformPacket.TransformEntry();
                var ptp = new PlayersTransformPacket();
                te.Guid = GameSession.Instance.LocalPlayer.ID;
                te.Position = newPosition;
                te.Rotation = newRotation;
                ptp.Transforms.Add(te);
                NetworkSession.Instance.SendAll(ptp, NetworkChannelType.UnreliableOrdered);
            }
        }

        public void UpdateNetworkAnimatorValues(float horizontal, float vertical, float movementAmount)
        {
            horizontalMovement = horizontal; 
            verticalMovement = vertical;
            moveAmount = movementAmount;

            if (!NetworkSession.Instance.IsServer)
            {
                PlayerAnimatorPacket pap = new PlayerAnimatorPacket
                {
                    horizontalMovement = horizontal,
                    verticalMovement = vertical,
                    moveAmount = movementAmount
                };
                NetworkSession.Instance.Send(pap, NetworkChannelType.UnreliableOrdered);
            }
            else
            {
                var ae = new PlayersAnimatorPacket.AnimatorEntry();
                var pap = new PlayersAnimatorPacket();
                ae.Guid = GameSession.Instance.LocalPlayer.ID;
                ae.horizontalMovement = horizontal;
                ae.verticalMovement = vertical;
                ae.moveAmount = movementAmount;
                pap.Animators.Add(ae);
                NetworkSession.Instance.SendAll(pap, NetworkChannelType.UnreliableOrdered);
            }
        }
    }
}