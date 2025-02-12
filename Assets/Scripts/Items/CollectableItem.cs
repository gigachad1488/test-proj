using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    public void Collect(Inventory inventory)
    {
        inventory.AddItem(this);
        gameObject.SetActive(false);
    }
}
