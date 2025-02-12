using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Collider))]
public class TruckPickUp : MonoBehaviour
{
    [Inject]
    private Inventory inventory;

    [SerializeField]
    private int capacity = 5;

    [SerializeField]
    private float pickCd = 1f;

    [SerializeField]
    private BoxCollider itemsBounds;

    private bool canPick = true;

    private List<CollectableItem> items = new List<CollectableItem>();

    [SerializeField, HideInInspector]
    private Vector3[,] itemsGrid = new Vector3[3, 3];

    private void Start()
    {
        itemsGrid = new Vector3[3, 3];
        float stepX = itemsBounds.size.x / 2f;
        float stepZ = itemsBounds.size.z / 2f;

        for (int row = 0; row < 3; row++)
        {
            float z = -itemsBounds.size.z / 2 + stepZ * row;
            for (int col = 0; col < 3; col++)
            {
                float x = -itemsBounds.size.x / 2 + stepX * col;
                Vector3 localPoint = new Vector3(x, 0, z);
                itemsGrid[row, col] = itemsBounds.transform.TransformPoint(localPoint);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!canPick)
            return;

        if (items.Count > capacity - 1)
            return;

        if (!inventory.HasItems)
            return;

        if (!other.gameObject.CompareTag("Player"))
            return;

        canPick = false;
        CollectableItem item = inventory.GetLastItem();
        items.Add(item);

        int row = items.Count % 3;

        if (row == 0)
            row = 3;

        row--;

        int collumn = (items.Count - 1) / 3;

        item.transform.SetParent(transform, false);
        item.transform.position = itemsGrid[row, collumn];
        item.gameObject.SetActive(true);

        Invoke(nameof(PickCdReset), pickCd);
    }

    private void PickCdReset()
    {
        canPick = true;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;

    //    for (int i = 0; i < itemsGrid.GetLength(0); i++)
    //    {
    //        for (int j = 0; j < itemsGrid.GetLength(1); j++)
    //        {
    //            Gizmos.DrawCube(itemsGrid[i, j], Vector3.one);
    //        }
    //    }    
    //}
}
