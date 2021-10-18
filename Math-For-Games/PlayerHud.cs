using System;
using System.Collections.Generic;
using System.Text;

namespace Math_For_Games
{
    class PlayerHud : Actor
    {
        //The player that will have their stats displayed
        private Player _player;
        private UIText _element;

        //Takes in the player and UIText element that will be displayed
        public PlayerHud(Player player, UIText element)
        {
            _player = player;
            _element = element;
        }

        public override void Start()
        {
            base.Start();
            _element.Start();
        }

        public override void Update(float deltaTime)
        {
            _element.Text = $"This is where you display the player's stat";
        }

        public override void Draw()
        {
            _element.Draw();
        }
    }
}
