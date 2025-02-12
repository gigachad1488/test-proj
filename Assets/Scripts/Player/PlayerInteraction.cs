using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    private float interactionRadius = 3f;

    [SerializeField]
    private LayerMask itemsLayerMask;

    [Inject]
    private Inventory inventory;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, interactionRadius, itemsLayerMask))
                {
                    if (hit.transform.TryGetComponent<CollectableItem>(out CollectableItem item))
                    {
                        item.Collect(inventory);
                    }
                }
            }
        }
    }
}
