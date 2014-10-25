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

namespace LolHens {
    public class LolHensInterface : TAPI.ModInterface {
		public override bool PreDrawItemSlotItem(SpriteBatch sb, ItemSlot slot) {
			int renderMode = 0;
			// 0 render item
			// 1 render slotNum and ammoCount and handle color
			// 2 only render slotNum and ammoCount
			// >2 render nothing
			Item slotItem = slot.MyItem;
			if (slotItem != null) {
				ModItem modItem = slotItem.GetSubClass<ModItem>();
				if (modItem != null && modItem is ICustomRenderedItem) {
					renderMode = ((ICustomRenderedItem)modItem).PreDrawItemSlotItem(slotItem, slotItem.GetAlpha(slotItem.GetTextureColor()) * slot.alpha, sb, slot);
					if (renderMode == 1 && slot.MyItem.color != default(Color))
						((ICustomRenderedItem)modItem).PreDrawItemSlotItem(slotItem, slotItem.GetColor(Color.White) * slot.alpha, sb, slot);
				}
			}
			if (renderMode > 0) {
				if (renderMode <= 2) {
					float num = 1f;
					Texture2D texture = slot.MyItem.GetTexture();
					if (texture.Width > 32 || texture.Height > 32)
					{
						num = ((texture.Width > texture.Height) ? (32f / (float)texture.Width) : (32f / (float)texture.Height));
					}
					num *= slot.scale;
					
					if (slot.MyItem.stack > 1) {
						sb.DrawString(Main.fontItemStack, string.Concat(slot.MyItem.stack), new Vector2(slot.pos.X + 10f * slot.scale, slot.pos.Y + 26f * slot.scale), Color.White * slot.alpha, 0f, default(Vector2), num, SpriteEffects.None, 0f);
					}
					if (slot.modBase == null && slot.type == "Hotbar") {
						string text = (slot.index == 9) ? "0" : string.Concat(slot.index + 1);
						Color value = (Main.localPlayer.selectedItem == slot.index) ? new Color(0, 255, 0, 50) : Main.inventoryBack;
						sb.DrawString(Main.fontItemStack, text, new Vector2(slot.pos.X + 6f, slot.pos.Y + 4f), value * slot.alpha, 0f, default(Vector2), slot.scale * 0.8f, SpriteEffects.None, 0f);
						if (slot.MyItem.useAmmo > 0) {
							int useAmmo = slot.MyItem.useAmmo;
							int num2 = 0;
							for (int i = 0; i < 58; i++) {
								if (Main.localPlayer.inventory[i].ammo == useAmmo) {
									num2 += Main.localPlayer.inventory[i].stack;
								}
							}
							sb.DrawString(Main.fontItemStack, string.Concat(num2), slot.pos + new Vector2(8f, 30f) * slot.scale, Color.White * slot.alpha, 0f, default(Vector2), slot.scale * 0.8f, SpriteEffects.None, 0f);
						}
					}
					if (slot.modBase == null && slot.type == "Inventory" && slot.index < 10) {
						string text2 = (slot.index == 9) ? "0" : string.Concat(slot.index + 1);
						Color value2 = (Main.localPlayer.selectedItem == slot.index) ? new Color(0, 255, 0, 50) : Main.inventoryBack;
						sb.DrawString(Main.fontItemStack, text2, new Vector2(slot.pos.X + 6f, slot.pos.Y + 4f), value2 * slot.alpha, 0f, default(Vector2), slot.scale * 0.8f, SpriteEffects.None, 0f);
					}
				}
				return false;
			}
			return true;
		}
	}
}