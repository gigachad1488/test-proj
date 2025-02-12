using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<CollectableItem> items = new List<CollectableItem>();

    public bool HasItems {  get { return items.Count > 0; } }

    public void AddItem(CollectableItem item)
    {
        items.Add(item);
    }

    public CollectableItem GetLastItem()
    {
        CollectableItem item = items[items.Count - 1];
        items.RemoveAt(items.Count - 1);
        return item;
    }

    public List<CollectableItem> GetAllItems()
    {
        List<CollectableItem> allItems = items;
        items.Clear();
        return allItems;
    }
}
