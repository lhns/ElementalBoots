using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TAPI;
using TAPI.UIKit;
using Terraria;

using LolHens.Projectiles;

namespace LolHens.Items
{
    public class SpaceLaser : LolHensRayGun
    {
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

        public override int GetRayLength()
        {
            return 20;
        }

        public override void PostShootCustom(Player player, Projectile projectile)
        {
            projectile.velocity *= 0.001f;

            base.PostShootCustom(player, projectile);

            CancelMana();
        }
    }
}