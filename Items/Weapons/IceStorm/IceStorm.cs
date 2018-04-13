using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Audio;

namespace ElementalBoots.Items.Weapons.IceStorm
{
    class IceStorm: Weapon
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            item.value = Value.SILVER * 10;
            item.rare = 4;

            item.useStyle = 4;
            item.useAnimation = 10;
            item.useTime = 10;
            item.UseSound = new LegacySoundStyle(2, 9);

            item.damage = 35;
            item.knockBack = 10;
            item.autoReuse = true;
            item.noMelee = true;
            item.magic = true;
            item.shoot = mod.ProjectileType("IceCrystal");
            item.shootSpeed = 2;
            item.mana = 2;
        }
    }
}
