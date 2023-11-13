using FlaxEngine;
using System;

namespace Game
{
    public class WorldSaveGameManager : IDontDestroyOnLoad
    {
        public static WorldSaveGameManager instance = null;

        public SceneReference GameScene;

        public override void OnStart()
        {
            base.OnStart();

            if (instance != null)
            {
                Destroy(Actor);
            }
            else
            {
                instance = this;
            }
        }

        public void LoadNewGame()
        {
            SceneManager.instance.LoadScene(GameScene);
        }

        public Guid GetWorldSceneIndex()
        {
            return GameScene.ID;
        }
    }
}