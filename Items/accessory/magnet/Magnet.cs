using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolHens.Items
{
    class Magnet : LolHensItem
    {
        public override void InitType(Type type)
        {
            if (type != typeof(Magnet)) return;

            modBase.eventRegistry.Register((Event.ChestGenerated e) => {
                if ((e.chestInfo.height == ChestInfo.Height.UNDERGROUND || e.chestInfo.height == ChestInfo.Height.CAVERN)
                    && (e.chestInfo.style == ChestInfo.Style.GOLD || e.chestInfo.style == ChestInfo.Style.GOLD_LOCKED)) e.chestInfo.AddLoot(item, 0.05f, true);
            });
        }

        public override void Effects(Terraria.Player player)
        {
            base.Effects(player);
            player.itemGrabRange += 200;
        }
    }
}
