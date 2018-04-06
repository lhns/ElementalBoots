using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ElementalBoots.Items.Accessories.BalloonBundle
{
    public abstract class BalloonBundle : MItem
    {
        public bool Cloud, Blizzard, Sandstorm, Fart, Sail, Tornado = false;
        public bool Horseshoe, Honey = false;

        public override void SetDefaults()
        {
            item.maxStack = 1;
            item.accessory = true;
        }

        public override void UpdateAccessory2(Player player, bool hideVisual)
        {
            base.UpdateAccessory2(player, hideVisual);

            player.jumpBoost = true;

            player.dJumpEffectCloud = Cloud;
            player.dJumpEffectBlizzard = Blizzard;
            player.dJumpEffectSandstorm = Sandstorm;
            player.dJumpEffectFart = Fart;
            player.dJumpEffectSail = Sail;
            
            if (Tornado) player.GetModPlayer<MPlayer>(mod).ApplyAccessoryEffects(mod.GetItem("TornadoInABottle").item);

            if (Horseshoe) player.noFallDmg = true;

            if (Honey) player.bee = true;
        }
    }
}