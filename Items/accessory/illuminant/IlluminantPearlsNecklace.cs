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
        public override void Init()
        {
            modBase.eventRegistry.Register((Event.PlayerRespawn e) =>
            {
                if (!equipped) return;

                e.player.player.statLife = e.player.player.statLifeMax;
                e.player.player.statMana = e.player.player.statManaMax;
            }, this);
        }

        public override void Effects(Player player)
        {
            base.Effects(player);

            player.panic = true;
            player.starCloak = System.Math.Max(player.starCloak, 3);
            player.longInvince = true;
        }

        public override void DamagePlayer(NPC npc, Player owner, int hitDir, ref int damage, ref bool crit, ref float critMult)
        {
            Buffs.LolHensBuff.lastTrigger = item;
            owner.AddBuff("LolHens:IlluminantBuff", 1200, item, false);
            owner.AddBuff("LolHens:IlluminantCrystal", 250, item, false, false);
            owner.AddBuff("Vanilla:Panic!", 600, item, false);
        }
    }
}