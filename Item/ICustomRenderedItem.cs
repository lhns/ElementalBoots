using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TAPI;
using TAPI.UIKit;
using Terraria;

namespace LolHens.Items {
	public interface ICustomRenderedItem {
		int PreDrawItemSlotItem(Item item, Color color, SpriteBatch sb, ItemSlot slot);
	}
}