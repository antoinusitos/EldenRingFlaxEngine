using FlaxEngine;
using System;
using System.Collections.Generic;

namespace Game
{
    public class SceneManager : Script
    {
        public static SceneManager instance = null;

        public Scene currentUsedScene = null;
        public Scene dontDestroyOnLoadScene = null;

        private List<Actor> actorsToMove = new List<Actor>();

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
        }

        public void LoadScene(Guid sceneReference)
        {
            Level.UnloadScene(currentUsedScene);
            Level.LoadSceneAsync(sceneReference);
        }

        public void AddToDontDestroyOnLoad(Actor actor)
        {
            actorsToMove.Add(actor);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if(actorsToMove.Count > 0)
            {
                actorsToMove[0].SetParent(dontDestroyOnLoadScene, true);
                actorsToMove.RemoveAt(0);
            }
        }
    }
}