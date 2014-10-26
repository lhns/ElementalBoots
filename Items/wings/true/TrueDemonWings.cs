using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TAPI;
using Terraria;

namespace LolHens.Items
{
    public class TrueDemonWings : ElementalWings
    {

        public override void InitTextures()
        {
            texture = modBase.textures["Items/wings/true/TrueDemonWingsPlayer"];
        }

        public override void InitPost()
        {
            wingID = LolHensBase.AddWings(item, texture, 140, 8, true);
        }
    }
}