using LitJson;
using System;
using System.Collections;
using System.Linq;
using Terraria;
using TAPI;

namespace LolHens
{
    public class UniversalRecipeResolver : Resolver
    {
        private RecipeResolver resolver;
        private Item item;

        public UniversalRecipeResolver(Item item, JsonData json)
        {
            this.resolver = new RecipeResolver(item, json);
            this.item = item;
        }

        public override void Resolve()
        {
            try {
                if (item.def.modBase == null)
                {
                    item.def.modBase = MBase.GetDummyVanillaModBase();

                    resolver.Resolve();

                    item.def.modBase = null;
                }
                else
                {
                    resolver.Resolve();
                }
            } catch (Exception e) {
                TConsole.Print(e);
            }
        }
    }
}