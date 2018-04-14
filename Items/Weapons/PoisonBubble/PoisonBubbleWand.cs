using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementalBoots.Items.Weapons.PoisonBubble
{
    class PoisonBubbleWand: Weapon
    {
        private int dustDelay = 0;

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.value = Value.SILVER * 10;
            item.rare = 7;

            item.useStyle = 1;
            item.useAnimation = 28;
            item.useTime = 28;
            item.damage = 50;
            item.knockBack = 2;
            item.autoReuse = true;
            item.noMelee = true;
            item.magic = true;
            item.mana = 5;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BubbleWand);
            recipe.AddIngredient(ItemID.PoisonStaff);
            recipe.AddTile(mod, "InventorsWorkshop");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool UseItem(Player player, float progress)
        {
            dustDelay++;
            if (dustDelay > 2 && progress > 0.2f)
            {
                dustDelay = 0;
                float x = player.Center.X + player.direction * ((float)Main.rand.Next(400) / 10 + 2);
                float y = player.Center.Y + progress * 100 - (64 + 25);
                float velX = player.direction * (float)Main.rand.Next(20) / 10 * 1.6f;
                float velY = (float)Main.rand.Next(10) / 10 - 0.5f + (progress * 100 - 50) / 20;
                Projectile.NewProjectile(x, y, velX, velY, mod.ProjectileType("PoisonBubble"), item.damage, item.knockBack, player.whoAmI);
            }
            return false;
        }
    }
}
