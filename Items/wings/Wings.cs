using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TAPI;
using Terraria;

namespace LolHens.Items
{
    public class Wings
    {
        public int time;
        public float speed;
        public bool glow;

        public Wings(int time, float speed, bool glow)
        {
            this.time = time;
            this.speed = speed;
            this.glow = glow;
        }
    }
}