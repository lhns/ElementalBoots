using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using Terraria;

namespace LolHens.Items
{
    public abstract class LolHensRayGun : LolHensGun
    {
        protected Projectile[] ray;
        
        public Boolean rayUseAmmo = false;
        public Boolean rayTileCollision = false;

        public LolHensRayGun() : base()
        {
            ray = new Projectile[GetRayLength()];
        }

        public abstract int GetRayLength();

        public virtual Vector2 GetProjectileRotation(Projectile projectile)
        {
            return new Vector2(projectile.velocity.X, projectile.velocity.Y);
        }

        public virtual int GetProjectileRadius(Projectile projectile)
        {
            return projectile.Texture.Height;
        }

        public virtual bool PrePlaceRayProjectile(Projectile projectile)
        {
            if (rayTileCollision)
            {
                Tile tile = Main.tile[(int)(projectile.Center.X / 16f), (int)(projectile.Center.Y / 16f)];
                if (tile.active() && tile.collisionType != -1) return false;
            }
            return true;
        }

        public override void PostShootCustom(Player player, Projectile projectile, Vector2 position, Vector2 velocity, int projType, int damage, float knockback)
        {
            ray[0] = projectile;

            for (int i = 1; i < ray.Length; i++)
            {
                if (!rayUseAmmo || player.UseAmmo())
                    ray[i] = Main.projectile[Projectile.NewProjectile(position.X, position.Y, 0, 0, projType, damage, knockback, player.whoAmI, 0f, 0f)];
            }

            if (rayUseAmmo) manaMultiplier *= ray.Length;

            UpdatePosition();
        }

        public void UpdatePosition()
        {
            if (ray[0] == null || !ray[0].active) return;

            float radius = ray[0].Texture.Height;

            ray[0].rotation = (float) + 1.57f;

            Vector2 offset = GetProjectileRotation(ray[0]);
            offset.Normalize();

            bool cancelled = false;
            
            for (int i = 1; i < ray.Length; i++)
            {
                if (ray[i] == null || !ray[i].active)
                {
                    cancelled = true;
                    continue;
                }

                if (cancelled)
                {
                    ray[i].Kill();
                    continue;
                }

                Vector2 pos = offset * GetProjectileRadius(ray[i - 1]);
                pos.X += ray[i - 1].Center.X;
                pos.Y += ray[i - 1].Center.Y;

                ray[i].Center = pos;
                ray[i].velocity = ray[0].velocity;
                ray[i].rotation = ray[0].rotation;

                if (!PrePlaceRayProjectile(ray[i]))
                {
                    ray[i].Kill();
                    cancelled = true;
                    continue;
                }
            }
        }
    }
}
