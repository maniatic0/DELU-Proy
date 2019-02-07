namespace MoreTags
{
    public static class Tag
    {
        public static AllTags all = new AllTags();
        public static DamageableGroup Damageable = new DamageableGroup("Damageable");
        public static HealableGroup Healable = new HealableGroup("Healable");
    }

    public class DamageableGroup : TagGroup
    {
        public DamageableGroup(string name) : base(name) { }
        public TagName Player = new TagName("Damageable.Player");
        public TagName Enemy = new TagName("Damageable.Enemy");
        public TagName Object = new TagName("Damageable.Object");
    }

    public class HealableGroup : TagGroup
    {
        public HealableGroup(string name) : base(name) { }
        public TagName Player = new TagName("Healable.Player");
        public TagName Enemy = new TagName("Healable.Enemy");
    }

    public class AllTags : TagNames
    {
        public AllTags() : base(TagSystem.AllTags()) { }
        public TagChildren Player = new TagChildren("Player");
        public TagChildren Enemy = new TagChildren("Enemy");
        public TagChildren Object = new TagChildren("Object");
    }
}
