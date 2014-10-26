using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TAPI;
using Terraria;

namespace LolHens.Items
{
    public class ElementalWings : LolHensItem
    {
        public Texture2D texture = null;
        public int wingID;

        public override void Init()
        {
            glowing = true;
        }
    }
}