using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace ElementalBoots.Items.Accessories.Wings.TerraWings
{
    [AutoloadEquip(EquipType.Wings)]
    class TrueAngelWings: Wings
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            glowing = true;

            wingTimeMax = 140;
        }
    }
}
