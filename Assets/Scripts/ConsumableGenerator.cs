using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
class ConsumableGenerator : Generator
{
    Dropdown _dropdown;
    Button _button;
    GameObject _log;
    [Header("Sprites - Consumables")]
    public Sprite[] spritesCurse;
    public Sprite[] spritesRot;
    public Sprite[] spritesPoison;
    public Sprite[] spritesHealth;
    public Sprite[] spritesSpeed;
    public Sprite[] spritesResource;
    public List<string> types;
    public Consumable.Category category;
    void Awake()
    {
        types.Add("Select category");
        types.Add("Bonus");
        types.Add("Malus");
        _dropdown = GameObject.Find("DropDownCons").GetComponent<Dropdown>();
        _button = GameObject.Find("ButtonConsumable").GetComponent<Button>();
        _button.onClick.AddListener(delegate { if (_dropdown.value != 0) Generate(); });
        _dropdown.ClearOptions();
        _dropdown.GetComponent<Dropdown>().AddOptions(types);
        _log = GameObject.Find("Log2");
        _log.SetActive(false);
    }
    void Update()
    {
        _button.GetComponent<Image>().color = _dropdown.value != 0 ? Color.white : Color.gray;
        _button.enabled = _dropdown.value != 0 ? true : false;
        if (_dropdown.value != 0)
            category = (Consumable.Category) System.Enum.Parse(typeof(Consumable.Category), _dropdown.options[_dropdown.value].text);
    }
    public void Generate()
    {
        Consumable consumable = new Consumable(category);
        Inventory.GetInstance().elements.Add(consumable);
        int rand;
        switch (consumable.ConName)
        {
            case Consumable.ConsumableName.Poison:
                rand = Random.Range(0, spritesPoison.Length - 1);
                consumable.elementDisplay = spritesPoison[rand];
                break;
            case Consumable.ConsumableName.Curse:
                rand = Random.Range(0, spritesCurse.Length - 1);
                consumable.elementDisplay = spritesCurse[rand];
                break;
            case Consumable.ConsumableName.Rot:
                rand = Random.Range(0, spritesRot.Length - 1);
                consumable.elementDisplay = spritesRot[rand];
                break;
            case Consumable.ConsumableName.HealthPotion:
                rand = Random.Range(0, spritesHealth.Length - 1);
                consumable.elementDisplay = spritesHealth[rand];
                break;
            case Consumable.ConsumableName.ResourcePotion:
                rand = Random.Range(0, spritesResource.Length - 1);
                consumable.elementDisplay = spritesResource[rand];
                break;
            case Consumable.ConsumableName.SpeedPotion:
                rand = Random.Range(0, spritesSpeed.Length - 1);
                consumable.elementDisplay = spritesSpeed[rand];
                break;
        }
        Log(consumable);
        StartCoroutine(LogCoroutine(_log));
    }
    protected override void Log(Element element)
    {
        var e = (Consumable) element;
        Debug.Log("<b>Element #" + e.id +
            "</b> - Type: " + e.type +
            ", Categorie: " + e.category +
            ", Name: " + e.name +
            ", Price: " + e.price);
    }
}