using UnityEngine;
class Element
{
    public enum Type
    {
        Equipment,
        Consumable
    }
    public Sprite elementDisplay { get; set; }
    public Type type { get; protected set; }
    public int id { get; protected set; }
    public float price { get; protected set; }
    public Element(Type type)
    {
        this.type = type;
        id = Inventory.GetInstance().elements.Count;
    }
}
class Equipment : Element
{
    int _offset;
    public enum Category
    {
        Attack,
        Defense
    }
    public enum Name
    {
        Sword,
        Axe,
        Bow,
        Helmet,
        Chestplate,
        Shield
    }
    public enum Rarity
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary,
        Artefact
    }
    public Category category { get; private set; }
    public Name name { get; private set; }
    public Rarity rarity { get; private set; }
    public string staminaLabel { get; private set; }
    public float staminaValue { get; private set; }
    public string strengthLabel { get; private set; }
    public float strengthValue { get; private set; }
    public Equipment(Category category) : base(Element.Type.Equipment)
    {
        this.category = category;
        staminaLabel = "Stamina";
        strengthLabel = "Strength";
        _offset = category > 0 ? 3 : 0;
        staminaValue = Random.Range(4, 6);
        strengthValue = Random.Range(3, 5);
        name = (Name) Random.Range(_offset, _offset + 3);
        rarity = (Rarity) Random.Range(0, 6);
        switch (rarity)
        {
            case Rarity.Common:
                staminaValue *= 2;
                strengthValue *= 2;
                break;
            case Rarity.Uncommon:
                staminaValue *= 3;
                strengthValue *= 3;
                break;
            case Rarity.Rare:
                staminaValue *= 4;
                strengthValue *= 4;
                break;
            case Rarity.Epic:
                staminaValue *= 5;
                strengthValue *= 5;
                break;
            case Rarity.Legendary:
                staminaValue *= 7;
                strengthValue *= 7;
                break;
            case Rarity.Artefact:
                staminaValue *= 10;
                strengthValue *= 10;
                break;
        }
        price = _offset + 1 * 10;
    }
}
class Consumable : Element
{
    public enum Category
    {
        Bonus,
        Malus
    }
    public enum ConsumableName
    {
        HealthPotion,
        SpeedPotion,
        ResourcePotion,
        Poison,
        Curse,
        Rot
    }
    public ConsumableName ConName { get; private set; }
    public Category category { get; private set; }
    public string name { get; private set; }
    public string EffectLabel { get; private set; }
    public Consumable(Category PotionType) : base(Element.Type.Consumable)
    {
        float randChance;
        randChance = Random.Range(0.0f, 1.0f);
        switch (PotionType)
        {
            case Category.Bonus:
                if (randChance < 0.33f)
                {
                    category = Category.Bonus;
                    ConName = ConsumableName.HealthPotion;
                    name = ConName.ToString();
                    EffectLabel = "Restore Health Points(HP)";
                    price = 10;
                }
                else if (randChance >= 0.33f && randChance < 0.66f)
                {
                    category = Category.Bonus;
                    ConName = ConsumableName.SpeedPotion;
                    name = ConName.ToString();
                    EffectLabel = "Multiply your speed by 2";
                    price = 20;
                }
                else if (randChance >= 0.66f)
                {
                    category = Category.Bonus;
                    ConName = ConsumableName.ResourcePotion;
                    name = ConName.ToString();
                    EffectLabel = "Fill up your resource pool";
                    price = 30;
                }
                break;
            case Category.Malus:
                if (randChance < 0.33f)
                {
                    category = Category.Malus;
                    ConName = ConsumableName.Poison;
                    name = ConName.ToString();
                    EffectLabel = "Deal 50 damage to the target";
                    price = 40;
                }
                else if (randChance >= 0.33f && randChance < 0.66f)
                {
                    category = Category.Malus;
                    ConName = ConsumableName.Curse;
                    name = ConName.ToString();
                    EffectLabel = "Target's health is reduced by 30%";
                    price = 50;
                }
                else if (randChance >= 0.66f)
                {
                    category = Category.Malus;
                    ConName = ConsumableName.Rot;
                    name = ConName.ToString();
                    EffectLabel = "Slow the target by 50% for 10sec";
                    price = 60;
                }
                break;
        }
    }
}