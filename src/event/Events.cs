using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LibEventManagerCSharp;
using Terraria;
using TAPI;

namespace LolHens
{
    public class Events
    {
        public static EventRegistry registry;

        internal static void Init()
        {
            registry = new EventRegistry();
        }

        internal static void CallModLoaded(MBase modBase)
        {
            registry.Call(new Events.ModLoaded.Factory())(modBase);
        }

        internal static void CallAllModsLoaded(MBase modBase)
        {
            registry.Call(new Events.AllModsLoaded.Factory())(modBase);
        }

        internal static void CallOptionChanged(MBase modBase, Option option)
        {
            registry.Call(new Events.OptionChanged.Factory())(modBase, option);
        }

        public class EntityDamaged : Event
        {
            public readonly MPlayer player;
            public readonly CodableEntity victim;
            public readonly CodableEntity attacker;
            public readonly Projectile projectile;
            public readonly int hitDir;
            public int damage;
            public float knockback;
            public bool crit;
            public float critMult;

            public EntityDamaged(MPlayer player, CodableEntity victim, CodableEntity attacker, Projectile projectile, int hitDir, int damage, float knockback, bool crit, float critMult)
            {
                this.player = player;
                this.victim = victim;
                this.attacker = attacker;
                this.projectile = projectile;
                this.hitDir = hitDir;
                this.damage = damage;
                this.knockback = knockback;
                this.crit = crit;
                this.critMult = critMult;
            }

            public class Factory : EventFactory<EntityDamaged, Func<MPlayer, CodableEntity, CodableEntity, Projectile, int, int, float, bool, float, EntityDamaged>>
            {
                protected override Func<MPlayer, CodableEntity, CodableEntity, Projectile, int, int, float, bool, float, EntityDamaged> NewEventFactory(EventRegistry registry)
                {
                    return (MPlayer player, CodableEntity victim, CodableEntity attacker, Projectile projectile, int hitDir, int damage, float knockback, bool crit, float critMult) =>
                    {
                        return Call(registry, new EntityDamaged(player, victim, attacker, projectile, hitDir, damage, knockback, crit, critMult));
                    };
                }
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

            public class Factory : EventFactory<PlayerDeath, Func<MPlayer, double, int, bool, string, PlayerDeath>>
            {
                protected override Func<MPlayer, double, int, bool, string, PlayerDeath> NewEventFactory(EventRegistry registry)
                {
                    return (MPlayer player, double damage, int hitDir, bool pvp, string deathText) =>
                    {
                        return Call(registry, new PlayerDeath(player, damage, hitDir, pvp, deathText));
                    };
                }
            }
        }

        public class PlayerRespawn : Event
        {
            public readonly MPlayer player;

            public PlayerRespawn(MPlayer player)
            {
                this.player = player;
            }

            public class Factory : EventFactory<PlayerRespawn, Func<MPlayer, PlayerRespawn>>
            {
                protected override Func<MPlayer, PlayerRespawn> NewEventFactory(EventRegistry registry)
                {
                    return (MPlayer player) =>
                    {
                        return Call(registry, new PlayerRespawn(player));
                    };
                }
            }
        }

        public class ChestGenerated : Event
        {
            public readonly ChestInfo chestInfo;

            public ChestGenerated(ChestInfo chestInfo)
            {
                this.chestInfo = chestInfo;
            }

            public class Factory : EventFactory<ChestGenerated, Func<ChestInfo, ChestGenerated>>
            {
                protected override Func<ChestInfo, ChestGenerated> NewEventFactory(EventRegistry registry)
                {
                    return (ChestInfo chestInfo) =>
                    {
                        return Call(registry, new ChestGenerated(chestInfo));
                    };
                }
            }
        }

        public class ModLoaded : Event
        {
            public readonly MBase modBase;

            public ModLoaded(MBase modBase)
            {
                this.modBase = modBase;
            }

            protected override void OnEventPost(ref bool remove)
            {
                remove = true;
            }

            public class Factory : EventFactory<ModLoaded, Func<MBase, ModLoaded>>
            {
                protected override Func<MBase, ModLoaded> NewEventFactory(EventRegistry registry)
                {
                    return (MBase modBase) =>
                    {
                        return Call(registry, new ModLoaded(modBase));
                    };
                }
            }
        }

        public class AllModsLoaded : Event
        {
            public readonly MBase modBase;

            public AllModsLoaded(MBase modBase)
            {
                this.modBase = modBase;
            }

            protected override void OnEventPost(ref bool remove)
            {
                remove = true;
            }

            public class Factory : EventFactory<AllModsLoaded, Func<MBase, AllModsLoaded>>
            {
                protected override Func<MBase, AllModsLoaded> NewEventFactory(EventRegistry registry)
                {
                    return (MBase modBase) =>
                    {
                        return Call(registry, new AllModsLoaded(modBase));
                    };
                }
            }
        }

        public class OptionChanged : Event
        {
            public readonly MBase modBase;
            public readonly Option option;

            public OptionChanged(MBase modBase, Option option)
            {
                this.modBase = modBase;
                this.option = option;
            }

            public class Factory : EventFactory<OptionChanged, Func<MBase, Option, OptionChanged>>
            {
                protected override Func<MBase, Option, OptionChanged> NewEventFactory(EventRegistry registry)
                {
                    return (MBase modBase, Option option) =>
                    {
                        return Call(registry, new OptionChanged(modBase, option));
                    };
                }
            }
        }
    }
}
