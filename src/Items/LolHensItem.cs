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

        public Vector2 useScreenPos = default(Vector2);

        public Boolean glowing = false;
        public Boolean diagonalTexture = false;

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

        public override bool PreShoot(Player player, Vector2 position, Vector2 velocity, int projType, int damage, float knockback)
        {
            useScreenPos = Main.mouseWorld - player.position;
            UseStyle(player);

            return base.PreShoot(player, position, velocity, projType, damage, knockback);
        }

        public Vector2 GetUsePos(Player player)
        {
            return player.position + useScreenPos;
        }

        public override void UseStyle(Player player)
        {
            if (diagonalTexture)
            {
                Vector2 pos = player.position + useScreenPos;
                player.direction = (pos.X < player.Center.X ? -1 : 1);
                float PlayerMouseDistX = pos.X - player.Center.X;
                float PlayerMouseDistY = pos.Y - player.Center.Y;
                player.itemRotation = (float)Math.Atan2(PlayerMouseDistY * player.direction, PlayerMouseDistX * player.direction) + (((float)Math.PI / 4f) * player.direction);
                player.itemLocation = player.Center;
            }
            else
            {
                base.UseStyle(player);
            }
        }

        public override void SetUseFrame(Player player)
        {
            if (diagonalTexture)
            {
                player.itemRotation -= (((float)Math.PI / 4f) * player.direction);
                player.SetFrameGun(item);
                player.itemRotation += (((float)Math.PI / 4f) * player.direction);
            }
            else
            {
                base.SetUseFrame(player);
            }
        }

        public virtual void DrawItemSlotItem(ref SpriteBatch sb, ref ItemSlot slot, ref Item item, ref Texture2D texture, ref Color color, ref float scale)
        {
        }

        public virtual bool DrawItemSlotItemTexture(SpriteBatch sb, ItemSlot slot, Item item, Texture2D texture, Color color, float scale)
        {
            return true;
        }

        public virtual bool DrawItemSlotBgColor(SpriteBatch sb, ItemSlot slot, Item item, Texture2D texture, float scale)
        {
            return true;
        }

        public virtual bool DrawItemSlotItemCount(SpriteBatch sb, ItemSlot slot, Item item)
        {
            return true;
        }

        public virtual bool DrawItemSlotNum(SpriteBatch sb, ItemSlot slot)
        {
            return true;
        }

        public virtual bool DrawItemSlotItemStats(SpriteBatch sb, ItemSlot slot, Item item, float scale)
        {
            return true;
        }
    }
}