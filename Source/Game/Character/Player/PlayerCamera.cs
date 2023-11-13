using FlaxEngine;

namespace Game
{
    public class PlayerCamera : IDontDestroyOnLoad
    {
        public static PlayerCamera instance = null;

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
    }
}