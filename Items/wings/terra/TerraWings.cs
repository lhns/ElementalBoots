using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TAPI;
using Terraria;

namespace LolHens.Items
{
    public class TerraWings : ElementalWings
    {

        public override void InitTextures()
        {
            texture = modBase.textures["Items/wings/terra/TerraWingsPlayer"];
        }

        public override void InitPost()
        {
            wingID = LolHensBase.AddWings(item, texture, 300, 8, true);
        }
    }
}