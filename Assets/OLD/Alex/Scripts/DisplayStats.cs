using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayStats : MonoBehaviour
{
    public Armor displayArmorStats;
    public GameObject statsUI;
    // Start is called before the first frame update
    

    private void Awake()
    {
        Debug.Log(this.name);
        statsUI = GameObject.Find(this.name + "Stats");
        statsUI.SetActive(false);
        
    }
    private void OnMouseOver()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 20;
        statsUI.SetActive(true);
        if(Crafting.allArmors.Exists(x => x.LabelSlot.Contains(this.name)))
        {
            displayArmorStats = Crafting.allArmors.Find(x => x.LabelSlot == this.name);
            Debug.Log(displayArmorStats.LabelEffect);
            statsUI.GetComponent<TMP_Text>().text = displayArmorStats.LabelSlot + "\n" + displayArmorStats.LabelMat + "\n" + displayArmorStats.LabelStamina + " : " + displayArmorStats.Stamina + "\n" + displayArmorStats.LabelStat + " : " + displayArmorStats.Stat + "\n" + displayArmorStats.LabelEffect;
        }
        statsUI.transform.position = worldPosition;
    }
    private void OnMouseExit()
    {
        statsUI.GetComponent<TMP_Text>().text = "";
        statsUI.SetActive(false);
    }
}
