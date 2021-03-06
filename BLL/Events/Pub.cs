using System;
using System.Drawing;
using BLL.Properties;

namespace BLL.Events
{
    public class Pub
    {
        private const decimal _beerPrice = 2;
        private const int _defaultStep = 5;

        public string WelcomeMessage
        {
            get
            {
                return PubResources.PubWelcomeMessage;
            }
        }

        public string Color => Colors.PubColor;

        public Bitmap Image => Images.Pub;

        public int AccessToPub { get; set; } = _defaultStep;

        public int DefaultStep => _defaultStep;

        public string PlayGame(PlayerLogic player)
        {
            if(player is null)
                throw new ArgumentNullException(nameof(player), "The player value cannot be null.");
            
            if (player.CurrentBudget >= _beerPrice && player.CurrentBeers < player.MaxBeers)
            {
                player.LoseMoney(_beerPrice);
                return player.BuyBeer();
            }
            else
                return PubResources.PubPlayerLackOfMoneyPostscript;
        }

        public string LoseGame() => PubResources.PubLoseGame;

        public override string ToString() => PubResources.Pub;

        public void Reset()
        {
            AccessToPub = _defaultStep;
        }
    }
}
