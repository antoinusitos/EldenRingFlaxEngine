using FlaxEngine;
using FlaxEngine.Networking;
using System;

namespace Game
{
    public class DemoSceneScript : Script
    {
        private GameSession _game;
        public Prefab PlayerPrefab;
        private float _lastTransformSent;

        /// <inheritdoc/>
        public override void OnEnable()
        {
            _game = GameSession.Instance;
            _game.OnPlayerAdded += OnPlayerAdded;
            _game.OnPlayerRemoved += OnPlayerRemoved;
            for (var i = 0; i < _game.Players.Count; i++)
            {
                OnPlayerAdded(_game.Players[i]);
            }

            _lastTransformSent = 0;
        }

        public void OnPlayerAdded(Player player)
        {
            Debug.Log("OnPlayerAdded");
            Actor spawnedPlayer = PrefabManager.SpawnPrefab(PlayerPrefab, Actor);
            PlayerNetworkManager playerNetworkManager = spawnedPlayer.GetScript<PlayerNetworkManager>();
            playerNetworkManager.isOwner = false;
            GameSession.Instance.AffectPlayerNetworkManager(playerNetworkManager, player.ID);
            /*var script = player.GetScript<NetworkPlayerScript>();
            script.Player = player;*/
            spawnedPlayer.Name = "Player_" + player.Name;
            Random rand = new Random();
            spawnedPlayer.Position = new Vector3(rand.Next(-100, 100), rand.Next(-100, 100), 0);
        }

        public void OnPlayerRemoved(Player player)
        {
            Debug.Log("OnPlayerRemoved");
            Destroy(player.playerNetworkManager.Actor);
        }

        /// <inheritdoc/>
        public override void OnDisable()
        {
            _game.OnPlayerAdded -= OnPlayerAdded;
            _game.OnPlayerRemoved -= OnPlayerRemoved;
            for (var i = 0; i < _game.Players.Count; i++)
            {
                _game.Players[i].playerNetworkManager = null;
            }

            NetworkSession.Instance.Disconnect();
        }

        /// <inheritdoc/>
        public override void OnUpdate()
        {
            if (NetworkSession.Instance.IsServer && Time.UnscaledGameTime - _lastTransformSent > 0.05f)
            {
                var te = new PlayersTransformPacket.TransformEntry();
                var ptp = new PlayersTransformPacket();
                for (var i = 0; i < GameSession.Instance.Players.Count; i++)
                {
                    te.Guid = GameSession.Instance.Players[i].ID;
                    //te.Position = GameSession.Instance.Players[i].Position;
                    //te.Rotation = GameSession.Instance.Players[i].Rotation;
                    te.Position = GameSession.Instance.Players[i].playerNetworkManager.networkPosition;
                    te.Rotation = GameSession.Instance.Players[i].playerNetworkManager.networkRotation;
                    ptp.Transforms.Add(te);
                }

                te.Guid = GameSession.Instance.LocalPlayer.ID;
                //te.Position = GameSession.Instance.LocalPlayer.Position;
                //te.Rotation = GameSession.Instance.LocalPlayer.Rotation;
                te.Position = GameSession.Instance.LocalPlayer.playerNetworkManager.networkPosition;
                te.Rotation = GameSession.Instance.LocalPlayer.playerNetworkManager.networkRotation;
                ptp.Transforms.Add(te);
                NetworkSession.Instance.SendAll(ptp, NetworkChannelType.UnreliableOrdered);
                _lastTransformSent = Time.UnscaledGameTime;
            }
        }
    }
}
