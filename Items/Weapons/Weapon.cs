using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace ElementalBoots.Items.Weapons
{
    abstract class Weapon: MItem
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            item.maxStack = 1;
        }

        public int useTime = 0;

        public override bool UseItem(Player player)
        {
            var progress = (float)(++useTime) / (float)(item.useAnimation - 1);
            if (useTime >= item.useAnimation - 1) useTime = 0;

            return UseItem(player, progress);
        }

        public virtual bool UseItem(Player player, float progress)
        {
            return base.UseItem(player);
        }
    }
}
