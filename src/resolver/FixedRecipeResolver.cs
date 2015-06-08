using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LitJson;

using TAPI;
using Terraria;

namespace LolHens
{
    class FixedRecipeResolver : Resolver
    {
        private Item item;
        private JsonData j;

        public FixedRecipeResolver(Item item, JsonData json)
        {
            this.item = item;
            this.j = json;
        }

        public override void Resolve()
        {
            string internalName = this.item.def.modBase.mod.InternalName;
            Recipe newRecipe = Recipe.newRecipe;
            newRecipe.createItem.SetDefaults(this.item.netID, false);
            newRecipe.createItem.stack = 1;
            if (this.j.Has("creates") && this.j["creates"].IsInt)
            {
                newRecipe.createItem.stack = (int)this.j["creates"];
            }
            if (this.j.Has("items") && this.j["items"].IsObject)
            {
                foreach (string text in ((System.Collections.IDictionary)this.j["items"]).Keys)
                {
                    if (text.StartsWith("g:") && !ItemDef.byName.ContainsKey(text))
                    {
                        Item item = new Item();
                        item.SetDefaults(text);
                        item.type = ItemDef.nextType++;
                        item.netID = item.type;
                        item.stack = 1;
                        item.name = text;
                        ItemDef.byName.Add(item.name, item);
                        ItemDef.byType.Add(item.type, item);
                        item.stack = (int)this.j["items"][text];
                        item.material = true;
                        Recipe.newRecipe.requiredItem.Add(item);
                    }
                    else
                    {
                        Item item2 = new Item();
                        if (ItemDef.byName.ContainsKey(internalName + ":" + text))
                        {
                            item2.SetDefaults(internalName + ":" + text);
                        }
                        else if (ItemDef.byName.ContainsKey("Vanilla:" + text))
                        {
                            item2.SetDefaults("Vanilla:" + text);
                        }
                        else
                        {
                            if (!ItemDef.byName.ContainsKey(text))
                            {
                                throw new Mods.LoadException(string.Concat(new string[]
								{
									"No item \"",
									text,
									"\" found for recipe of item \"",
									this.item.name,
									"\""
								}));
                            }
                            item2.SetDefaults(text);

                            if (text.StartsWith("g:"))
                            {
                                MBase.instance.eventRegistry.Register((Events.AllModsLoaded e) => item2.displayName = ItemDef.byType[item2.type].displayName);
                            }
                        }

                        item2.stack = (int)this.j["items"][text];
                        item2.material = true;
                        if (item2.def != null)
                        {
                            item2.def.item.material = true;
                        }
                        else
                        {
                            ItemDef.byName[item2.name].material = true;
                            ItemDef.byType[item2.type].material = true;
                        }
                        newRecipe.requiredItem.Add(item2);
                    }
                }
            }
            if (this.j.Has("tiles") && !Main.dedServ)
            {
                for (int i = 0; i < this.j["tiles"].Count; i++)
                {
                    string tile = (string)this.j["tiles"][i];
                    if (tile == "@Water")
                    {
                        Recipe.newRecipe.needWater = true;
                    }
                    else if (tile == "@Honey")
                    {
                        Recipe.newRecipe.needHoney = true;
                    }
                    else if (tile == "@Lava")
                    {
                        Recipe.newRecipe.needLava = true;
                    }
                    else
                    {
                        if (tile.Equals("Iron Anvil") || tile.Equals("Lead Anvil") || tile.Equals("Iron Anvil or Lead Anvil"))
                        {
                            tile = "Anvil";
                        }
                        if (tile.Equals("Mythril Anvil") || tile.Equals("Orichalcum Anvil"))
                        {
                            tile = "Mythril or Orichalcum Anvil";
                        }
                        if (tile.Equals("Adamantite Forge") || tile.Equals("Titanium Forge"))
                        {
                            tile = "Adamantite or Titanium Forge";
                        }
                        int[] array = (
                            from value in TileDef.displayName.Select(delegate(string value, int num)
                            {
                                if (string.IsNullOrEmpty(value))
                                {
                                    return -1;
                                }
                                if (!value.Equals(tile))
                                {
                                    return -1;
                                }
                                return num;
                            })
                            where value != -1
                            select value).ToArray<int>();
                        if (array.Length < 1)
                        {
                            throw new Mods.LoadException(string.Concat(new string[]
							{
								"No tile \"",
								tile,
								"\" found for recipe of item \"",
								this.item.name,
								"\""
							}));
                        }
                        int num3 = 0;
                        for (int j = 0; j < array.Length; j++)
                        {
                            int num2 = array[j];
                            if (TileDef.tileModBase[num2] != null && TileDef.tileModBase[num2].mod.InternalName.Equals(internalName))
                            {
                                num3 = j;
                                break;
                            }
                        }
                        newRecipe.requiredTile.Add(array[num3]);
                    }
                }
            }
            Recipe.AddRecipe();
        }
    }
}
