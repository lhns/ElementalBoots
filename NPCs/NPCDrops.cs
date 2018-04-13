using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace ElementalBoots.NPCs
{
    class NPCDrops: GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            base.NPCLoot(npc);

            List<Drop> drops;
            ElementalBootsMod.instance.npcDrops.TryGetValue(npc.type, out drops);
            if (drops != null)
            {
                foreach (Drop drop in drops)
                {
                    drop.DropAt(npc.getRect());
                }
            }
        }
    }
}
