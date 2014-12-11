using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;
using TAPI;

namespace LolHens.NPCs
{
    public class FrostSlime: ModNPC 
    {
        public override void AI()
        {
            bool active = false;
            if (npc.target > -1 && npc.target < Main.player.Length)
            {
                Player player = Main.player[npc.target];

                for (int i = 0; i < player.buffType.Length; i++)
                {
                    if (player.buffType[i] == BuffDef.byName["LolHens:FrostSlime"])
                    {
                        active = true;
                        player.buffTime[i] = 600;
                    }
                }

                if (active)
                {
                    if (npc.velocity.X == 0)
                    {
                        if (npc.position.X > player.position.X)
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
            if (!active) npc.active = false;
        }
    }
}
