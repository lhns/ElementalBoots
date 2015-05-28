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
        private Resolver recipeResolver;
        private Item item;

        public UniversalRecipeResolver(Item item, JsonData json)
        {
            this.recipeResolver = new FixedRecipeResolver(item, json);
            this.item = item;
        }

        public override void Resolve()
        {
            try {
                if (item.def.modBase == null)
                {
                    item.def.modBase = MBase.GetDummyVanillaModBase();

                    recipeResolver.Resolve();

                    item.def.modBase = null;
                }
                else
                {
                    recipeResolver.Resolve();
                }
            } catch (Exception e) {
                TConsole.Print(e);
            }
        }
    }
}