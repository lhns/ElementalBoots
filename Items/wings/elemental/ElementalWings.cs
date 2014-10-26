using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TAPI;
using Terraria;

namespace LolHens.Items
{
    public class ElementalWings : LolHensItem
    {
        public Texture2D texture = null;
        public int wingID;

        public override void InitTextures()
        {
            texture = modBase.textures["Items/wings/elemental/ElementalWingsPlayer"];
        }

        public override void InitPost()
        {
            wingID = LolHensBase.AddWings(item, texture, 600, 8, true);
        }

        public override void Effects(Player player)
        {
            player.wings = wingID;
        }
    }
}