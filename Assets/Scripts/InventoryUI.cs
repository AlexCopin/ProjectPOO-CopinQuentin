using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
class InventoryUI : MonoBehaviour
{
    bool _isInventoryVisible;
    int _index;
    Inventory _inventoryInstance;
    Button _inventoryButton, _sortByIdButton, _sortByTypeButton, _sortByPriceButton;
    GameObject _generatorsLayer, _inventoryLayer;
    List<GameObject> _gameObjects = new List<GameObject>();
    void Awake()
    {
        _inventoryInstance = Inventory.GetInstance();
        _inventoryButton = GameObject.Find("InventoryButton").GetComponent<Button>();
        _sortByIdButton = GameObject.Find("SortByIdButton").GetComponent<Button>();
        _sortByTypeButton = GameObject.Find("SortByTypeButton").GetComponent<Button>();
        _sortByPriceButton = GameObject.Find("SortByPriceButton").GetComponent<Button>();
        _inventoryButton.onClick.AddListener(delegate { _isInventoryVisible ^= true; RefreshInventory(); });
        _sortByIdButton.onClick.AddListener(delegate { _inventoryInstance.SortInventory(Inventory.SortOrder.Id); RefreshInventory(); });
        _sortByTypeButton.onClick.AddListener(delegate { _inventoryInstance.SortInventory(Inventory.SortOrder.Type); RefreshInventory(); });
        _sortByPriceButton.onClick.AddListener(delegate { _inventoryInstance.SortInventory(Inventory.SortOrder.Price); RefreshInventory(); });
        _generatorsLayer = GameObject.Find("Generating");
        _inventoryLayer = GameObject.Find("Inventory");
    }
    void Update()
    {
        _generatorsLayer.SetActive(!_isInventoryVisible);
        _inventoryLayer.SetActive(_isInventoryVisible);
        if (_isInventoryVisible)
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 12; x++)
                {
                    if (_index < _inventoryInstance.elements.Count)
                    {
                        GameObject gameObject = new GameObject("#" + _inventoryInstance.elements[_index].id.ToString());
                        RectTransform rect = gameObject.AddComponent<RectTransform>();
                        Image image = gameObject.AddComponent<Image>();
                        DisplayStats dispStat = gameObject.AddComponent<DisplayStats>();
                        _gameObjects.Add(gameObject);
                        rect.transform.SetParent(_inventoryLayer.transform);
                        rect.anchorMin = new Vector2(0, 1);
                        rect.anchorMax = new Vector2(0, 1);
                        rect.pivot = new Vector2(0, 1);
                        rect.anchoredPosition = new Vector2(x * 60, -y * 60);
                        rect.sizeDelta = new Vector2(50, 50);
                        rect.localScale = Vector3.one;
                        image.sprite = _inventoryInstance.elements[_index].elementDisplay;
                        _index++;
                    }
                }
            }
        }
    }
    void RefreshInventory()
    {
        _index = 0;
        foreach (var gameObject in _gameObjects)
            Destroy(gameObject);
    }
}