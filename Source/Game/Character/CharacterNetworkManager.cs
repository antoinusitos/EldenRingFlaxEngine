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

        public void UpdateNetworkPosition(Vector3 value)
        {
            networkPosition = value;
            PlayerTransformPacket ptp = new PlayerTransformPacket
            {
                Position = networkPosition
            };
            NetworkSession.Instance.SendAll(ptp, NetworkChannelType.UnreliableOrdered);
        }
    }
}