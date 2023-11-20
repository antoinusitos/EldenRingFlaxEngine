using FlaxEngine;
using System;

namespace Game
{
    public class DemoSceneScript : Script
    {
        private GameSession _game;
        public Prefab PlayerPrefab;

        public override void OnEnable()
        {
            _game = GameSession.Instance;
            _game.OnPlayerAdded += OnPlayerAdded;
            _game.OnPlayerRemoved += OnPlayerRemoved;
            for (var i = 0; i < _game.Players.Count; i++)
            {
                OnPlayerAdded(_game.Players[i]);
            }
        }

        public void OnPlayerAdded(Player player)
        {
            Debug.Log("OnPlayerAdded");
            Actor spawnedPlayer = PrefabManager.SpawnPrefab(PlayerPrefab, Actor);
            PlayerNetworkManager playerNetworkManager = spawnedPlayer.GetScript<PlayerNetworkManager>();
            playerNetworkManager.isOwner = false;
            GameSession.Instance.AffectPlayerNetworkManager(playerNetworkManager, player.ID);
            spawnedPlayer.Name = "Player_" + player.Name;
            Random rand = new Random();
            spawnedPlayer.Position = new Vector3(rand.Next(-100, 100), rand.Next(-100, 100), 0);
        }

        public void OnPlayerRemoved(Player player)
        {
            Debug.Log("OnPlayerRemoved");
            Destroy(player.playerNetworkManager.Actor);
        }

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
    }
}
