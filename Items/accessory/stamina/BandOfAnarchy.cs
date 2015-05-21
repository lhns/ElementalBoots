using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TAPI;
using Terraria;

namespace LolHens.Items
{
    public class BandOfAnarchy : BandOfStamina
    {
        public override void Effects(Player player)
        {
            base.Effects(player);

            player.lifeRegen += 1;
            player.manaRegenDelayBonus++;
            player.manaRegenBonus += 25;
        }
    }
}