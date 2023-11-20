using System;
using FlaxEngine.Networking;

namespace Game
{
    public class PlayerAnimatorPacket : NetworkPacket
    {
        public float horizontalMovement = 0.0f;
        public float verticalMovement = 0.0f;
        public float moveAmount = 0.0f;

        public override void Serialize(ref NetworkMessage msg)
        {
            msg.WriteSingle(horizontalMovement);
            msg.WriteSingle(verticalMovement);
            msg.WriteSingle(moveAmount);
        }

        public override void Deserialize(ref NetworkMessage msg)
        {
            horizontalMovement = msg.ReadSingle();
            verticalMovement = msg.ReadSingle();
            moveAmount = msg.ReadSingle();
        }

        public override void ServerHandler(ref NetworkConnection sender)
        {
            Guid guid = NetworkSession.Instance.GuidByConn(ref sender);
            var player = GameSession.Instance.GetPlayer(guid);
            player.playerNetworkManager.horizontalMovement = horizontalMovement;
            player.playerNetworkManager.verticalMovement = verticalMovement;
            player.playerNetworkManager.moveAmount = moveAmount;
        }
    }
}
