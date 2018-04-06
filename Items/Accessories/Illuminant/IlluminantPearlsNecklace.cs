using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace ElementalBoots.Items.Accessories.Illuminant
{
    class IlluminantPearlsNecklace : CompoundAccessory
    {
        public override void SetDefaults()
        {
            item.maxStack = 1;
            item.value = 30 * Value.GOLD;
            item.rare = 8;
            item.accessory = true;
        }

        public override IList<Item> GetCompoundAccessories()
        {
            return new List<Item>
            {
                mod.GetItem("IlluminantHeartNecklace").item,
                mod.GetItem("IlluminantStarNecklace").item
            };
        }
    }
}
