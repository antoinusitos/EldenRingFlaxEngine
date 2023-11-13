using FlaxEngine;

namespace Game
{
    public class PlayerUIManager : IDontDestroyOnLoad
    {
        public static PlayerUIManager instance = null;

        [Header("NETWORK JOIN")]
        public bool startGameAsClient = false;

        public override void OnStart()
        {
            base.OnStart();

            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(Actor);
            }
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if(startGameAsClient)
            {
                startGameAsClient = false;
                Debug.Log("lol10");
                NetworkSession.Instance.Disconnect();
                NetworkManager.instance.StartClient();
            }
        }
    }
}