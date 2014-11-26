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
        public override void EffectsPre(Player player)
        {
            //player.gravDir *= 2f;
            //player.runAcceleration *= 10f;
        }
    }
}