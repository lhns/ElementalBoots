using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using Terraria;
using TAPI;

namespace LolHens.NPCs
{
    public class FrostSlime : LolHensPet
    {
        private const int damage = 40;
        private const int knockback = 2;

        public bool ability = true;

        int timer = 0;

        public override void Init()
        {
            base.Init();

            petBuff = BuffDef.byName["LolHens:FrostSlimeBuff"];
            teleportDistance = 2000;

            modBase.eventRegistry.Register((Event.EntityDamaged e) =>
            {
                if (e.victim == owner)
                {
                    timer = 140;
                }
                else if (e.attacker == owner)
                {
                    timer = Math.Max(timer, 60);
                }
            }, this);
        }

        public override void HitEffect(int hitDirection, double damage, bool isDead)
        {
            timer = Math.Max(timer, 80);
        }

        public override void AI()
        {
            base.AI();

            if (npc.active)
            {
                if (npc.velocity.X == 0)
                {
                    if (npc.position.X > owner.position.X)
                    {
                        npc.direction = -1;
                    }
                    else
                    {
                        npc.direction = 1;
                    }
                }
                npc.spriteDirection = npc.direction;

                ShootProjectiles();
            }
        }

        public override bool Teleport(Vector2 position)
        {
            for (int m = 0; m < 10; m++)
            {
                int dustID = Dust.NewDust(position, npc.width, npc.height, 59, (Main.rand.Next() - 0.5f), (Main.rand.Next() - 0.5f), 100, Color.White, 1.2f);
            }
            return true;
        }

        const int projChance = 5;
        const int soundChance = 2;
        const int soundEffect = 17;

        private void ShootProjectiles()
        {
            if (!ability || timer <= 0) return;
            timer--;

            float vel;

            vel = WorldGen.genRand.NextFloat() * 0.8f + 0.2f;
            if (WorldGen.genRand.Next(projChance) == 0)
            {
                ProjDef.byName["LolHens:FrostProjectile"].New(entity.Center.X + 14, entity.Center.Y + 1, 8f * vel, -8f * vel, damage, knockback, owner.whoAmI);
                if (WorldGen.genRand.Next(soundChance) == 0) Main.PlaySound(2, (int)npc.Center.X, (int)npc.Center.Y, soundEffect);
            }

            vel = WorldGen.genRand.NextFloat() * 0.8f + 0.2f;
            if (WorldGen.genRand.Next(projChance) == 0)
            {
                ProjDef.byName["LolHens:FrostProjectile"].New(entity.Center.X - 6, entity.Center.Y - 2, -8f * vel, -8f * vel, damage, knockback, owner.whoAmI);
                if (WorldGen.genRand.Next(soundChance) == 0) Main.PlaySound(2, (int)npc.Center.X, (int)npc.Center.Y, soundEffect);
            }
        }
    }
}
