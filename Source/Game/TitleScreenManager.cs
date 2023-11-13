using FlaxEngine;
using FlaxEngine.GUI;

namespace Game
{
    public class TitleScreenManager : Script
    {
        public UIControl pressStartButton = null;

        public UIControl titleScreenMainMenu = null;
        public UIControl newGameStartButton = null;

        public UIControl joinGameButton = null;

        public NetworkManager networkManager = null;

        public override void OnEnable()
        {
            Button pressStartButtonTemp = (Button)pressStartButton.Control;
            pressStartButtonTemp.Clicked += PressStartButtonEvents;
            pressStartButtonTemp.Focus();

            Button newGameStartButtonTemp = (Button)newGameStartButton.Control;
            newGameStartButtonTemp.Clicked += StartNewGame;

            ((Button)joinGameButton.Control).Clicked += JoinGameButtonEvents;

            Screen.CursorVisible = true;
            Screen.CursorLock = CursorLockMode.None;
        }

        private void PressStartButtonEvents()
        {
            StartNetworkAsHost();
            pressStartButton.IsActive = false;
            titleScreenMainMenu.IsActive = true;
            Button newGameStartButtonTemp = (Button)newGameStartButton.Control;
            newGameStartButtonTemp.Focus();
        }

        public void StartNetworkAsHost()
        {
            networkManager.StartServer();
        }

        private void JoinGameButtonEvents()
        {
            networkManager.StartClient();
        } 

        public void StartNewGame()
        {
            WorldSaveGameManager.instance.LoadNewGame();
        }
    }
}