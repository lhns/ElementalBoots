using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TAPI;
using Terraria;

namespace LolHens.Items
{
    public class TornadoInABalloon : TornadoInABottle
    {
        public override void Effects(Player player)
        {
            base.Effects(player);
            player.jumpBoost = true;
        }
    }
}