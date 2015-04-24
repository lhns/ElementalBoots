using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TAPI;
using Terraria;

namespace LolHens.Items
{
    public class BandOfStamina : LolHensItem
    {
        public override void Effects(Player player)
        {
            base.Effects(player);
            if (time > 4)
            {
                for (int i = 0; i < (int)(Math.Abs(player.velocity.X) * 1.4f); i++)
                {
                    int dust = Dust.NewDust(new Vector2(player.position.X, player.position.Y + player.height / 2.4f), player.width, player.height / 3, 6, 0f, 0f, 0, default(Color), 1.6f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity -= player.velocity * 0.5f;
                }
            }
        }
    }
}