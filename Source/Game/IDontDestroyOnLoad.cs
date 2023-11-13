using FlaxEngine;

namespace Game
{
    public class IDontDestroyOnLoad : Script
    {
        private bool once = true;

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (!once)
            {
                return;
            }

            once = false;

            SceneManager.instance.AddToDontDestroyOnLoad(Actor);
        }
    }
}