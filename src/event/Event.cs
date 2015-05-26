﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;
using TAPI;

namespace LolHens
{
    public class Event
    {
        private Boolean cancelled = false;

        public void Cancel()
        {
            cancelled = true;
        }

        public Boolean Cancelled()
        {
            return cancelled;
        }

        public virtual void OnEventPre() { }

        public virtual void OnEventPost() { }

        public class EntityDamaged : Event
        {
            public readonly MPlayer player;
            public readonly CodableEntity victim;
            public readonly Projectile projectile;
            public readonly int hitDir;
            public int damage;
            public float knockback;
            public bool crit;
            public float critMult;

            public EntityDamaged(MPlayer player, CodableEntity victim, Projectile projectile, int hitDir, int damage, float knockback, bool crit, float critMult)
            {
                this.player = player;
                this.victim = victim;
                this.projectile = projectile;
                this.hitDir = hitDir;
                this.damage = damage;
                this.knockback = knockback;
                this.crit = crit;
                this.critMult = critMult;
            }

            public static void Call(EventRegistry registry, MPlayer player, CodableEntity victim, Projectile projectile, int hitDir, ref int damage, ref float knockback, ref bool crit, ref float critMult)
            {
                EntityDamaged lolHensEvent = new EntityDamaged(player, victim, projectile, hitDir, damage, knockback, crit, critMult);

                registry.Call(lolHensEvent);

                damage = lolHensEvent.damage;
                knockback = lolHensEvent.knockback;
                crit = lolHensEvent.crit;
                critMult = lolHensEvent.critMult;
            }
        }

        public class PlayerDeath : Event
        {
            public readonly MPlayer player;
            public readonly double damage;
            public readonly int hitDir;
            public readonly bool pvp;
            public readonly string deathText;

            public PlayerDeath(MPlayer player, double damage, int hitDir, bool pvp, string deathText)
            {
                this.player = player;
                this.damage = damage;
                this.hitDir = hitDir;
                this.pvp = pvp;
                this.deathText = deathText;
            }

            public static void Call(EventRegistry registry, MPlayer player, double damage, int hitDir, bool pvp, string deathText)
            {
                PlayerDeath lolHensEvent = new PlayerDeath(player, damage, hitDir, pvp, deathText);

                registry.Call(lolHensEvent);
            }
        }

        public class PlayerRespawn : Event
        {
            public readonly MPlayer player;

            public PlayerRespawn(MPlayer player)
            {
                this.player = player;
            }

            public static void Call(EventRegistry registry, MPlayer player)
            {
                PlayerRespawn lolHensEvent = new PlayerRespawn(player);

                registry.Call(lolHensEvent);
            }
        }

        public class ChestGenerated : Event
        {
            public readonly ChestInfo chestInfo;

            public ChestGenerated(ChestInfo chestInfo)
            {
                this.chestInfo = chestInfo;
            }

            public static void Call(EventRegistry registry, ChestInfo chestInfo)
            {
                ChestGenerated lolHensEvent = new ChestGenerated(chestInfo);

                registry.Call(lolHensEvent);
            }
        }
    }
}