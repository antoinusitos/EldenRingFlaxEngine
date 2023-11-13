using FlaxEngine;
using FlaxEngine.GUI;

namespace Game
{
    public class NetworkPlayerScript : Script
    {
        public Player Player;

        public override void OnEnable()
        {
            base.OnEnable();

            //Actor.GetScript<LocalPlayerScript>().Enabled = false;
        }

        /// <inheritdoc/>
        public override void OnUpdate()
        {
            if(Player == null)
            {
                //Actor.GetScript<LocalPlayerScript>().Enabled = true;
                Enabled = false;
                return;
            }

            // Sync actor transform
            var trans = Actor.Transform;
            trans.Translation = Vector3.Lerp(trans.Translation, Player.Position, 0.4f);
            trans.Orientation = Quaternion.Lerp(trans.Orientation, Player.Rotation, 0.4f);
            Actor.Transform = trans;

            // Sync actor name
            /*var label = Actor.FindActor<UIControl>();
            label.Get<Label>().Text = Player.Name;
            Actor.Name = "Player_" + Player.Name;*/
        }
    }
}
