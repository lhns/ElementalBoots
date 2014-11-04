using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TAPI;
using Terraria;
using TAPI.UIKit;

namespace LolHens.Items
{
    public abstract class LolHensItem : ModItem
    {
        new public LolHensBase modBase;

        private float usedTime = 0;
        public float usedPercent = 0;
        public int time = 0;

        public Boolean glowing = false;

        public sealed override void Initialize()
        {
            modBase = base.modBase as LolHensBase;
            modBase.items.Add(this);
            Init();
            if (!Main.dedServ) InitTextures();
            InitPost();
        }

        public virtual void Init() { }

        public virtual void InitTextures() { }

        public virtual void InitPost() { }

        public virtual void PlayerDied(Player player) { }

        public virtual void UseItemPost(Player player) { }

        public virtual int PreDrawItemSlotItem(Item item, Color color, SpriteBatch sb, ItemSlot slot) { return 0; }

        public override void Effects(Player player)
        {
            time++;
            EffectsPre(player);
            if (LolHensPlayer.dead) PlayerDied(player);
        }

        public virtual void EffectsPre(Player player) { }

        public override bool? UseItem(Player player)
        {
            usedPercent = ++usedTime / (float)(item.useAnimation - 1);
            if (usedPercent >= 1)
            {
                UseItemPost(player);
                usedTime = 0;
                usedPercent = 0;
                return false;
            }
            return null;
        }
    }
}