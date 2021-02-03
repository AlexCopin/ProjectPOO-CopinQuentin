using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SlotScript : MonoBehaviour
{
    TMP_Dropdown choices;
    public enum ElementType
    {
        MAT,
        ITEM,
        STONE,
        EFFECT
    }
    public ElementType type;
    List<string> dropOptions = new List<string>();
    public void Start()
    {
        choices = this.GetComponent<TMP_Dropdown>();
        choices.ClearOptions();
        switch (type)
        {
            case ElementType.MAT:
                dropOptions.Add("NONE");
                dropOptions.Add("CLOTH");
                dropOptions.Add("LEATHER");
                dropOptions.Add("MAIL");
                dropOptions.Add("PLATE");
                choices.AddOptions(dropOptions);
                break;
            case ElementType.ITEM:
                dropOptions.Add("NONE");
                dropOptions.Add("HEAD");
                dropOptions.Add("SHOULDER");
                dropOptions.Add("CHEST");
                dropOptions.Add("HANDS");
                dropOptions.Add("LEGS");
                dropOptions.Add("SHOES");
                choices.AddOptions(dropOptions);
                break;
            case ElementType.STONE:
                dropOptions.Add("NONE");
                dropOptions.Add("INTELLIGENCE");
                dropOptions.Add("AGILITY");
                dropOptions.Add("STRENGTH");
                choices.AddOptions(dropOptions);
                break;
            case ElementType.EFFECT:
                dropOptions.Add("NONE");
                dropOptions.Add("THORN");
                dropOptions.Add("ICESHIELD");
                dropOptions.Add("BOOSTED STAT");
                dropOptions.Add("BOOSTED STAMINA");
                choices.AddOptions(dropOptions);
                break;
        }
    }
}
