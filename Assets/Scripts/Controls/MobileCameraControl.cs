using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;

public class MobileCameraControl : OnScreenControl
{
    [InputControl(layout = "Vector2")]
    [SerializeField]
    private string m_controlPath;

    private float halfScreenWidth = Screen.width * 0.5f;

    private Vector2 dragVector = Vector2.zero;

    private int rightFingerId;
    private Vector2 lookInput = Vector2.zero;

    protected override string controlPathInternal
    {
        get => m_controlPath;
        set => m_controlPath = value;
    }

    private void Update()
    {
        GetTouchInput();
    }

    private void GetTouchInput()
    {
        if (Input.touchCount <= 0)
        {
            lookInput = Vector2.zero;
            rightFingerId = -1;

            SendValueToControl<Vector2>(lookInput);

            return;
        }

        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch t = Input.GetTouch(i);

            switch (t.phase)
            {
                case TouchPhase.Began:
                    if (t.position.x > halfScreenWidth && rightFingerId == -1)
                    {
                        rightFingerId = t.fingerId;
                    }
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    if (t.fingerId == rightFingerId)
                    {
                        rightFingerId = -1;

                        lookInput = Vector2.zero;
                    }
                    break;
                case TouchPhase.Moved:
                    if (t.fingerId == rightFingerId)
                    {
                        lookInput = t.deltaPosition;
                    }
                    break;
                case TouchPhase.Stationary:
                    if (t.fingerId == rightFingerId)
                    {
                        lookInput = Vector2.zero;
                    }
                    break;
            }
        }

        SendValueToControl<Vector2>(lookInput);
    }
}
