using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Object
{


    public string LabelMat { get; private set; }
    public string LabelStamina { get; private set; }
    public string LabelStat { get; private set; }
    public string LabelSlot { get; private set; }
    public string LabelEffect { get; private set; }
    public int Stamina { get; private set; }
    public int Stat { get; private set; }

    public Armor()
    {
    }
    public Armor(Element.Material mat, Element.Item slot, Element.Stones mainStat, Element.Effects effect)
    {
        LabelStamina = "Stamina";
        Stamina = 50;
        Stat = 40;
        switch (mat)
        {
            case Element.Material.CLOTH:
                LabelMat = "Cloth";
                Stamina -= 10;
                break;
            case Element.Material.LEATHER:
                LabelMat = "Leather";
                break;
            case Element.Material.MAIL:
                LabelMat = "Mail";
                break;
            case Element.Material.PLATE:
                LabelMat = "Plate";
                Stamina += 10;
                break;
        }

        switch (slot)
        {
            case Element.Item.HEAD:
                LabelSlot = "Head";
                break;
            case Element.Item.SHOULDER:
                LabelSlot = "Shoulder";
                break;
            case Element.Item.CHEST:
                LabelSlot = "Chest";
                Stamina += 10;
                Stat += 10;
                break;
            case Element.Item.HANDS:
                LabelSlot = "Hands";
                Stamina -= 10;
                Stat -= 10;
                break;
            case Element.Item.LEGS:
                LabelSlot = "Legs";
                Stamina += 10;
                Stat += 10;
                break;
            case Element.Item.SHOES:
                LabelSlot = "Shoes";
                Stamina -= 10;
                Stat -= 10;
                break;
        }

        switch (mainStat)
        {
            case Element.Stones.INTELLIGENCE:
                LabelStat = "Intelligence";
                break;
            case Element.Stones.AGILITY:
                LabelStat = "Agility";
                break;
            case Element.Stones.STRENGTH:
                LabelStat = "Strength";
                break;
        }

        switch (effect)
        {
            case Element.Effects.NONE:
                LabelEffect = "None";
                break;
            case Element.Effects.BOOSTEDSTAT:
                LabelEffect = "Boostedstat";
                Stat *= 2;
                break;
            case Element.Effects.BOOSTEDSTAM:
                LabelEffect = "Boostedstat";
                Stamina *= 2;
                break;
            case Element.Effects.THORN:
                LabelEffect = "Thorn";
                break;
            case Element.Effects.ICESHIELD:
                LabelEffect = "Iceshield";
                break;
        }
    }
}
