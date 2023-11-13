using FlaxEngine;

namespace Game
{
    public class NetworkManager : IDontDestroyOnLoad
    {
        public static NetworkManager instance = null;

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
        }

        public void StartServer()
        {
            if(!NetworkSession.Instance.Host("ServerName", 7777))
            {
                Debug.Log("Server Created");

            }
            else
            {
                Debug.Log("Error : Failed to create a Server");
            }
        }

        public void StartClient()
        {
            if(!NetworkSession.Instance.Connect("ClientName", "127.0.0.1", 7777))
            {
                Debug.Log("Server Joined");
            }
            else
            {
                Debug.Log("Error : Failed to join a Server");
            }
        }
    }
}