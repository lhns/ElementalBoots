using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TAPI;
using Terraria;

namespace LolHens.Items
{
    public class BandOfStamina : LolHensItem
    {
        public override void Init()
        {
            base.Init();

            modBase.eventRegistry.Register((LolHensEvent.EntityDamaged e) =>
            {
                time = 0;
            });

            modBase.eventRegistry.Register((LolHensEvent.PlayerRespawn e) =>
            {
                time = 0;
            });
        }

        public override void Effects(Player player)
        {
            base.Effects(player);

            int chance = 0;

            if (seconds() >= 20)
            {
                chance = 15;
            }
            else if (seconds() >= 10)
            {
                chance = 5;
            }
            else if (seconds() >= 3)
            {
                chance = 2;
            }
            else if (seconds() >= 1)
            {
                chance = 1;
            }

            player.meleeCrit += chance;
            player.rangedCrit += chance;
            player.magicCrit += chance;
        }
    }
}