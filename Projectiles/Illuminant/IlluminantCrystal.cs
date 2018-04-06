using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace ElementalBoots.Projectiles.Illuminant
{
    public class IlluminantCrystal: MProjectile
    {
        public override void SetDefaults()
        {
            projectile.aiStyle = 0;
            projectile.timeLeft = 800;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = true;
            projectile.ranged = true;
            projectile.penetrate = 10;
            projectile.magic = true;
        }

        public override void AI()
        {
            int nearest = -1;
            float nearestDistance = 0;
            Vector2 distanceVec;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].active
                    && !Main.npc[i].dontTakeDamage
                    && !Main.npc[i].friendly
                    && Main.npc[i].lifeMax > 5
                    && Collision.CanHit(projectile.position, projectile.width, projectile.height, Main.npc[i].position, Main.npc[i].width, Main.npc[i].height))
                {
                    distanceVec = new Vector2(Main.npc[i].Center.X - projectile.Center.X, Main.npc[i].Center.Y - projectile.Center.Y);
                    float distance = (float)System.Math.Sqrt((double)(distanceVec.X * distanceVec.X + distanceVec.Y * distanceVec.Y));
                    if (nearest == -1 || distance < nearestDistance)
                    {
                        nearest = i;
                        nearestDistance = distance;
                    }
                }
            }

            if (nearest > -1)
            {
                Vector2 velocity = new Vector2(Main.npc[nearest].Center.X - projectile.Center.X, Main.npc[nearest].Center.Y - projectile.Center.Y);
                float hyp = (float)System.Math.Sqrt((double)(velocity.X * velocity.X + velocity.Y * velocity.Y));
                float norm = 15f / hyp;
                velocity.X *= norm;
                velocity.Y *= norm;
                projectile.velocity.X = (projectile.velocity.X + velocity.X) / 2;
                projectile.velocity.Y = (projectile.velocity.Y + velocity.Y) / 2;
            }

            projectile.rotation = (float)System.Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 0.9f;

            Lighting.AddLight((int)(projectile.Center.X / 16f), (int)(projectile.Center.Y / 16f), 0.9f * 0.5f, 0.5f * 0.5f, 0.95f * 0.5f);
        }

        public override void Kill(int timeLeft)
        {
            Collision.HitTiles(projectile.position, projectile.velocity, projectile.width, projectile.height);
            Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y, 1);
            for (int m = 0; m < 5; m++)
            {
                int dustID = Dust.NewDust(projectile.position, projectile.width, projectile.height, 27, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, Color.White, 1.2f);
            }
        }
    }
}