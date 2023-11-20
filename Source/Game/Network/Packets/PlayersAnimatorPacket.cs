using System;
using System.Collections.Generic;
using FlaxEngine.Networking;

public class PlayersAnimatorPacket : NetworkPacket
{
    public struct AnimatorEntry
    {
        public Guid Guid;
        public float horizontalMovement;
        public float verticalMovement;
        public float moveAmount;
    }

    public List<AnimatorEntry> Animators = new List<AnimatorEntry>();

    public override void Serialize(ref NetworkMessage msg)
    {
        msg.WriteInt32(Animators.Count);
        for (var i = 0; i < Animators.Count; i++)
        {
            msg.WriteGuid(Animators[i].Guid);
            msg.WriteSingle(Animators[i].horizontalMovement);
            msg.WriteSingle(Animators[i].verticalMovement);
            msg.WriteSingle(Animators[i].moveAmount);
        }
    }

    public override void Deserialize(ref NetworkMessage msg)
    {
        Animators.Clear();
        var count = msg.ReadInt32();
        for (int i = 0; i < count; i++)
        {
            AnimatorEntry te = new AnimatorEntry();
            te.Guid = msg.ReadGuid();
            te.horizontalMovement = msg.ReadSingle();
            te.verticalMovement = msg.ReadSingle();
            te.moveAmount = msg.ReadSingle();
            Animators.Add(te);
        }
    }

    public override void ClientHandler()
    {
        for (var i = 0; i < Animators.Count; i++)
        {
            if (GameSession.Instance.LocalPlayer.ID == Animators[i].Guid)
                continue;

            var player = GameSession.Instance.GetPlayer(Animators[i].Guid);
            player.playerNetworkManager.horizontalMovement = Animators[i].horizontalMovement;
            player.playerNetworkManager.verticalMovement = Animators[i].verticalMovement;
            player.playerNetworkManager.moveAmount = Animators[i].moveAmount;
        }
    }
}
