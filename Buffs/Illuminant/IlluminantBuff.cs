using Terraria;

namespace ElementalBoots.Buffs.Illuminant
{
    class IlluminantBuff: MBuff
    {
        public override void Update2(Entity entity, ref int buffIndex)
        {
            Lighting.AddLight((int)(entity.Center.X / 16f), (int)(entity.Center.Y / 16f), 0.9f, 0.5f, 0.95f);
        }
    }
}