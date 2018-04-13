using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Microsoft.Xna.Framework;

namespace ElementalBoots.Items.Weapons.Extinguisher
{
    class Extinguisher: Gun
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            item.value = Value.SILVER * 10;
            item.rare = 1;
            item.useStyle = 5;
            item.useAnimation = 15;
            item.accessory = true;
            item.useTime = 1;
            item.damage = 2;
            item.knockBack = 1;
            item.UseSound = new LegacySoundStyle(2, 13);
            item.autoReuse = true;
            item.noMelee = true;
            item.ranged = true;
            item.magic = true;
            item.shoot = mod.ProjectileType("Smoke");
            item.shootSpeed = 4;
            item.mana = 2;

            relativeVelocity = true;
            bulletSpread = 1.2f;
            relativeShootOffset = new Vector2(24, -16);
            absoluteShootOffset = new Vector2(-5, -4);
            xOffset = -5;
        }

        public override void OnChestGenerated(ChestInfo chestInfo)
        {
            base.OnChestGenerated(chestInfo);

            if ((chestInfo.height == ChestInfo.Height.UNDERGROUND || chestInfo.height == ChestInfo.Height.CAVERN)
                    && (chestInfo.style == ChestInfo.Style.GOLD || chestInfo.style == ChestInfo.Style.GOLD_LOCKED))
                chestInfo.AddLoot(item, 0.05f, true);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);

            player.fireWalk = true;
        }
    }
}
