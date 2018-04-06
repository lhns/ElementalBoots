﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace ElementalBoots.Items.Accessories.BalloonBundle
{
    [AutoloadEquip(EquipType.Balloon)]
    public class SextupleHoneyBalloonBundle : BalloonBundle
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            item.value = 8 * Value.GOLD;
            item.rare = 9;

            Cloud = Blizzard = Sandstorm = Tornado = Fart = Sail = true;
            Honey = true;
        }
    }
}
