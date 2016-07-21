using System;
using System.Collections.Generic;
using ElementalBoots;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementalBoots.Items.Accessories.Tornado
{
    public class TornadoInABalloon : CompoundAccessory
    {
        public override bool Autoload(ref string name, ref string texture, IList<EquipType> equips)
        {
            equips.Add(EquipType.Balloon);

            return true;
        }

        public override void SetDefaults()
        {
            item.name = "Tornado in a Balloon";
            item.maxStack = 1;
            item.value = 1 * Value.GOLD;
            item.rare = 4;
            item.accessory = true;
            item.toolTip = "Allows the holder to double jump";
        }

        public override IList<Item> GetCompoundAccessories()
        {
            return new List<Item>
            {
                Defs.GetItem(ItemID.ShinyRedBalloon),
                mod.GetItem("TornadoInABottle").item
            };
        }
    }
}