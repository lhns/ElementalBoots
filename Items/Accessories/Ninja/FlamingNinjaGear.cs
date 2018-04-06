using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace ElementalBoots.Items.Accessories.Ninja
{
    class FlamingNinjaGear: CompoundAccessory
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            
            item.value = 20 * Value.GOLD;
            item.rare = 8;
        }

        public override IList<Item> GetCompoundAccessories()
        {
            return new List<Item>
            {
                ElementalBootsMod.instance.GetItemType(ItemID.FireGauntlet),
                ElementalBootsMod.instance.GetItemType(ItemID.MasterNinjaGear)
            };
        }

        public int dustDelay = 0;

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);

            if (dustDelay++ > 3)
            {
                dustDelay = 0;

                int dust = Dust.NewDust(new Vector2(player.position.X, player.position.Y + player.height / 2.4f), player.width, player.height / 3, 6, 0f, 0f, 0, default(Color), 1.6f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity -= player.velocity * 0.5f;
                if (player.velocity.X != 0)
                {
                    for (int i = 0; i < (int)(Math.Abs(player.velocity.X) * 1.4f); i++)
                    {
                        dust = Dust.NewDust(new Vector2(player.position.X, player.position.Y + player.height / 2.4f), player.width, player.height / 3, 6, 0f, 0f, 0, default(Color), 1.6f);
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].velocity -= player.velocity * 0.5f;
                    }
                }
            }
        }
    }
}
