using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TAPI;
using Terraria;

namespace LolHens.Items
{
    public class GiantBalloonBundle : TornadoInABalloon
    {
        public int slowFallLeft = 0;

        public override void Effects(Player player)
        {
            base.Effects(player);
            player.doubleJump = true;
            player.doubleJump2 = true;
            player.doubleJump3 = true;
            player.doubleJump4 = true;

            if (player.velocity.Y == 0) slowFallLeft = 0;
            if (slowFallLeft > 0) player.fallStart = (int)(player.position.Y / 16f);
            if (player.gravDir == -1f)
            {
                if (player.velocity.Y < -2f && slowFallLeft > 0)
                {
                    slowFallLeft--;
                    player.velocity.Y = -2f;
                }
            }
            else
            {
                if (player.velocity.Y > 2f && slowFallLeft > 0)
                {
                    slowFallLeft--;
                    player.velocity.Y = 2f;
                }
            }
        }
    }
}