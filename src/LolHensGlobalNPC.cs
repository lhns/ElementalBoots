using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection;
using LitJson;

using TAPI;
using Terraria;

namespace LolHens
{
    [GlobalMod] public class LolHensGlobalNPC : TAPI.ModNPC
    {
        public static class NPC
        {
            public const int MERCHANT = 17;
            public const int DRYAD = 20;
            public const int GOBLIN_TINKERER = 107;
            public const int WIZARD = 108;
            public const int MECHANIC = 124;
            public const int STEAMPUNKER = 178;
            public const int CYBORG = 209;
            public const int WITCH_DOCTOR = 228;
            public const int PIRATE = 229;
        }
        
        public override void PostSetupShop(Chest chest, ref int index) {
            base.PostSetupShop(chest, ref index);
            
            if (npc.type == NPC.PIRATE) chest.item[index++].SetDefaults(ItemDef.byName["LolHens:LilBombard"].type);
        }
    }
}