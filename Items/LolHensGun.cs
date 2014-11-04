using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using Terraria;
using TAPI;


namespace LolHens.Items
{
    public class LolHensGun : LolHensItem
    {
        public int bulletOffset = 0;

        public override bool ConsumeAmmo(Player p) { return bulletOffset == 0; }

        public override bool PreShoot(Terraria.Player player, Vector2 position, Vector2 velocity, int projType, int damage, float knockback)
        {
            if (bulletOffset == 0) return true;
            Vector2 direction = new Vector2(velocity.X, velocity.Y);
            direction.Normalize();
            direction *= bulletOffset;
            Vector2 newPos = new Vector2(position.X + direction.X, position.Y + direction.Y);
            Tile tile = Main.tile[(int)(newPos.X / 16f), (int)(newPos.Y / 16f)];
            if (!tile.active() || tile.collisionType == -1)
            {
                int projectile = Projectile.NewProjectile(newPos.X, newPos.Y, velocity.X, velocity.Y, projType, damage, knockback, player.whoAmI, 0f, 0f);
                player.UseAmmo();
            }
            else
            {
                Main.PlaySound(0, (int)newPos.X, (int)newPos.Y, 1);
                for (int m = 0; m < 5; m++)
                {
                    int dustID = Dust.NewDust(newPos, 10, 10, 1, velocity.X * 0.1f, velocity.Y * 0.1f, 100, default(Color), 1.2f);
                }
            }
            return false;
        }
    }
}
