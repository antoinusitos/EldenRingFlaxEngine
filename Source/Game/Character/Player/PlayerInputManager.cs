using FlaxEngine;
using System;

namespace Game
{
    public class PlayerInputManager : IDontDestroyOnLoad
    {
        public static PlayerInputManager instance = null;

        private bool forceFirstUpdate = true;

        private Vector2 movementInput = Vector2.Zero;
        public float verticalInput = 0f;
        public float horizontalInput = 0f;
        public float moveAmount = 0f;

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

            HandleMovementInput();
        }

        public override void OnDestroy()
        {
            Level.SceneLoaded -= Level_SceneLoaded;
        }

        private void HandleMovementInput()
        {
            verticalInput = Input.GetAxis("Vertical");
            horizontalInput = Input.GetAxis("Horizontal");

            moveAmount = Mathf.Clamp(Mathf.Abs(verticalInput) + Mathf.Abs(horizontalInput), 0, 1);

            if(moveAmount <= 0.5f && moveAmount > 0)
            {
                moveAmount = 0.5f;
            }
            else if(moveAmount > 0.5f && moveAmount <= 1)
            {
                moveAmount = 1;
            }
        }
    }
}