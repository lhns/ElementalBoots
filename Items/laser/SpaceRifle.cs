using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TAPI;
using TAPI.UIKit;
using Terraria;

namespace LolHens.Items
{
    public class SpaceRifle : LolHensGun
    {
        public override void Init()
        {
            base.Init();
            bulletOffset = 50;
            //bulletOrigin.Y += 2;
        }

        public override void DrawItemSlotItem(ref SpriteBatch sb, ref ItemSlot slot, ref Item item, ref Texture2D texture, ref Color color, ref float scale)
        {
            scale *= 1.4f;
        }
    }
}