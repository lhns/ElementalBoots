using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace ElementalBoots.Items.Weapons
{
    abstract class Gun: Weapon
    {
        public Vector2 holdoutOffset = Vector2.Zero;
        public Vector2 shootOrigin = Vector2.Zero;
        public Vector2 shootOffset = Vector2.Zero;
        public float rotationOffset = 0;
        public bool relativeVelocity = false;

        public float bulletSpread = 0;
        public Boolean noclip = false;

        public override Vector2? HoldoutOffset()
        {
            return holdoutOffset;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            var newShootOrigin = new Vector2(shootOrigin.X * player.direction, shootOrigin.Y);

            var newShootOffset = new Vector2(shootOffset.X, shootOffset.Y * player.direction).RotatedBy(new Vector2(speedX, speedY).ToRotation() + rotationOffset * player.direction);

            var newPosition = new Vector2(position.X + newShootOrigin.X + newShootOffset.X, position.Y + newShootOrigin.Y + newShootOffset.Y);

            var newVelocity = new Vector2(speedX, speedY).RotatedBy((Main.rand.NextFloat() - 0.5f) * bulletSpread);

            if (relativeVelocity) newVelocity = newVelocity + player.velocity;

            position = newPosition;
            speedX = newVelocity.X;
            speedY = newVelocity.Y;

            var cancel = false;

            Tile tile = Main.tile[(int)(newPosition.X / 16f), (int)(newPosition.Y / 16f)];

            if (!noclip && tile.active() && tile.collisionType != -1)
            {
                cancel = true;

                Main.PlaySound(0, (int)newPosition.X, (int)newPosition.Y, 1);
                for (int m = 0; m < 5; m++)
                {
                    int dustID = Dust.NewDust(newPosition, 10, 10, 1, newVelocity.X * 0.1f, newVelocity.Y * 0.1f, 100, default(Color), 1.2f);
                }
            }

            if (!cancel) UseAmmo(player);

            return !cancel;
        }

        public void UseAmmo(Player player)
        {
            consumeAmmo = true;
            var shoot = item.shoot;
            var speed = item.shootSpeed;
            var canShoot = true;
            var damage = item.damage;
            var knockBack = item.knockBack;
            player.PickAmmo(item, ref shoot, ref speed, ref canShoot, ref damage, ref knockBack);
        }

        private bool consumeAmmo = false;

        public override bool ConsumeAmmo(Player player)
        {
            if (consumeAmmo)
            {
                consumeAmmo = false;
            }
            else
            {
                return false;
            }

            return base.ConsumeAmmo(player);
        }
    }
}
