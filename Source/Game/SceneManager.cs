using FlaxEngine;
using System;

namespace Game
{
    public class SceneManager : Script
    {
        public static SceneManager instance = null;

        public Scene currentUsedScene = null;
        public Scene dontDestroyOnLoadScene = null;

        public override void OnStart()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(Actor);
            }
            Level.SceneLoaded += Level_SceneLoaded;
        }

        private void Level_SceneLoaded(Scene scene, System.Guid Guid)
        {
            if(scene != null)
            {
                currentUsedScene = scene;
            }
        }

        public void LoadScene(SceneReference sceneReference)
        {
            Level.UnloadScene(currentUsedScene);
            Level.LoadSceneAsync(sceneReference);
            //Level.UnloadScene(currentScene);
            //Level.LoadScene(sceneReference);
        }

        public void LoadScene(Guid sceneReference)
        {
            Level.UnloadScene(currentUsedScene);
            Level.LoadSceneAsync(sceneReference);
            //Level.UnloadScene(currentScene);
            //Level.LoadScene(sceneReference);
        }

        public void AddToDontDestroyOnLoad(Actor actor)
        {
            actor.SetParent(dontDestroyOnLoadScene, true);
        }
    }
}