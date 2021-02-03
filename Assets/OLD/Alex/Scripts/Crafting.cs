using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Crafting : MonoBehaviour
{
    public GameObject Slot1;
    public GameObject Slot2;
    public GameObject Slot3;
    public GameObject Slot4;
    public GameObject ButtonCraft;


    public GameObject slotHead;
    public GameObject slotShoulder;
    public GameObject slotChest;
    public GameObject slotHands;
    public GameObject slotLegs;
    public GameObject slotShoes;

    Element.Material armorMat;
    Element.Item armorSlot;
    Element.Stones armorStat;
    Element.Effects armorEffect;

    public static List<Armor> allArmors = new List<Armor>();
    public List<Sprite> sprites;

    List<TMP_Dropdown.OptionData> removedOptions = new List<TMP_Dropdown.OptionData>();

    private void Start()
    {
        Slot1 = GameObject.Find("Slot1");
        Slot2 = GameObject.Find("Slot2");
        Slot3 = GameObject.Find("Slot3");
        Slot4 = GameObject.Find("Slot4");

        slotHead = GameObject.Find("Head");
        slotShoulder = GameObject.Find("Shoulder");
        slotChest = GameObject.Find("Chest");
        slotHands = GameObject.Find("Hands");
        slotLegs = GameObject.Find("Legs");
        slotShoes = GameObject.Find("Shoes");

        ButtonCraft = GameObject.Find("ButtonCraft");
    }

    public void ChooseType()
    {
        switch (Slot1.GetComponent<TMP_Dropdown>().value)
        {
            case 0: // None
                armorMat = Element.Material.NONE;
                break;
            case 1: //Cloth
                armorMat = Element.Material.CLOTH;
                break;
            case 2: //Leather
                armorMat = Element.Material.LEATHER;
                break;
            case 3: //Mail
                armorMat = Element.Material.MAIL;
                break;
            case 4: //Plaque
                armorMat = Element.Material.PLATE;
                break;
        }

        switch (Slot2.GetComponent<TMP_Dropdown>().options[Slot2.GetComponent<TMP_Dropdown>().value].text)
        {
            case "NONE": // none 
                armorSlot = Element.Item.NONE;
                break;
            case "HEAD": // head
                armorSlot = Element.Item.HEAD;
                break;
            case "SHOULDER": // shoulder
                armorSlot = Element.Item.SHOULDER;
                break;
            case "CHEST": // Chest
                armorSlot = Element.Item.CHEST;
                break;
            case "HANDS": // Hands
                armorSlot = Element.Item.HANDS;
                break;
            case "LEGS": // Legs
                armorSlot = Element.Item.LEGS;
                break;
            case "SHOES": // Shoes
                armorSlot = Element.Item.SHOES;
                break;
        }

        switch (Slot3.GetComponent<TMP_Dropdown>().value)
        {
            case 0: // None
                armorStat = Element.Stones.NONE;
                break;
            case 1: // Intel
                armorStat = Element.Stones.INTELLIGENCE;
                break;
            case 2: // Agi
                armorStat = Element.Stones.AGILITY;
                break;
            case 3: //Stren
                armorStat = Element.Stones.STRENGTH;
                break;
        }

        switch (Slot4.GetComponent<TMP_Dropdown>().value)
        {

            case 0: //none
                armorEffect = Element.Effects.NONE;
                break;
            case 1: //Thorn
                armorEffect = Element.Effects.THORN;
                break;
            case 2: //IceShield
                armorEffect = Element.Effects.ICESHIELD;
                break;
            case 3: //Boosted main stat
                armorEffect = Element.Effects.BOOSTEDSTAT;
                break;
            case 4: //Boosted stamina
                armorEffect = Element.Effects.BOOSTEDSTAM;
                break;
        }
    }

    private void Update()
    {
        if (Slot1.GetComponent<TMP_Dropdown>().value == 0 || Slot2.GetComponent<TMP_Dropdown>().value == 0 || Slot3.GetComponent<TMP_Dropdown>().value == 0) 
        {
            ButtonCraft.SetActive(false);
        }
        else
        {
            ButtonCraft.SetActive(true);
        }
    }

    public void CreateArmor()
    {
        Armor newArmor = new Armor(armorMat, armorSlot, armorStat, armorEffect);
        allArmors.Add(newArmor);

        ChangeSprites();

        removedOptions.Add(Slot2.GetComponent<TMP_Dropdown>().options[Slot2.GetComponent<TMP_Dropdown>().value]);
        Slot2.GetComponent<TMP_Dropdown>().options.RemoveAt(Slot2.GetComponent<TMP_Dropdown>().value);
        Slot1.GetComponent<TMP_Dropdown>().value = 0;
        Slot2.GetComponent<TMP_Dropdown>().value = 0;
        Slot3.GetComponent<TMP_Dropdown>().value = 0;
        Slot4.GetComponent<TMP_Dropdown>().value = 0;
        Debug.Log(newArmor.LabelMat);
        Debug.Log("Armor added");
    }

    public void DeleteArmor()
    {
        allArmors.Clear();
        slotHead.GetComponent<SpriteRenderer>().color = Color.black;
        slotShoulder.GetComponent<SpriteRenderer>().color = Color.black;
        slotChest.GetComponent<SpriteRenderer>().color = Color.black;
        slotHands.GetComponent<SpriteRenderer>().color = Color.black;
        slotLegs.GetComponent<SpriteRenderer>().color = Color.black;
        slotShoes.GetComponent<SpriteRenderer>().color = Color.black;

        Slot2.GetComponent<TMP_Dropdown>().AddOptions(removedOptions);
        Debug.Log("Equipement reset");
    }

    public void ChangeSprites()
    {
        foreach (Armor armor in allArmors)
        {
            string displayArmor = armor.LabelSlot + armor.LabelMat + armor.LabelEffect;
            Debug.Log(displayArmor);
            switch (armor.LabelSlot)
            {
                case "Head":
                    slotHead.GetComponent<SpriteRenderer>().color = Color.white;
                    slotHead.GetComponent<SpriteRenderer>().sprite = sprites.Find(x => x.name == displayArmor);
                    break;
                case "Shoulder":
                    slotShoulder.GetComponent<SpriteRenderer>().color = Color.white;
                    slotShoulder.GetComponent<SpriteRenderer>().sprite = sprites.Find(x => x.name == displayArmor);
                    break;
                case "Chest":
                    slotChest.GetComponent<SpriteRenderer>().color = Color.white;
                    slotChest.GetComponent<SpriteRenderer>().sprite = sprites.Find(x => x.name == displayArmor);
                    break;
                case "Hands":
                    slotHands.GetComponent<SpriteRenderer>().color = Color.white;
                    slotHands.GetComponent<SpriteRenderer>().sprite = sprites.Find(x => x.name == displayArmor);
                    break;
                case "Legs":
                    slotLegs.GetComponent<SpriteRenderer>().color = Color.white;
                    slotLegs.GetComponent<SpriteRenderer>().sprite = sprites.Find(x => x.name == displayArmor);
                    break;
                case "Shoes":
                    slotShoes.GetComponent<SpriteRenderer>().color = Color.white;
                    slotShoes.GetComponent<SpriteRenderer>().sprite = sprites.Find(x => x.name == displayArmor);
                    break;
            }
        }
    }
}


