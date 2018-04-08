using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace ElementalBoots.Items.Accessories.Wings.LaserWings
{
    [AutoloadEquip(EquipType.Wings)]
    class SpectrumWings : LaserWings
    {
        protected override int[] lightInterpTranslation()
        {
            return new int[] { 0, 1, 2, 3, 4 };
        }

        protected override float lightIndex()
        {
            return (GetLastEquippedTime() * 0.02f) % 5;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            wingTimeMax = 250;

            maxAscentSpeedMultiplier *= 1.2f;
            maxHorizontalSpeedMultiplier *= 1.2f;
        }
    }
}
