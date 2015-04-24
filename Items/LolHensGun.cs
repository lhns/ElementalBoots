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
        public Vector2 bulletOffset = new Vector2(0, 0);
        public Vector2 bulletOrigin = new Vector2(0, 0);
        public float bulletXCorrection = 0;

        public Boolean noclip = false;
        public float bulletSpread = 0;
        public bool addPlayerVel = false;
        public Projectile projOverride = null;

        private bool cancelMana = false;
        public int mana = 0;

        public override bool ConsumeAmmo(Player p) { return false; }

        public override bool PreShoot(Player player, Vector2 position, Vector2 velocity, int projType, int damage, float knockback)
        {
            base.PreShoot(player, position, velocity, projType, damage, knockback);

            Vector2 holdOrigin = new Vector2(bulletOrigin.X * player.direction + bulletXCorrection, bulletOrigin.Y);

            Vector2 holdDirection = new Vector2(bulletOffset.X, bulletOffset.Y * player.direction).Rotate(new Vector2(velocity.X, velocity.Y).ToRotation(), new Vector2(0, 0));

            Vector2 newPos = new Vector2(position.X + holdOrigin.X + holdDirection.X, position.Y + holdOrigin.Y + holdDirection.Y);

            Vector2 newVel = new Vector2(velocity.X, velocity.Y).Rotate((Main.rand.NextFloat() - 0.5f) * bulletSpread);

            if (addPlayerVel) velocity = velocity + player.velocity;
            
            if (projOverride != null) projType = projOverride.type;

            Boolean cancel = false;

            Tile tile = Main.tile[(int)(newPos.X / 16f), (int)(newPos.Y / 16f)];

            if (!noclip && tile.active() &&  tile.collisionType != -1) {
                cancel = true;

                Main.PlaySound(0, (int)newPos.X, (int)newPos.Y, 1);
                for (int m = 0; m < 5; m++)
                {
                    int dustID = Dust.NewDust(newPos, 10, 10, 1, velocity.X * 0.1f, velocity.Y * 0.1f, 100, default(Color), 1.2f);
                }
            }

            if (!cancel && !PreShootCustom(player, newPos, newVel, projType)) cancel = true;

            if (!cancel)
            {
                int projectile = Projectile.NewProjectile(newPos.X, newPos.Y, newVel.X, newVel.Y, projType, damage, knockback, player.whoAmI, 0f, 0f);
                
                player.UseAmmo();

                PostShootCustom(player, Main.projectile[projectile]);
            }
            else
            {
                CancelMana();
            }

            if (cancelMana)
            {
                cancelMana = false;
                if (item.mana != 0) mana = item.mana;
                item.mana = 0;
            }
            else if (mana != 0)
            {
                item.mana = mana;
                mana = 0;
            }

            return false;
        }

        public virtual bool PreShootCustom(Player player, Vector2 position, Vector2 velocity, int projType) { return true; }

        public virtual void PostShootCustom(Player player, Projectile projectile) { }

        public void CancelMana()
        {
            cancelMana = true;
        }
    }
}
