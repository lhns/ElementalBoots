using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection;
using LitJson;

using TAPI;
using Terraria;
using LolHens.Items;

namespace LolHens
{
    public class LolHensPlayer : TAPI.ModPlayer
    {
        new public LolHensBase modBase;

        //public static bool dead = false;
        public static bool flyingVertically = false;
        public static bool resetFlightTimer = false;

        private bool dead = false;

        public override void Initialize()
        {
            modBase = base.modBase as LolHensBase;
        }

        public override void PostUpdate()
        {
            if (dead && player.respawnTimer == 0)
            {
                dead = false;
                LolHensEvent.PlayerRespawn.Call(this);
            }
            /*dead = false;
            if (player.dead) died = true;
            if (player.respawnTimer == 0 && died)
            {
                LolHensBase.instance.onPlayerRespawn.ForEach(action => action(this));
                died = false;
                dead = true;
            }*/

            resetFlightTimer = (player.grappling[0] >= 0
                || (player.grappling[0] == -1 && !player.tongued && (player.velocity.Y == 0f || player.sliding))
                || (player.pulley));

            flyingVertically = player.grappling[0] == -1 && !player.tongued && player.velocity.Y != 0f;
        }

        public override void ModifyDrawLayerList(System.Collections.Generic.List<PlayerLayer> list)
        {
            LolHensItem wings = Main.localPlayer.GetWingsItem();
            if (wings != null && wings.glowing) PlayerLayer.extraDrawInfo.colorArmorBody = new Color(255, 255, 255);
        }

        public override void PostKill(double damage, int hitDirection, bool pvp, string deathText)
        {
            dead = true;
            LolHensEvent.PlayerDeath.Call(this, damage, hitDirection, pvp, deathText);
        }

        public override void DamageNPC(NPC npc, int hitDir, ref int damage, ref float knockback, ref bool crit, ref float critMult)
        {
            Damage(npc, null, hitDir, ref damage, ref knockback, ref crit, ref critMult);
        }

        public override void DamageNPC(Projectile projectile, NPC npc, int hitDir, ref int damage, ref float knockback, ref bool crit, ref float critMult)
        {
            Damage(npc, projectile, hitDir, ref damage, ref knockback, ref crit, ref critMult);
        }

        public override void DamagePVP(Player victim, int hitDir, ref int damage, ref bool crit, ref float critMult)
        {
            float knockback = 0;
            Damage(victim, null, hitDir, ref damage, ref knockback, ref crit, ref critMult);
        }

        public override void DamagePVP(Projectile projectile, Player victim, int hitDir, ref int damage, ref bool crit, ref float critMult)
        {
            float knockback = 0;
            Damage(victim, projectile, hitDir, ref damage, ref knockback, ref crit, ref critMult);
        }

        public override void DamagePlayer(NPC npc, int hitDir, ref int damage, ref bool crit, ref float critMult)
        {
            float knockback = 0;
            Damage(player, null, hitDir, ref damage, ref knockback, ref crit, ref critMult);
        }

        public override void DamagePlayer(Projectile projectile, int hitDir, ref int damage, ref bool crit, ref float critMult)
        {
            float knockback = 0;
            Damage(player, projectile, hitDir, ref damage, ref knockback, ref crit, ref critMult);
        }

        private void Damage(CodableEntity victim, Projectile projectile, int hitDir, ref int damage, ref float knockback, ref bool crit, ref float critMult)
        {
            LolHensEvent.EntityDamaged.Call(this, victim, projectile, hitDir, ref damage, ref knockback, ref crit, ref critMult);
        }
    }
}