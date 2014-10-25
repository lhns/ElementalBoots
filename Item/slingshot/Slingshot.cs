using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TAPI;
using TAPI.UIKit;
using Terraria;

namespace LolHens.Items {
    public class Slingshot : LolHensItem, ICustomRenderedItem {
		public bool shot = false;
		private Texture2D[] textures = new Texture2D[7];
		
        public Slingshot(ModBase modBase, Item i) : base(modBase, i) {}
		
		public override void LoadTextures() {
			textures[0] = modBase.textures["Item/slingshot/Slingshot.png"];
			textures[1] = modBase.textures["Item/slingshot/Slingshot2.png"];
			textures[2] = modBase.textures["Item/slingshot/Slingshot3.png"];
			textures[3] = modBase.textures["Item/slingshot/Slingshot4.png"];
			textures[4] = modBase.textures["Item/slingshot/Slingshot5.png"];
			textures[5] = modBase.textures["Item/slingshot/Slingshot6.png"];
			textures[6] = modBase.textures["Item/slingshot/Slingshot7.png"];
		}
		
		public override bool? UseItem(Player player) {
			base.UseItem(player);
			
			if (usedPercent > 0.9) {
				Main.itemTexture[item.type] = textures[0];
			} else if (usedPercent > 0.8f) {
				Main.itemTexture[item.type] = textures[5];
			} else if (usedPercent > 0.7f) {
				Main.itemTexture[item.type] = textures[6];
				if (!shot) {
					shot = true;
					Vector2 vector = new Vector2(player.Center.X, player.position.Y);
					Vector2 velocity = new Vector2((float)Main.mouseX + Main.screenPosition.X - vector.X, (float)Main.mouseY + Main.screenPosition.Y - vector.Y);
					float hyp = (float)System.Math.Sqrt((double)(velocity.X * velocity.X + velocity.Y * velocity.Y));
					double deg = (Math.Atan((-velocity.Y) / (velocity.X * player.direction)) / (2 * Math.PI) * 360);
					if (velocity.X * player.direction < 0) deg *= -1;
					deg = Math.Max(Math.Min(deg, 50), -50);
					double ang = deg / 360 * (2 * Math.PI);
					velocity.X = (float)(Math.Cos(ang) * hyp) * player.direction;
					velocity.Y = (float)(Math.Sin(ang) * hyp) * -1;
					float norm = 20f / hyp;
					velocity.X *= norm;
					velocity.Y *= norm;
					UseAmmo(player);
					Projectile.NewProjectile(vector.X, vector.Y, velocity.X, velocity.Y, ProjDef.byName["LolHens:Acorn"].type, item.damage, item.knockBack, player.whoAmI);
					Main.PlaySound(2, (int)vector.X, (int)vector.Y, 5);
				}
			} else {
				Main.itemTexture[item.type] = textures[(int) Math.Min(4, usedPercent * 8)];
			}
			return false;
		}
		
		public override void UseItemPost(Player player) {
			Main.itemTexture[item.type] = textures[0];
			shot = false;
		}
		
		public int PreDrawItemSlotItem(Item item, Color color, SpriteBatch sb, ItemSlot slot) {
			float num = 1f;
			Texture2D texture = slot.MyItem.GetTexture();
			if (Main.localPlayer.selectedItem != slot.index) texture = textures[0];
			if (texture.Width > 32 || texture.Height > 32) {
				num = ((texture.Width > texture.Height) ? (32f / (float)texture.Width) : (32f / (float)texture.Height));
			}
			num *= slot.scale * 1.6f;
			sb.Draw(texture, new Vector2(slot.pos.X + 26f * slot.scale - (float)texture.Width * 0.5f * num, slot.pos.Y + 26f * slot.scale - (float)texture.Height * 0.5f * num), null, color, 0f, new Vector2(0.5f, 0.5f), num, SpriteEffects.None, 0f);
			return 1;
		}
	}
}