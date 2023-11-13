using FlaxEngine;

namespace Game
{

    public class SplashScreen : Script
    {
        public SceneReference dontDestroyScene;
        public SceneReference mainMenuScene;

        private bool one = true;
        private bool two = true;
        private float timer = 0f;

        public override void OnUpdate()
        {
            base.OnUpdate();

            if(two)
            {
                two = false;
                Level.LoadScene(dontDestroyScene);
            }

            timer += Time.DeltaTime;

            if(timer < 1)
            {
                return;
            }

            if (!one)
            {
                return;
            }

            one = false;

            Level.LoadSceneAsync(mainMenuScene);
        }
    }
}