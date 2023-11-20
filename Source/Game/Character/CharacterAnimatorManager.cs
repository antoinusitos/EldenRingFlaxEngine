using FlaxEngine;

namespace Game
{
    public class CharacterAnimationManager : Script
    {
        public AnimatedModel animatedModel = null;

        private AnimGraphParameter horizontalParameter = null;
        private AnimGraphParameter verticalParameter = null;

        public override void OnAwake()
        {
            base.OnAwake();
        }

        public override void OnStart()
        {
            base.OnStart();

            horizontalParameter = animatedModel.GetParameter("Horizontal");
            verticalParameter = animatedModel.GetParameter("Vertical");
        }

        public void UpdateAnimatorMovement(float horizontalValue, float verticalValue)
        {
            horizontalParameter.Value = horizontalValue;
            verticalParameter.Value = verticalValue;
        }
    }
}