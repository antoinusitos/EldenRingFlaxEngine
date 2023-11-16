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

        private float _lastTransformSent;

        public void UpdateNetworkPositionAndRotation(Vector3 newPosition, Quaternion newRotation)
        {
            networkPosition = newPosition;
            networkRotation = newRotation;

            PlayerTransformPacket ptp = new PlayerTransformPacket
            {
                Position = networkPosition,
                Rotation = networkRotation
            };
            NetworkSession.Instance.Send(ptp, NetworkChannelType.UnreliableOrdered);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            return;

            if(!isOwner)
            {
                return;
            }

            if (Time.UnscaledGameTime - _lastTransformSent > 0.05f)
            {
                PlayerTransformPacket ptp = new PlayerTransformPacket
                {
                    Position = networkPosition,
                    Rotation = networkRotation
                };
                NetworkSession.Instance.Send(ptp, NetworkChannelType.UnreliableOrdered);
                _lastTransformSent = Time.UnscaledGameTime;
            }
        }
    }
}