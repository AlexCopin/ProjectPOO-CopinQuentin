using UnityEngine;
using UnityEngine.UI;
class RandomGenerator : Generator
{
    Button _button;
    EquipmentGenerator _equipmentGenerator;
    ConsumableGenerator _consumableGenerator;
    void Awake()
    {
        _button = GameObject.Find("RandomButton").GetComponent<Button>();
        _button.onClick.AddListener(Generate);
        _equipmentGenerator = GameObject.Find("Scripts").GetComponent<EquipmentGenerator>();
        _consumableGenerator = GameObject.Find("Scripts").GetComponent<ConsumableGenerator>();
    }
    public void Generate()
    {
        var rand = Random.Range(0, 2);
        if (rand == 0)
        {
            _equipmentGenerator.category = (Equipment.Category) rand;
            _equipmentGenerator.Generate();
        }
        else
        {
            _consumableGenerator.category = (Consumable.Category) rand;
            _consumableGenerator.Generate();
        }
    }
}