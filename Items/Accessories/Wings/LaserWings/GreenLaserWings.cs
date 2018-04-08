using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace ElementalBoots.Items.Accessories.Wings.LaserWings
{
    [AutoloadEquip(EquipType.Wings)]
    class GreenLaserWings : LaserWings
    {
        protected override float lightIndex()
        {
            return 3;
        }
    }
}
