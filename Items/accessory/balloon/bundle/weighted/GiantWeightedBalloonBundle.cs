using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TAPI;
using Terraria;

namespace LolHens.Items
{
    public class GiantWeightedBalloonBundle : TornadoInABalloon
    {

        public override void Effects(Player player)
        {
            base.Effects(player);
            player.doubleJump = true;
            player.doubleJump2 = true;
            player.doubleJump3 = true;
            player.doubleJump4 = true;
            player.noFallDmg = true;
        }
    }
}