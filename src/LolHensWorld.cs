using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LitJson;

using TAPI;
using Terraria;
using LolHens.Items;

namespace LolHens
{
    public class LolHensWorld : TAPI.ModWorld
    {
        public override void WorldGenPostGen()
        {
            ChestInfo.OnChestsGenerate();
        }
    }
}