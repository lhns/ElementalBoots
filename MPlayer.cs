using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace ElementalBoots
{
    class MPlayer : ModPlayer
    {
        public long Time { get; private set; }

        private ISet<IEquipped> _equippedItems = new HashSet<IEquipped>();
        
        public void UpdateEquipped(IEquipped equipped)
        {
            equipped.SetLastEquippedTime(Time);

            if (!_equippedItems.Contains(equipped))
            {
                _equippedItems.Add(equipped);

                equipped.OnEquip(player);
            }
        }

        private void UpdateUnEquipped()
        {
            var newEquippedItems = new HashSet<IEquipped>();

            foreach (var equipped in _equippedItems)
            {
                if (equipped.GetLastEquippedTime() < Time)
                {
                    equipped.OnUnEquip(player);
                }
                else
                {
                    newEquippedItems.Add(equipped);
                }
            }

            _equippedItems = newEquippedItems;
        }

        private bool postRespawn = false;

        private int defaultItemGrabRange;

        public override void PreUpdate()
        {
            defaultItemGrabRange = Player.defaultItemGrabRange;

            UpdateUnEquipped();

            Time += 1;
        }

        public override void PostUpdate()
        {
            base.PostUpdate();

            Player.defaultItemGrabRange = defaultItemGrabRange;

            if (postRespawn && !player.dead)
            {
                postRespawn = false;

                Events.Registry().Call(new Events.PlayerPostRespawn(this));
            }
        }

        private int id = -1;

        public int GetID()
        {
            if (id > -1) return id;
            for (var i = 0; i < Main.player.Length; i++)
            {
                if (Main.player[i] == player)
                {
                    id = i;
                    return i;
                }
            }
            throw new Exception("Player not found!");
        }

        public void ApplyAccessoryEffects(Item item, bool hideVisual = false)
        {
            player.VanillaUpdateEquip(item);
            bool flag1 = false, flag2 = false, flag3 = false;
            player.VanillaUpdateAccessory(GetID(), item, hideVisual, ref flag1, ref flag2, ref flag3);
        }

        public override void OnRespawn(Player player)
        {
            base.OnRespawn(player);

            postRespawn = true;
        }

        public override void PostHurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            base.PostHurt(pvp, quiet, damage, hitDirection, crit);

            Events.Registry().Call(new Events.PlayerPostHurt(this, pvp, quiet, damage, hitDirection, crit));
        }

        public override void ModifyDrawLayers(List<PlayerLayer> layers)
        {
            base.ModifyDrawLayers(layers);

            layers.Add(new PlayerLayer(mod.Name, "ModyfyPlayerDrawData", (PlayerDrawInfo info) =>
            {
                Main.playerDrawData = ModifyPlayerDrawData(Main.playerDrawData);
            }));
        }

        public List<DrawData> ModifyPlayerDrawData(List<DrawData> playerDrawData)
        {
            Events.Registry().Call(new Events.ModifyPlayerDrawData(this, playerDrawData));

            return playerDrawData;
        }

        public bool consumeAmmo = true;

        public override bool ConsumeAmmo(Item weapon, Item ammo)
        {
            if (!consumeAmmo)
            {
                consumeAmmo = true;
                return false;
            }

            return base.ConsumeAmmo(weapon, ammo);
        }
    }
}
