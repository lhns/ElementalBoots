using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementalBoots.Items.Accessories.Ninja
{
    [AutoloadEquip(EquipType.HandsOn)]
    class MagmaClimbingGear: CompoundAccessory
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            
            item.value = 12 * Value.GOLD;
            item.rare = 8;
        }

        public override IList<Item> GetCompoundAccessories()
        {
            return new List<Item>
            {
                ElementalBootsMod.instance.GetItemType(ItemID.FireGauntlet),
                ElementalBootsMod.instance.GetItemType(ItemID.TigerClimbingGear)
            };
        }
    }
}
