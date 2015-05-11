using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TAPI;
using Terraria;

namespace LolHens.Items
{
    public class LaserWings : ElementalWings
    {
        protected static float[] lightR = new float[] { 0.900f, 0.650f, 0.020f, 0.784f, 0.165f, 0.640f };
        protected static float[] lightG = new float[] { 0.000f, 0.650f, 0.650f, 0.650f, 0.500f, 0.050f };
        protected static float[] lightB = new float[] { 0.030f, 0.720f, 0.020f, 0.035f, 0.980f, 0.830f };
        protected static float brightness = 0.8f;

        public override void EffectsPre(Player player)
        {
            int itemType = getItemType();
            float colorR = lightR[itemType] * brightness;
            float colorG = lightG[itemType] * brightness;
            float colorB = lightB[itemType] * brightness;
            Lighting.AddLight((int)(player.Center.X / 16f), (int)(player.Center.Y / 16f), colorR, colorG, colorB);
        }

        private int getItemType()
        {
            String name = item.displayName.ToLower();
            if (name.StartsWith("red"))
            {
                return 0;
            }
            else if (name.StartsWith("white"))
            {
                return 1;
            }
            else if (name.StartsWith("green"))
            {
                return 2;
            }
            else if (name.StartsWith("yellow"))
            {
                return 3;
            }
            else if (name.StartsWith("blue"))
            {
                return 4;
            }
            else if (name.StartsWith("purple"))
            {
                return 5;
            }
            return -1;
        }
    }
}