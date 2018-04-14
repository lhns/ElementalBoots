using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementalBoots.Items.Accessories.BalloonBundle
{
    abstract class BalloonBundle : Accessory
    {
        public bool Cloud, Blizzard, Sandstorm, Fart, Sail, Tornado;
        public bool Horseshoe, Obsidian, Honey;
        protected int[] upgradeOf, upgradeRequires;

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);

            player.jumpBoost = true;

            player.doubleJumpCloud = Cloud;
            player.doubleJumpBlizzard = Blizzard;
            player.doubleJumpSandstorm = Sandstorm;
            player.doubleJumpFart = Fart;
            player.doubleJumpSail = Sail;
            if (Tornado) player.GetModPlayer<MPlayer>(mod).ApplyAccessoryEffects(mod.GetItem("TornadoInABottle").item);
            if (Horseshoe) player.noFallDmg = true;
            if (Obsidian) player.fireWalk = true;
            if (Honey) player.bee = true;
        }

        public override void AddRecipes()
        {
            var balloons = 0;
            if (Cloud) balloons++;
            if (Blizzard) balloons++;
            if (Sandstorm) balloons++;
            if (Fart) balloons++;
            if (Sail) balloons++;
            if (Tornado) balloons++;

            ModRecipe recipe = new ModRecipe(mod);
            if (Cloud) recipe.AddIngredient(ItemID.CloudinaBalloon);
            if (Blizzard) recipe.AddIngredient(ItemID.BlizzardinaBalloon);
            if (Sandstorm) recipe.AddIngredient(ItemID.SandstorminaBalloon);
            if (Fart) recipe.AddIngredient(ItemID.FartInABalloon);
            if (Sail) recipe.AddIngredient(ItemID.BalloonPufferfish);
            if (Tornado) recipe.AddIngredient(mod, "TornadoInABalloon");
            if (Obsidian && Horseshoe) recipe.AddIngredient(ItemID.ObsidianHorseshoe);
            else if (Obsidian) recipe.AddIngredient(ItemID.ObsidianSkull);
            else if (Horseshoe) recipe.AddIngredient(ItemID.LuckyHorseshoe);
            if (Honey) recipe.AddIngredient(ItemID.HoneyComb);
            recipe.AddIngredient(ItemID.SoulofFlight, balloons * 5);
            recipe.AddTile(mod, "InventorsWorkshop");
            recipe.SetResult(this);
            recipe.AddRecipe();

            if (Cloud && Blizzard && Sandstorm)
            {
                recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.BundleofBalloons);
                if (Fart) recipe.AddIngredient(ItemID.FartInABalloon);
                if (Sail) recipe.AddIngredient(ItemID.BalloonPufferfish);
                if (Tornado) recipe.AddIngredient(mod, "TornadoInABalloon");
                if (Obsidian && Horseshoe) recipe.AddIngredient(ItemID.ObsidianHorseshoe);
                else if (Obsidian) recipe.AddIngredient(ItemID.ObsidianSkull);
                else if (Horseshoe) recipe.AddIngredient(ItemID.LuckyHorseshoe);
                if (Honey) recipe.AddIngredient(ItemID.HoneyComb);
                recipe.AddIngredient(ItemID.SoulofFlight, balloons * 5);
                recipe.AddTile(mod, "InventorsWorkshop");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }

            if (upgradeOf != null)
            {
                for (int i = 0; i < upgradeOf.Length; i++)
                {
                    recipe = new ModRecipe(mod);
                    recipe.AddIngredient(upgradeOf[i]);
                    recipe.AddIngredient(upgradeRequires[i]);
                    recipe.AddIngredient(ItemID.SoulofFlight, 5);
                    recipe.AddTile(mod, "InventorsWorkshop");
                    recipe.SetResult(this);
                    recipe.AddRecipe();
                }
            }
        }
    }
}