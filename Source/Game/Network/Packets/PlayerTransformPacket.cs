using System;
using FlaxEngine;
using FlaxEngine.Networking;

public class PlayerTransformPacket : NetworkPacket
{
    public Vector3 Position;
    public Quaternion Rotation;

    public override void Serialize(ref NetworkMessage msg)
    {
        msg.WriteVector3(Position);
        msg.WriteQuaternion(Rotation);
    }

    public override void Deserialize(ref NetworkMessage msg)
    {
        Position = msg.ReadVector3();
        Rotation = msg.ReadQuaternion();
    }

    public override void ServerHandler(ref NetworkConnection sender)
    {
        Guid guid = NetworkSession.Instance.GuidByConn(ref sender);
        var player = GameSession.Instance.GetPlayer(guid);
        player.playerNetworkManager.networkPosition = Position;
        player.playerNetworkManager.networkRotation = Rotation;

        var te = new PlayersTransformPacket.TransformEntry();
        var ptp = new PlayersTransformPacket();
        te.Guid = guid;
        te.Position = Position;
        te.Rotation = Rotation;
        ptp.Transforms.Add(te);
        NetworkSession.Instance.SendAll(ptp, NetworkChannelType.UnreliableOrdered);
    }
}
