using System.Drawing;
using BLL.Properties;

namespace BLL
{
    public class Pub
    {
        private const decimal _beerPrice = 2;

        public string WelcomeMessage
        {
            get
            {
                return PubResources.PubWelcomeMessage;
            }
        }

        public string Color => Colors.PubColor;

        public Bitmap Image => Images.Pub;

        public string PlayGame(Player player)
        {
            if (player.CurrentBudget >= _beerPrice && player.CurrentBeers < player.MaxBeers)
            {
                player.LoseMoney(_beerPrice);
                return player.BuyBeer();
            }
            else
                return player.ToDie() + " " + PubResources.PubPlayerLackOfMoneyPostscript;
        }

        public string LoseGame() => PubResources.PubLoseGame;

        public override string ToString() => PubResources.Pub;
    }
}
