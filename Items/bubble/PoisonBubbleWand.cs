using System;
using Microsoft.Xna.Framework;

using TAPI;
using Terraria;

namespace LolHens.Items
{
    public class PoisonBubbleWand : LolHensItem
    {
        public int dustDelay = 0;
        public bool toxic = true;

        public override bool? UseItem(Player player)
        {
            base.UseItem(player);

            dustDelay = Math.Max(0, dustDelay - 1);
            if (dustDelay <= 0 && usedPercent > 0.2f)
            {
                dustDelay = 2;
                float x = player.Center.X + player.direction * ((float)Main.rand.Next(400) / 10 + 2);
                float y = player.Center.Y + usedPercent * 100 - (64 + 25);
                float velX = player.direction * (float)Main.rand.Next(20) / 10 * 1.6f;
                float velY = (float)Main.rand.Next(10) / 10 - 0.5f + (usedPercent * 100 - 50) / 20;
                Projectile.NewProjectile(x, y, velX, velY, ProjDef.byName["LolHens:PoisonBubble"].type, toxic ? item.damage : 0, toxic ? item.knockBack : 0, player.whoAmI);
            }
            return false;
        }
    }
}