using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementalBoots;

namespace ElementalBoots.Items.Accessories.FrozenTurtleShield
{
    [AutoloadEquip(EquipType.Shield)]
    class FrozenTurtleShield: CompoundAccessory
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            
            item.value = 10 * Value.GOLD;
            item.rare = 8;
        }

        public override IList<Item> GetCompoundAccessories()
        {
            return new List<Item>
            {
                ElementalBootsMod.instance.GetItemType(ItemID.PaladinsShield),
                ElementalBootsMod.instance.GetItemType(ItemID.FrozenTurtleShell)
            };
        }
    }
}
