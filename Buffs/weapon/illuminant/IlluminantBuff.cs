using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

using TAPI;
using Terraria;
using LolHens.Items;

namespace LolHens.Buffs
{
    public class IlluminantBuff : LolHensBuff
    {
        public override void Effects(CodableEntity entity, int index)
        {
            base.Effects(entity, index);
            Lighting.AddLight((int)(entity.Center.X / 16f), (int)(entity.Center.Y / 16f), 0.9f, 0.5f, 0.95f);
        }
    }
}