using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TAPI;
using Terraria;

namespace LolHens.Items
{
    public class FlamingNinjaGear : LolHensItem
    {
        public bool fiery = true;
        public int dustDelay = 0;
        public float brightness = 0.8f;


        public override void Effects(Player player)
        {
            player.knockbackMeleeModVanilla *= 2f;
            player.meleeSpeed += 0.09f;
            player.meleeDamage += 0.09f;
            player.magmaStone = true;

            player.blackBelt = true;
            player.dash = 1;
            player.spikedBoots += 2;

            if (fiery)
            {
                dustDelay = Math.Max(0, dustDelay - 1);
                if (dustDelay <= 0)
                {
                    dustDelay = 3;
                    int dust = Dust.NewDust(new Vector2(player.position.X, player.position.Y + player.height / 2.4f), player.width, player.height / 3, 6, 0f, 0f, 0, default(Color), 1.6f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity -= player.velocity * 0.5f;
                    if (player.velocity.X != 0)
                    {
                        for (int i = 0; i < (int)(Math.Abs(player.velocity.X) * 1.4f); i++)
                        {
                            dust = Dust.NewDust(new Vector2(player.position.X, player.position.Y + player.height / 2.4f), player.width, player.height / 3, 6, 0f, 0f, 0, default(Color), 1.6f);
                            Main.dust[dust].noGravity = true;
                            Main.dust[dust].velocity -= player.velocity * 0.5f;
                        }
                    }
                }
                // Lighting.AddLight((int)((player.position.X + player.width / 2) / 16f), (int)((player.position.Y + player.height / 1.5f) / 16f), brightness * 1f, brightness * 0.7f, brightness * 0.6f);
            }
        }
    }
}