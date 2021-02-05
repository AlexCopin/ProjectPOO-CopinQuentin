using System.Collections.Generic;
using UnityEngine;
class Inventory
{
    public enum SortOrder
    {
        Id,
        Type,
        Price,
    }
    static Inventory _instance;
    public List<Element> elements { get; private set; } = new List<Element>();
    public static Inventory GetInstance()
    {
        if (_instance == null) _instance = new Inventory();
        return _instance;
    }
    public void SortInventory(SortOrder sortOrder)
    {
        switch (sortOrder)
        {
            case SortOrder.Id:
                elements.Sort((a, b) => a.id.CompareTo(b.id));
                break;
            case SortOrder.Type:
                elements.Sort((a, b) => ((int) a.type).CompareTo((int) b.type));
                elements.Sort((a, b) =>
                {
                    var res = ((int) a.type).CompareTo((int) b.type);
                    if (res == 0) res = a.id.CompareTo(b.id);
                    return res;
                });
                break;
            case SortOrder.Price:
                elements.Sort((a, b) => a.price.CompareTo(b.price));
                break;
        }
    }
}