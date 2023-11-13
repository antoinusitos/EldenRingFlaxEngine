using FlaxEngine;

namespace Game
{
    public class CharacterManager : IDontDestroyOnLoad
    {
        public CharacterController characterController = null;

        public override void OnAwake()
        {
            base.OnAwake();

            Debug.Log("OnAwake CharacterManager");

            characterController = (CharacterController)Actor;
        }
    }
}