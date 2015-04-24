using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TAPI;
using TAPI.UIKit;
using Terraria;

using LolHens.Projectiles;

namespace LolHens.Items
{
    public class SpaceLaser : LolHensGun
    {
        private SpaceLaserRay proj; 

        public override void Init()
        {
            base.Init();
            bulletOrigin.X -= 2;
            bulletOrigin.Y -= 10;
            bulletOffset.X += 74;
            bulletOffset.Y -= 2;
        }

        public override void DrawItemSlotItem(ref SpriteBatch sb, ref ItemSlot slot, ref Item item, ref Texture2D texture, ref Color color, ref float scale)
        {
            scale *= 1.5f;
        }

        public override bool PreShootCustom(Player player, Vector2 position, Vector2 velocity, int projType) {
            if (proj == null || !proj.projectile.active)
            {
                return true;
            }
            else
            {
                proj.projectile.Center = position;
                proj.projectile.velocity = velocity * 0.01f;
                proj.projectile.timeLeft = 30;
                proj.UpdatePosition();
                return false;
            }
        }

        public override void PostShootCustom(Player player, Projectile projectile)
        {
            proj = projectile.AsLolHensProjectile() as SpaceLaserRay;
            proj.projectile.velocity.X *= 0.01f;
            proj.projectile.velocity.Y *= 0.01f;
        }
    }
}