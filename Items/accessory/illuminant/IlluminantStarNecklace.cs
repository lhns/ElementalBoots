using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TAPI;
using Terraria;

namespace LolHens.Items
{
    public class IlluminantStarNecklace : LolHensItem
    {
        public override void Init()
        {
            Events.registry.Register((Events.PlayerRespawn e) =>
            {
                if (!equipped) return;

                e.player.player.statMana = e.player.player.statManaMax;
            }, this);
        }

        public override void Effects(Player player)
        {
            base.Effects(player);

            player.starCloak = System.Math.Max(player.starCloak, 3);
            player.longInvince = true;
        }

        public override void DamagePlayer(NPC npc, Player owner, int hitDir, ref int damage, ref bool crit, ref float critMult)
        {
            owner.AddBuff("LolHens:IlluminantBuff", 900, item, false);
        }
    }
}