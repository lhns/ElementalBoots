using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace ElementalBoots.Projectiles
{
    abstract class MProjectile: ModProjectile
    {
        public void SetTexture(Texture2D texture)
        {
            Main.projectileTexture[projectile.type] = texture;
            projectile.width = texture.Width;
            projectile.height = texture.Height;
        }
    }
}