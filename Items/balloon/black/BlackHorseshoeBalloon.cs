using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TAPI;
using Terraria;

namespace LolHens.Items
{
    public class BlackHorseshoeBalloon : TornadoInABalloon
    {
        public override void Effects(Player player)
        {
            base.Effects(player);
            player.noFallDmg = true;
        }
    }
}