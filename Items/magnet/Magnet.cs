using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolHens.Items
{
    class Magnet : LolHensItem
    {
        public override void Effects(Terraria.Player player)
        {
            base.Effects(player);
            player.itemGrabRange += 200;
        }
    }
}
