using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ElementalBoots.Items.Accessories.BalloonBundle
{
    public abstract class BalloonBundle: MItem
    {
        public bool Cloud = false, Blizzard = false, Sandstorm = false, Fart = false, Sail = false, Tornado = false;

        public override bool Autoload(ref string name, ref string texture, IList<EquipType> equips)
        {
            equips.Add(EquipType.Balloon);

            return true;
        }

        public override void SetDefaults()
        {
            item.name = "Balloon Bundle";
            item.maxStack = 1;
            item.value = 1 * Value.GOLD;
            item.rare = 4;
            item.accessory = true;
            item.toolTip = "Allows the holder to double jump";
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
        }
    }
}