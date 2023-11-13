using FlaxEngine;

namespace Game
{
    public class PlayerInputManager : IDontDestroyOnLoad
    {
        public static PlayerInputManager instance = null;

        private bool forceFirstUpdate = true;

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
                Level.SceneLoaded += Level_SceneLoaded;
            }
        }

        public override void OnEnable()
        {
            base.OnEnable();

            if(forceFirstUpdate)
            {
                forceFirstUpdate = false;
                base.OnUpdate();
                instance.Enabled = false;
            }
        }

        private void Level_SceneLoaded(Scene scene, System.Guid sceneGUID)
        {
            if(WorldSaveGameManager.instance == null)
            {
                return;
            }

            if (sceneGUID == WorldSaveGameManager.instance.GetWorldSceneIndex())
            {
                Enabled = true;
            }
            else
            {
                Enabled = false;
            }
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            // Here you can add code that needs to be called every frame

            //Input.GetAxis("Horizontal")
        }

        public override void OnDestroy()
        {
            Level.SceneLoaded -= Level_SceneLoaded;
        }
    }
}