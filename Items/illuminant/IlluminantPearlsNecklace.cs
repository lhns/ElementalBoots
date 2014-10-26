using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TAPI;
using Terraria;

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
            owner.AddBuff(BuffDef.byName["LolHens:IlluminantBuff"], 1200, false);
            owner.AddBuff(BuffDef.byName["Vanilla:Panic!"], 600, false);

            Projectile.NewProjectile(owner.Center.X, owner.Center.Y, 0, 0, ProjDef.byName["LolHens:IlluminantCrystal"].type, item.damage, item.knockBack, owner.whoAmI);
        }
    }
}