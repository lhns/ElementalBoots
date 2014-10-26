using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TAPI;
using Terraria;

namespace LolHens.Items
{
    public class TrueAngelWings : TrueDemonWings
    {

        public override void InitTextures()
        {
            texture = modBase.textures["Items/wings/true/TrueAngelWingsPlayer"];
        }
    }
}