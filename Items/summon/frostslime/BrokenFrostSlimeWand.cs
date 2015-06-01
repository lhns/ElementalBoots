using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;
using TAPI;
using LolHens.NPCs;

namespace LolHens.Items
{
    public class BrokenFrostSlimeWand : LolHensItem
    {
        public override void InitType(Type type)
        {
            if (type != typeof(BrokenFrostSlimeWand)) return;

            modBase.eventRegistry.Register((Event.ChestGenerated e) => {
                if (e.chestInfo.height == ChestInfo.Height.CAVERN
                    && (e.chestInfo.style == ChestInfo.Style.GOLD || e.chestInfo.style == ChestInfo.Style.GOLD_LOCKED)) e.chestInfo.AddLoot(item, 0.05f, true);
            });
        }

        public override bool? UseItem(Player player)
        {
            base.UseItem(player);

            (player.AddPet(NPCDef.byName["LolHens:FrostSlime"]) as FrostSlime).ability = false;
            return null;
        }
    }
}
