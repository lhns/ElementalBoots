using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementalBoots.Items.Accessories.Wings.LaserWings
{
    abstract class LaserWings : Wings
    {
        protected static float[] lightR = new float[] { 0.900f, 0.640f, 0.165f, 0.020f, 0.784f, 0.650f };
        protected static float[] lightG = new float[] { 0.000f, 0.050f, 0.500f, 0.650f, 0.650f, 0.650f };
        protected static float[] lightB = new float[] { 0.030f, 0.830f, 0.980f, 0.020f, 0.035f, 0.720f };
        protected static float brightness = 0.8f;

        protected abstract float lightIndex();

        protected virtual int[] lightInterpTranslation()
        {
            return new int[] { 0, 1, 2, 3, 4, 5 };
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            wingTimeMax = 200;

            constantAscend *= 2;
            ascentWhenFalling = 5f;
            ascentWhenRising = 1f;
            maxAscentSpeedMultiplier *= 2;
            maxHorizontalSpeedMultiplier = 2;
            horizontalAccelerationMultiplier = 5;

            glowing = true;

            item.value = 1 * Value.GOLD;
            item.rare = 3;
        }

        protected int correspondingPhaseblade = -1;

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(correspondingPhaseblade);
            recipe.AddIngredient(ItemID.CrystalShard, 20);
            recipe.AddIngredient(ItemID.Lens, 20);
            recipe.AddIngredient(ItemID.MeteoriteBar, 10);
            recipe.AddIngredient(ItemID.SoulofFlight, 20);
            recipe.AddTile(mod, "InventorsWorkshop");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);

            int[] translation = lightInterpTranslation();
            float colorR = lightR.Interpolate(lightIndex(), translation) * brightness;
            float colorG = lightG.Interpolate(lightIndex(), translation) * brightness;
            float colorB = lightB.Interpolate(lightIndex(), translation) * brightness;
            Lighting.AddLight((int)(player.Center.X / 16f), (int)(player.Center.Y / 16f), colorR, colorG, colorB);
        }
    }

    static class LaserWingsExtensions
    {
        public static float Interpolate(this float[] array, float index, int[] translation = null)
        {
            int length = array.Length;
            if (translation == null)
            {
                translation = new int[array.Length];
                for (int i = 0; i < translation.Length; i++) translation[i] = i;
            }
            else
            {
                length = Math.Min(length, translation.Length);
            }
            int floor = (int)index;
            float interp = index - floor;
            float val1 = array[translation[floor % length]];
            float val2 = array[translation[(floor + 1) % length]];
            return val1 + interp * (val2 - val1);
        }
    }
}
