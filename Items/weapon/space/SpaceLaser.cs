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
        private const float fadeDelay = 10;

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
            return 50;
        }

        private Vector2 GetFadeVec(Vector2 velocity, int projType)
        {
            Vector2 fadeVec = new Vector2(velocity.X, velocity.Y);
            fadeVec.Normalize();

            fadeVec *= ((float)(time % fadeDelay)) / fadeDelay * ProjDef.byType[projType].Texture.Height;

            return fadeVec;
        }

        public override bool PreShootCustom(Player player, ref Vector2 position, ref Vector2 velocity, ref int projType, ref int damage, ref float knockback)
        {
            position += GetFadeVec(velocity, projType);

            time++;

            return base.PreShootCustom(player, ref position, ref velocity, ref projType, ref damage, ref knockback);
        }

        public override void PostShootCustom(Player player, Projectile projectile, Vector2 position, Vector2 velocity, int projType, int damage, float knockback)
        {
            projectile.velocity *= 0.001f;

            base.PostShootCustom(player, projectile, position, velocity, projType, damage, knockback);

            Vector2 fadeVec = GetFadeVec(velocity, projType);

            Projectile.NewProjectile(position.X - fadeVec.X,
                position.Y - fadeVec.Y,
                projectile.velocity.X,
                projectile.velocity.Y,
                ProjDef.byName["LolHens:SpaceLaserNozzle"].type,
                damage,
                knockback,
                player.whoAmI,
                0f, 0f);
        }
    }
}