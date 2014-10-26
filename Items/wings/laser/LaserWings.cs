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
        protected string[] typeTexture = new string[]{
			"Items/wings/laser/LaserWingsPlayer0",
			"Items/wings/laser/LaserWingsPlayer1",
			"Items/wings/laser/LaserWingsPlayer2",
			"Items/wings/laser/LaserWingsPlayer3",
			"Items/wings/laser/LaserWingsPlayer4",
			"Items/wings/laser/LaserWingsPlayer5",
			"Items/wings/laser/SpectrumWingsPlayer"
		};
        //protected float[] lightR = new float[]{0.9f,	0.64f,	0.165f,	0.02f,	0.784f,	0.65f};
        //protected float[] lightG = new float[]{0f,		0.05f,	0.5f,	0.65f,	0.65f,	0.65f};
        //protected float[] lightB = new float[]{0.03f,	0.83f,	0.98f,	0.02f,	0.035f,	0.72f};

        protected float[] lightR = new float[] { 0.900f, 0.650f, 0.020f, 0.784f, 0.165f, 0.640f };
        protected float[] lightG = new float[] { 0.000f, 0.650f, 0.650f, 0.650f, 0.500f, 0.050f };
        protected float[] lightB = new float[] { 0.030f, 0.720f, 0.020f, 0.035f, 0.980f, 0.830f };
        protected int[] lightTrans = new int[] { 0, 5, 4, 2, 3 };
        private float time = 0;


        public override void InitTextures()
        {
            texture = modBase.textures[typeTexture[getItemType()]];
        }

        public override void InitPost()
        {
            wingID = LolHensBase.AddWings(item, texture, getItemType() < 6 ? 200 : 300, 8, true);
        }

        public virtual float getColor(int itemType)
        {
            if (itemType < 6)
            {
                return itemType;
            }
            else
            {
                time = (time + 0.02f) % 5;
                return time;
            }
        }

        public override void Effects(Player player)
        {
            base.Effects(player);
            int itemType = getItemType();
            float color = getColor(itemType);
            float brightness = 0.8f;
            float colorR = lightR.Interpolate(color, itemType == 6 ? lightTrans : null) * brightness;
            float colorG = lightG.Interpolate(color, itemType == 6 ? lightTrans : null) * brightness;
            float colorB = lightB.Interpolate(color, itemType == 6 ? lightTrans : null) * brightness;
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
            return 6;
        }
    }
}