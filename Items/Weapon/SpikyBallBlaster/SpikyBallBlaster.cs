using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace ElementalBoots.Items.Weapon.SpikyBallBlaster
{
    class SpikyBallBlaster: Gun
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            var spikyBall = ElementalBootsMod.instance.GetItemType(ItemID.SpikyBall);
            //spikyBall.ammo = spikyBall.type;
            //spikyBall.shoot = ProjectileID.SpikyBall;

            item.useAmmo = ItemID.SpikyBall;
            item.shoot = ProjectileID.SpikyBall;
            item.shootSpeed = 16;
            item.autoReuse = true;

            holdoutOffset = new Vector2(-6, -10);
            shootOffset = new Vector2(50, -10);
            shootOrigin = new Vector2(-2, -5);
            rotationOffset = 0.1f;

            item.ranged = true;
            item.noMelee = true;

            item.useStyle = 5;
            item.useAnimation = 15;
            item.useTime = 15;
        }
    }
}
