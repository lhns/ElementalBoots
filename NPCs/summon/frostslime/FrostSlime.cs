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
    public class FrostSlime: LolHensPet 
    {
        public override void Init()
        {
            base.Init();
            petBuff = BuffDef.byName["LolHens:FrostSlime"];
            teleportDistance = 2000;
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
            }
        }

        public override bool Teleport(Vector2 position) {
            for (int m = 0; m < 10; m++)
            {
                int dustID = Dust.NewDust(position, npc.width, npc.height, 59, (Main.rand.Next() - 0.5f), (Main.rand.Next() - 0.5f), 100, Color.White, 1.2f);
            }
            return true;
        }
    }
}
