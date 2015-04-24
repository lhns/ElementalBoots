using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using TAPI;
using Terraria;

namespace LolHens.Projectiles
{
    public class SpaceLaserRay : LolHensProjectile
    {
        private int length = 60;
        private float offset = 0;
        public SpaceLaserRay child;
        public SpaceLaserRay parent;
        public Boolean beam = false;

        public override void Init()
        {
            projectile.hurtsTiles = false;
            //projectile.velocity.X *= 0.01f;
            //projectile.velocity.Y *= 0.01f;
        }

        public override void AI()
        {
            UpdatePosition();

            if ((child == null || !child.projectile.active) && length > 0)
            {
                child = ProjDef.byName["LolHens:SpaceLaserRayBeam"].New(0, 0, projectile.velocity.X, projectile.velocity.Y, projectile.damage, projectile.knockBack, 0) as SpaceLaserRay;
                child.length = length - 1;
                child.beam = true;
                child.parent = this;
                child.UpdatePosition();
            }
        }

        public void UpdatePosition()
        {
            if (parent != null)
            {
                projectile.timeLeft = parent.projectile.timeLeft;
                projectile.rotation = parent.projectile.rotation;
                projectile.velocity.X = parent.projectile.velocity.X;
                projectile.velocity.Y = parent.projectile.velocity.Y;

                float radius = projectile.Texture.Height;

                if (parent != null && !parent.beam)
                {
                    radius *= 0.1f + (offset % 1);
                }

                Vector2 pos = new Vector2(radius, 0).Rotate(projectile.velocity.ToRotation());

                pos.X += parent.projectile.Center.X;
                pos.Y += parent.projectile.Center.Y;

                projectile.Center = pos;
            }
            else
            {
                projectile.rotation = (float)System.Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;

                if (child != null)
                {
                    child.offset += 0.02f;
                }
            }
        }

        public virtual bool OnTileCollide(ref Vector2 velocityChange)
        {
            projectile.Kill();

            return true;
        }
    }
}
