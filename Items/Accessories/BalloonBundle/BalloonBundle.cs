using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ElementalBoots.Items.Accessories.BalloonBundle
{
    abstract class BalloonBundle : MItem
    {
        public bool Cloud, Blizzard, Sandstorm, Fart, Sail, Tornado = false;
        public bool Horseshoe, Obsidian, Honey = false;

        public override void SetDefaults()
        {
            item.maxStack = 1;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);

            player.jumpBoost = true;

            player.doubleJumpCloud = Cloud;
            player.doubleJumpBlizzard = Blizzard;
            player.doubleJumpSandstorm = Sandstorm;
            player.doubleJumpFart = Fart;
            player.doubleJumpSail = Sail;
            if (Tornado) player.GetModPlayer<MPlayer>(mod).ApplyAccessoryEffects(mod.GetItem("TornadoInABottle").item);
            if (Horseshoe) player.noFallDmg = true;
            if (Obsidian) player.fireWalk = true;
            if (Honey) player.bee = true;
        }
    }
}