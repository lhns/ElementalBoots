using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TAPI;
using Terraria;

namespace LolHens.Items {
	public class LolHensItem : ModItem {
		public LolHensBase lolHensBase = null;
		public bool loaded = false;
		
		private float usedTime = 0;
		public float usedPercent = 0;
		
		private int playerDeaths = 0;
		
		public LolHensItem() {
			if (modBase is LolHensBase) lolHensBase = (LolHensBase) modBase;
			LolHensBase.items.Add(this);
			if (LolHensBase.loaded) {
				OnItemLoaded();
				loaded = true;
			}
		}
		
		public virtual void OnItemLoaded() {
			PreLoadTextures();
			if (!Main.dedServ) LoadTextures();
			PostLoadTextures();
		}
		
		public override void Effects(Player player) {
			if (playerDeaths < LolHensPlayer.playerDeaths) {
				playerDeaths++;
				PlayerDied(player);
			}
        }
		
		public override bool? UseItem(Player player) {
			usedPercent = ++usedTime / (float)(item.useAnimation - 1);
			if (usedPercent >= 1) {
				UseItemPost(player);
				usedTime = 0;
				usedPercent = 0;
				return false;
			}
			return null;
		}
		
		public void UseAmmo(Player player) {
			for (int i = 0; i < 58; i++) {
				if (player.inventory[i].ammo == player.inventory[player.selectedItem].useAmmo && player.inventory[i].stack > 0) {
					player.inventory[i].stack--;
					if (player.inventory[i].stack == 0) player.inventory[i].SetDefaults(0);
					return;
				}
			}
		}
		
		public virtual void PreLoadTextures() {}
		
		public virtual void LoadTextures() {}
		
		public virtual void PostLoadTextures() {}
		
		public virtual void PlayerDied(Player player) {}
		
		public virtual void UseItemPost(Player player) {}
	}
}