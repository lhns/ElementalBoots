using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibEventManagerCSharp;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace ElementalBoots.Items.Accessories.Wings
{
    abstract class Wings: Accessory
    {
        protected int wingTimeMax = 0;
        protected float constantAscend = 0.1f;
        protected float ascentWhenFalling = 0.5f;
        protected float ascentWhenRising = 0.1f;
        protected float minRisingSpeedMultiplier = 0.5f;
        protected float maxAscentSpeedMultiplier = 1.5f;
        protected float maxHorizontalSpeedMultiplier = 1;
        protected float horizontalAccelerationMultiplier = 1;

        protected bool glowing = false;

        private EventListener modifyPlayerDrawDataListener;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            modifyPlayerDrawDataListener = Events.Registry().Register((Events.ModifyPlayerDrawData e) =>
            {
                RenderGlow(e);
            });
        }

        private void RenderGlow(Events.ModifyPlayerDrawData e)
        {
            if (glowing)
            {
                for (int i = 0; i < e.playerDrawData.Count; i++)
                {
                    var drawData = e.playerDrawData[i];

                    if (item.wingSlot >= 0 && item.wingSlot < Main.wingsTexture.Length && drawData.texture == Main.wingsTexture[item.wingSlot])
                    {
                        drawData.color = new Color(250, 250, 250);
                        e.playerDrawData[i] = drawData;
                    }
                }
            }
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = this.ascentWhenFalling;
            ascentWhenRising = this.ascentWhenRising;
            maxCanAscendMultiplier = this.minRisingSpeedMultiplier;
            maxAscentMultiplier = this.maxAscentSpeedMultiplier;
            constantAscend = this.constantAscend;

        }

        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = maxHorizontalSpeedMultiplier * speed;
            acceleration *= horizontalAccelerationMultiplier;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);

            player.wingTimeMax = wingTimeMax;
        }
    }
}
