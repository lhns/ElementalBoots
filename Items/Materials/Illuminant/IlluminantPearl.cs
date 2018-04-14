using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;

namespace ElementalBoots.Items.Materials.Illuminant
{
    class IlluminantPearl: MItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            const float dropChance = 0.0167f;

            ElementalBootsMod.instance.AddDrop(NPCID.IlluminantBat, item.type, dropChance);
            ElementalBootsMod.instance.AddDrop(NPCID.IlluminantSlime, item.type, dropChance);
            ElementalBootsMod.instance.AddDrop(NPCID.ChaosElemental, item.type, dropChance);
            ElementalBootsMod.instance.AddDrop(NPCID.EnchantedSword, item.type, dropChance);
        }
    }
}
