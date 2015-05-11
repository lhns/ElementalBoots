using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;

namespace LolHens.Items
{
    class SpectrumWings : LaserWings
    {
        private static int[] lightTrans = new int[] { 0, 5, 4, 2, 3 };
        private float lastVerticalPlayerSpeed = 0;

        public override void EffectsPre(Player player)
        {
            //player.gravDir *= 10;
            float time = (this.time * 0.02f) % 5;
            float brightness = 0.8f;
            float colorR = lightR.Interpolate(time, lightTrans) * brightness;
            float colorG = lightG.Interpolate(time, lightTrans) * brightness;
            float colorB = lightB.Interpolate(time, lightTrans) * brightness;
            Lighting.AddLight((int)(player.Center.X / 16f), (int)(player.Center.Y / 16f), colorR, colorG, colorB);
        }
    }
}
