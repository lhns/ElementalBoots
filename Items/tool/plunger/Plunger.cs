using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using TAPI;
using TAPI.UIKit;

namespace LolHens.Items
{
    public class Plunger : LolHensItem
    {
        public override void InitType(Type type)
        {
            if (type != typeof(Plunger)) return;

            Events.registry.Register((Events.ChestGenerated e) =>
            {
                if (e.chestInfo.height == ChestInfo.Height.CAVERN
                    && (e.chestInfo.style == ChestInfo.Style.GOLD || e.chestInfo.style == ChestInfo.Style.GOLD_LOCKED)) e.chestInfo.AddLoot(item, 0.05f, true);
            });
        }

        // TODO: add climbing abilities
    }
}
