using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementalBoots.Items.Accessories.ElementalBoots
{
    [AutoloadEquip(EquipType.Shoes)]
    public class ElementalBoots : MItem
    {
        public override void SetDefaults()
        {
            item.maxStack = 1;
            item.value = 30*Value.GOLD;
            item.rare = 4;
            item.accessory = true;
        }

        public override void UpdateAccessory2(Player player, bool hideVisual)
        {
            base.UpdateAccessory2(player, hideVisual);

            player.accRunSpeed = Math.Max(player.accRunSpeed, 6.8f);
            player.rocketBoots = Math.Max(player.rocketBoots, 3);
            player.rocketTimeMax = Math.Max(player.rocketTimeMax, 21);
            player.moveSpeed += 0.08f;
            player.iceSkate = true;
            player.waterWalk = true;
            player.fireWalk = true;
            player.lavaMax += 420;

            if (!hideVisual) FlameEffect(player);
        }

        public int dustDelay = 0;
        public float brightness = 0.8f;

        private void FlameEffect(Player player)
        {
            dustDelay = Math.Max(0, dustDelay - 1);
            if (dustDelay <= 0)
            {
                dustDelay = 3;
                if (player.velocity.Y < 0 && player.rocketTime < player.rocketTimeMax)
                {
                    for (int i = 0; i < (int) (Math.Abs(player.velocity.Y)*1.8f); i++)
                    {
                        spawnFire(player, player.position.X - 5f, player.position.Y + player.height/1.2f, 1, 1);
                        spawnFire(player, player.position.X + player.width/3*2 + 5f,
                            player.position.Y + player.height/1.2f, 1, 1);
                    }
                }
                else if (Math.Abs(player.velocity.X) >= (player.accRunSpeed + 3)/2 && player.velocity.Y == 0f &&
                         player.mount == null)
                {
                    for (int i = 0; i < (int) (Math.Abs(player.velocity.X)*1.8f); i++)
                    {
                        spawnFire(player, player.position.X, player.position.Y + player.height/1f, player.width, 1);
                    }
                }
                else
                {
                    spawnFire(player, player.position.X, player.position.Y + player.height/1.5f, player.width,
                        player.height/3);
                }
            }
            // Lighting.AddLight((int)((player.position.X + player.width / 2) / 16f), (int)((player.position.Y + player.height / 1.5f) / 16f), brightness * 1f, brightness * 0.7f, brightness * 0.6f);
        }

        private void spawnFire(Player player, float x, float y, int w, int h)
        {
            int dust = Dust.NewDust(new Vector2(x, y), w, h, 6, 0f, 0f, 0, default(Color), 1.6f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].velocity -= player.velocity*0.5f;
        }
    }
}