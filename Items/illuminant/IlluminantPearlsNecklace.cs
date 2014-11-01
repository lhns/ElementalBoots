using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TAPI;
using Terraria;

using LolHens;

namespace LolHens.Items
{
    public class IlluminantPearlsNecklace : LolHensItem
    {

        public override void Effects(Player player)
        {
            base.Effects(player);

            player.panic = true;
            player.starCloak = System.Math.Max(player.starCloak, 3);
            player.longInvince = true;
        }

        public override void PlayerDied(Player player)
        {
            player.statLife = player.statLifeMax;
            player.statMana = player.statManaMax;
        }

        public override void DamagePlayer(NPC npc, Player owner, int hitDir, ref int damage, ref bool crit, ref float critMult)
        {
            Buffs.LolHensBuff.lastTrigger = item;
            owner.AddBuff("LolHens:IlluminantBuff", 1200, item, false);
            owner.AddBuff("LolHens:IlluminantCrystal", 300, item, false);
            owner.AddBuff("Vanilla:Panic!", 600, item, false);
        }
    }
}