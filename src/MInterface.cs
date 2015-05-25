using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection;
using LitJson;

using TAPI;
using TAPI.UIKit;
using Terraria;
using LolHens.Items;

namespace LolHens
{
    public class MInterface : TAPI.ModInterface
    {
        public override bool PreDrawItemSlotItem(SpriteBatch sb, ItemSlot slot)
        {
            Item item = slot.MyItem;

            if (item.IsBlank()) return false;

            LolHensItem lhItem = item.AsLolHensItem();

            if (lhItem == null) return true;


            Texture2D texture = item.GetTexture();

            float scale = slot.scale;

            if (texture.Width > 32 || texture.Height > 32)
            {
                if (texture.Width > texture.Height)
                {
                    scale *= 32f / (float)texture.Width;
                }
                else
                {
                    scale *= 32f / (float)texture.Height;
                }
            }

            Color color = item.GetTextureColor();


            lhItem.DrawItemSlotItem(ref sb, ref slot, ref item, ref texture, ref color, ref scale);

            if (lhItem.DrawItemSlotItemTexture(sb, slot, item, texture, color, scale))
            {
                DrawItemSlotItemTexture(sb, slot, item, texture, color, scale);
            }

            if (lhItem.DrawItemSlotBgColor(sb, slot, item, texture, scale))
            {
                DrawItemSlotBgColor(sb, slot, item, texture, scale);
            }

            if (lhItem.DrawItemSlotItemCount(sb, slot, item))
            {
                DrawItemSlotItemCount(sb, slot, item);
            }

            //if (slot.modBase == null) return false;

            if (lhItem.DrawItemSlotNum(sb, slot))
            {
                DrawItemSlotNum(sb, slot);
            }

            if (lhItem.DrawItemSlotItemStats(sb, slot, item, scale))
            {
                DrawItemSlotItemStats(sb, slot, item, scale);
            }

            return false;
        }

        private static void DrawItemSlotItemTexture(SpriteBatch sb, ItemSlot slot, Item item, Texture2D texture, Color color, float scale)
        {
            sb.Draw(texture, new Vector2(slot.pos.X + 26f * slot.scale - (float)texture.Width * 0.5f * scale, slot.pos.Y + 26f * slot.scale - (float)texture.Height * 0.5f * scale), null, item.GetAlpha(color) * slot.alpha, 0f, default(Vector2), scale, SpriteEffects.None, 0f);
        }

        private static void DrawItemSlotBgColor(SpriteBatch sb, ItemSlot slot, Item item, Texture2D texture, float scale)
        {
            if (item.color != default(Color))
            {
                sb.Draw(texture, new Vector2(slot.pos.X + 26f * slot.scale - (float)texture.Width * 0.5f * scale, slot.pos.Y + 26f * slot.scale - (float)texture.Height * 0.5f * scale), null, item.GetColor(Color.White) * slot.alpha, 0f, default(Vector2), scale, SpriteEffects.None, 0f);
            }
        }

        private static void DrawItemSlotItemCount(SpriteBatch sb, ItemSlot slot, Item item)
        {
            if (item.stack > 1)
            {
                sb.DrawString(Main.fontItemStack, string.Concat(item.stack), new Vector2(slot.pos.X + 10f * slot.scale, slot.pos.Y + 26f * slot.scale), Color.White * slot.alpha, 0f, default(Vector2), slot.scale, SpriteEffects.None, 0f);
            }
        }

        private static void DrawItemSlotNum(SpriteBatch sb, ItemSlot slot)
        {
            if (slot.type == "Hotbar" || (slot.type == "Inventory" && slot.index < 10))
            {
                string text = (slot.index == 9) ? "0" : string.Concat(slot.index + 1);
                Color value = (Main.localPlayer.selectedItem == slot.index) ? new Color(0, 255, 0, 50) : Main.inventoryBack;
                sb.DrawString(Main.fontItemStack, text, new Vector2(slot.pos.X + 6f, slot.pos.Y + 4f), value * slot.alpha, 0f, default(Vector2), slot.scale * 0.8f, SpriteEffects.None, 0f);
            }
        }

        private static void DrawItemSlotItemStats(SpriteBatch sb, ItemSlot slot, Item item, float scale)
        {
            if (slot.type == "Hotbar")
            {
                if (item.useAmmo > 0)
                {
                    int useAmmo = item.useAmmo;
                    int num2 = 0;
                    for (int i = 0; i < 58; i++)
                    {
                        if (Main.localPlayer.inventory[i].ammo == useAmmo)
                        {
                            num2 += Main.localPlayer.inventory[i].stack;
                        }
                    }
                    sb.DrawString(Main.fontItemStack, string.Concat(num2), slot.pos + new Vector2(8f, 30f) * slot.scale, Color.White * slot.alpha, 0f, default(Vector2), slot.scale * 0.8f, SpriteEffects.None, 0f);
                }
                else if (item.fishingPole > 0)
                {
                    int num3 = 0;
                    for (int j = 0; j < 58; j++)
                    {
                        if (Main.localPlayer.inventory[j].bait > 0)
                        {
                            num3 += Main.localPlayer.inventory[j].stack;
                        }
                    }
                    sb.DrawString(Main.fontItemStack, string.Concat(num3), slot.pos + new Vector2(8f, 30f) * slot.scale, Color.White * slot.alpha, 0f, default(Vector2), slot.scale * 0.8f, SpriteEffects.None, 0f);
                }
                else if (item.tileWand > 0)
                {
                    int tileWand = item.tileWand;
                    int num4 = 0;
                    for (int k = 0; k < 58; k++)
                    {
                        if (Main.localPlayer.inventory[k].type == tileWand)
                        {
                            num4 += Main.localPlayer.inventory[k].stack;
                        }
                    }
                    sb.DrawString(Main.fontItemStack, string.Concat(num4), slot.pos + new Vector2(8f, 30f) * slot.scale, Color.White * slot.alpha, 0f, default(Vector2), slot.scale * 0.8f, SpriteEffects.None, 0f);
                }
                if (item.potion)
                {
                    sb.Draw(Main.cdTexture, slot.pos + Main.inventoryBackTexture.Size() / 2f * slot.scale, null, Color.White * slot.alpha * (1f * (float)Main.localPlayer.potionDelay / (float)Main.localPlayer.potionDelayTime), 0f, Main.cdTexture.Size() / 2f, scale, SpriteEffects.None, 0f);
                }
            }
        }
    }
}