using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TAPI;
using Terraria;

namespace LolHens.Items
{
    public class MagmaClimbingGear : LolHensItem
    {
        public int dustDelay = 0;
        public float brightness = 0.8f;


        public override void Effects(Player player)
        {
            player.knockbackMeleeModVanilla *= 2f;
            player.meleeSpeed += 0.09f;
            player.meleeDamage += 0.09f;
            player.magmaStone = true;

            player.spikedBoots += 2;
        }
    }
}