using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
class DisplayStats : MonoBehaviour
{
    GameObject StatsPanel;
    bool mouseOver;
    private void Awake()
    {
        StatsPanel = GameObject.Find("StatsPanel");
        EventTrigger evtr = gameObject.AddComponent<EventTrigger>() as EventTrigger;
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((eventData) => { OnPointerEnter(); });
        evtr.triggers.Add(entry);
        EventTrigger.Entry entry2 = new EventTrigger.Entry();
        entry2.eventID = EventTriggerType.PointerExit;
        entry2.callback.AddListener((eventData) => { OnPointerExit(); });
        evtr.triggers.Add(entry2);
        StatsPanel.GetComponent<Image>().enabled = false;
    }
    private void Update()
    {
        if (mouseOver) StatsPanel.transform.position = Input.mousePosition;
    }
    public void OnPointerEnter()
    {
        StatsPanel.GetComponent<Image>().enabled = true;
        mouseOver = true;
        string elemIndex = this.name.Substring(1);
        int elemIndexInt = int.Parse(elemIndex);
        DisplayStatsOnPanel(Inventory.GetInstance().elements.Find(x => x.id == elemIndexInt));
    }
    public void OnPointerExit()
    {
        StatsPanel.GetComponent<Image>().enabled = false;
        mouseOver = false;
        StatsPanel.GetComponentInChildren<Text>().text = "";
    }
    void DisplayStatsOnPanel(Element elem)
    {
        if (elem.type == Element.Type.Consumable)
        {
            Consumable TempCon = (Consumable) elem;
            Color colorCons = Color.white;
            switch (TempCon.ConName)
            {
                case Consumable.ConsumableName.HealthPotion:
                    colorCons = new Color(189f / 255f, 64f / 255f, 54f / 255f);
                    break;
                case Consumable.ConsumableName.SpeedPotion:
                    colorCons = new Color(231f / 255f, 225f / 255f, 0);
                    break;
                case Consumable.ConsumableName.ResourcePotion:
                    colorCons = new Color(0, 156f / 255f, 195f / 255f);
                    break;
                case Consumable.ConsumableName.Curse:
                    colorCons = new Color(198f / 255f, 25f / 255f, 189f / 255f);
                    break;
                case Consumable.ConsumableName.Rot:
                    colorCons = new Color(160f / 255f, 145f / 255f, 61f / 255f);
                    break;
                case Consumable.ConsumableName.Poison:
                    colorCons = new Color(165 / 255f, 201f / 255f, 0f);
                    break;
            }
            float fontS = (float) StatsPanel.GetComponentInChildren<Text>().fontSize * 0.7f;
            StatsPanel.GetComponentInChildren<Text>().text = elem.type + "#" + elem.id + "\n" + "\n" + "<b><color=#" + ColorUtility.ToHtmlStringRGB(colorCons) + ">" +
                TempCon.ConName + "</color></b>" + "\n" + "<size=" + fontS + ">" + "<i>" +
                TempCon.EffectLabel + "</i>" + "</size>" + "\nPrice: " + TempCon.price;
        }
        else if (elem.type == Element.Type.Equipment)
        {
            Equipment TempEq = (Equipment) elem;
            Color colorRar = Color.white;
            switch (TempEq.rarity)
            {
                case Equipment.Rarity.Common:
                    colorRar = Color.white;
                    break;
                case Equipment.Rarity.Uncommon:
                    colorRar = new Color(84 / 255f, 166 / 255f, 0);
                    break;
                case Equipment.Rarity.Rare:
                    colorRar = new Color(0, 49f / 255f, 155 / 255f);
                    break;
                case Equipment.Rarity.Epic:
                    colorRar = new Color(96f / 255f, 20f / 255f, 111f / 255f);
                    break;
                case Equipment.Rarity.Legendary:
                    colorRar = new Color(227f / 255f, 88f / 255f, 0);
                    break;
                case Equipment.Rarity.Artefact:
                    colorRar = new Color(171 / 255f, 0f, 0f);
                    break;
            }
            StatsPanel.GetComponentInChildren<Text>().text = elem.type + "#" + elem.id + "\n" + "\n" + "<b><color=#" + ColorUtility.ToHtmlStringRGB(colorRar) + ">" +
                TempEq.rarity + "</color></b>" + "\n" + "Category : " + TempEq.category + "\n" + "Name : " + TempEq.name + "\n" +
                TempEq.staminaLabel + " : " + TempEq.staminaValue + "\n" + TempEq.strengthLabel + " : " +
                TempEq.strengthValue + "\n" + "\nPrice: " + +TempEq.price;
        }
    }
}