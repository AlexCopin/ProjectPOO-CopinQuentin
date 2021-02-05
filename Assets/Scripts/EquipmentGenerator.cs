using System.Collections;
using UnityEngine;
using UnityEngine.UI;
class EquipmentGenerator : Generator
{
    Dropdown _dropdown;
    Button _button;
    GameObject _log;
    [Header("Sprites - Equipments")]
    public Sprite[] spritesChest;
    public Sprite[] spritesShield;
    public Sprite[] spritesHelmet;
    public Sprite[] spritesBow;
    public Sprite[] spritesSword;
    public Sprite[] spritesAxe;
    public Equipment.Category category;
    void Awake()
    {
        _dropdown = GameObject.Find("Dropdown0").GetComponent<Dropdown>();
        _button = GameObject.Find("Button0").GetComponent<Button>();
        _button.onClick.AddListener(delegate { if (_dropdown.value != 0) Generate(); });
        _log = GameObject.Find("Log");
        _log.SetActive(false);
    }
    void Update()
    {
        _button.GetComponent<Image>().color = _dropdown.value != 0 ? Color.white : Color.gray;
        _button.enabled = _dropdown.value != 0 ? true : false;
        if (_dropdown.value != 0)
            category = (Equipment.Category) System.Enum.Parse(typeof(Equipment.Category), _dropdown.options[_dropdown.value].text);
    }
    public void Generate()
    {
        Equipment equipment = new Equipment(category);
        Inventory.GetInstance().elements.Add(equipment);
        int rand;
        switch (equipment.name)
        {
            case Equipment.Name.Sword:
                rand = Random.Range(0, spritesSword.Length - 1);
                equipment.elementDisplay = spritesSword[rand];
                break;
            case Equipment.Name.Axe:
                rand = Random.Range(0, spritesAxe.Length - 1);
                equipment.elementDisplay = spritesAxe[rand];
                break;
            case Equipment.Name.Bow:
                rand = Random.Range(0, spritesBow.Length - 1);
                equipment.elementDisplay = spritesBow[rand];
                break;
            case Equipment.Name.Helmet:
                rand = Random.Range(0, spritesHelmet.Length - 1);
                equipment.elementDisplay = spritesHelmet[rand];
                break;
            case Equipment.Name.Chestplate:
                rand = Random.Range(0, spritesChest.Length - 1);
                equipment.elementDisplay = spritesChest[rand];
                break;
            case Equipment.Name.Shield:
                rand = Random.Range(0, spritesShield.Length - 1);
                equipment.elementDisplay = spritesShield[rand];
                break;
        }
        Log(equipment);
        StartCoroutine(LogCoroutine(_log));
    }
    protected override void Log(Element element)
    {
        var e = (Equipment) element;
        Debug.Log("<b>Element #" + e.id +
            "</b> - Type: " + e.type +
            ", Rarity: " + e.rarity +
            ", Categorie: " + e.category +
            ", Name: " + e.name +
            ", Price: " + e.price);
    }
}