using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;
using TAPI;

namespace LolHens
{
    public abstract class LolHensEvent
    {
        private Boolean cancelled = false;

        public void Cancel()
        {
            cancelled = true;
        }

        public Boolean IsCancelled()
        {
            return cancelled;
        }

        protected virtual void OnEventPre() {}

        protected virtual void OnEventPost() {}

        public class Registry
        {
            private List<EventListener> eventListeners = new List<EventListener>();

            public void Register<E>(Action<E> listener) where E : LolHensEvent
            {
                eventListeners.Add(new EventListener(e => listener((E)e), typeof(E)));
            }

            public Boolean Call<E>(E lolHensEvent) where E : LolHensEvent
            {
                List<EventListener> eventListeners = new List<EventListener>();
                eventListeners.AddRange(this.eventListeners);

                lolHensEvent.OnEventPre();

                foreach (EventListener listener in eventListeners)
                {
                    if (listener.type == typeof(E))
                    {
                        listener.action(lolHensEvent);
                        if (lolHensEvent.IsCancelled()) break;
                    }
                }

                lolHensEvent.OnEventPost();

                return !lolHensEvent.IsCancelled();
            }

            private class EventListener
            {
                public Action<LolHensEvent> action;
                public Type type;

                public EventListener(Action<LolHensEvent> action, Type type)
                {
                    this.action = action;
                    this.type = type;
                }
            }
        }

        public class EntityDamaged : LolHensEvent
        {
            public readonly LolHensPlayer player;
            public readonly CodableEntity victim;
            public readonly Projectile projectile;
            public readonly int hitDir;
            public int damage;
            public float knockback;
            public bool crit;
            public float critMult;

            public EntityDamaged(LolHensPlayer player, CodableEntity victim, Projectile projectile, int hitDir, int damage, float knockback, bool crit, float critMult)
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

            public static void Call(Registry registry, LolHensPlayer player, CodableEntity victim, Projectile projectile, int hitDir, ref int damage, ref float knockback, ref bool crit, ref float critMult)
            {
                EntityDamaged lolHensEvent = new EntityDamaged(player, victim, projectile, hitDir, damage, knockback, crit, critMult);

                registry.Call(lolHensEvent);

                damage = lolHensEvent.damage;
                knockback = lolHensEvent.knockback;
                crit = lolHensEvent.crit;
                critMult = lolHensEvent.critMult;
            }
        }

        public class PlayerDeath : LolHensEvent
        {
            public readonly LolHensPlayer player;
            public readonly double damage;
            public readonly int hitDir;
            public readonly bool pvp;
            public readonly string deathText;

            public PlayerDeath(LolHensPlayer player, double damage, int hitDir, bool pvp, string deathText)
            {
                this.player = player;
                this.damage = damage;
                this.hitDir = hitDir;
                this.pvp = pvp;
                this.deathText = deathText;
            }

            public static void Call(Registry registry, LolHensPlayer player, double damage, int hitDir, bool pvp, string deathText)
            {
                PlayerDeath lolHensEvent = new PlayerDeath(player, damage, hitDir, pvp, deathText);

                registry.Call(lolHensEvent);
            }
        }

        public class PlayerRespawn : LolHensEvent
        {
            public readonly LolHensPlayer player;

            public PlayerRespawn(LolHensPlayer player)
            {
                this.player = player;
            }

            public static void Call(Registry registry, LolHensPlayer player)
            {
                PlayerRespawn lolHensEvent = new PlayerRespawn(player);

                registry.Call(lolHensEvent);
            }
        }

        public class ChestGenerated: LolHensEvent
        {
            public readonly ChestInfo chestInfo;

            public ChestGenerated(ChestInfo chestInfo)
            {
                this.chestInfo = chestInfo;
            }

            public static void Call(Registry registry, ChestInfo chestInfo)
            {
                ChestGenerated lolHensEvent = new ChestGenerated(chestInfo);

                registry.Call(lolHensEvent);
            }
        }
    }
}
