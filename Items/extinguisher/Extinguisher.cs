using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;

namespace LolHens.Items
{
    class Extinguisher : LolHensGun
    {
        public override void Init()
        {
            base.Init();
            bulletOffset.X = 30;
            bulletOffset.Y -= 20;
            bulletOrigin.Y += 18;
            bulletSpread = 1.2f;
            addPlayerVel = true;
        }

        public override void Effects(Player player)
        {
            base.Effects(player);
            player.fireWalk = true;
        }
    }
}
