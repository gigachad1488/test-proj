using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GarageDoors : MonoBehaviour
{
    [SerializeField]
    private float openingTime = 3f;

    [SerializeField]
    private Transform leftDoorHandle;

    [SerializeField] 
    private Transform rightDoorHandle;

    private bool isOpened = false;

    private WaitForEndOfFrame waitFrame = new WaitForEndOfFrame();

    private void OnTriggerEnter(Collider other)
    {
        if (!isOpened)
        {
            isOpened = true;

            StartCoroutine(DoorOpeningAnimation());
        }
    }

    private IEnumerator DoorOpeningAnimation()
    {
        float timer = 0;

        while (timer <= openingTime)
        {
            float yRotation = Mathf.Lerp(0, 140, timer / openingTime);

            Vector3 rotation = new Vector3(0, yRotation, 0);

            rightDoorHandle.transform.localEulerAngles = rotation;
            leftDoorHandle.transform.localEulerAngles = -rotation;

            timer += Time.deltaTime;

            yield return waitFrame;
        }

        this.enabled = false;
    }
}
