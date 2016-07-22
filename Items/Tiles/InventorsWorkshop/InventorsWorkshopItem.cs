using Terraria.ID;
using Terraria.ModLoader;

namespace ElementalBoots.Items.Tiles.InventorsWorkshop
{
    public class InventorsWorkshopItem : MItem
    {
        public override void SetDefaults()
        {
            item.name = "Inventor's Workshop";
            item.maxStack = 250;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.value = 10*Value.SILVER;
            item.rare = 1;
            item.createTile = mod.TileType("InventorsWorkshop");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.TinkerersWorkshop);
            recipe.AddIngredient(ItemID.Wood, 10);
            recipe.AddIngredient(ItemID.WoodenTable);
            recipe.AddIngredient(ItemID.SpellTome);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}