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
        const float brightness = 0.4f;

        public override void Init()
        {
            //projectile.hurtsTiles = false;

            modBase.eventRegistry.Register((Event.OptionChanged e) => ShowLight(), this);

            ShowLight();
        }

        private void ShowLight()
        {
            projectile.light = modBase.options.GetBoolean("SpaceLaserLight") ? 0.9f : 0;
        }

        public override void AI()
        {
            projectile.rotation = (float)System.Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;

            //Lighting.AddLight((int)(projectile.Center.X / 16f), (int)(projectile.Center.Y / 16f), 0.4f * brightness, 0.8f * brightness, 0.4f * brightness);
        }
    }
}
