using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TAPI;
using Terraria;

namespace LolHens.Items
{
    public class NightskyNecklace : LolHensItem
    {

        public override void Effects(Player player)
        {
            player.accMerman = true;
            player.wolfAcc = true;

            player.starCloak = System.Math.Max(player.starCloak, 3);
            player.longInvince = true;
        }
    }
}