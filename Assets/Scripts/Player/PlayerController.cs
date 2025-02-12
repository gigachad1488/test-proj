using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

[RequireComponent(typeof(Rigidbody), typeof(InputManager))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] 
    private float moveSpeed = 5f;

    [SerializeField] 
    private LayerMask groundLayerMask = ~0;

    [Space(5)]
    [Header("Camera")]
    [SerializeField] 
    private float cameraSensitivity = 1f;

    [SerializeField] 
    private Transform cameraTransform;

    [SerializeField]
    private Transform cameraRoot;

    [SerializeField, HideInInspector]
    private Rigidbody rb;

    [SerializeField]
    private float topLimit = 90f;

    [SerializeField]
    private float botLimit = -90f;

    private float xRotation;
    private bool isGrounded;

    [SerializeField, HideInInspector]
    private InputManager inputManager;

    private void OnValidate()
    {
        rb = GetComponent<Rigidbody>();
        inputManager = GetComponent<InputManager>();
    }

    void FixedUpdate()
    {
        Movement();
    }

    void LateUpdate()
    {
        CameraRotation();
    }

    private void Movement()
    {
        Vector3 moveDirection = transform.TransformVector(new Vector3(inputManager.Move.x, 0, inputManager.Move.y));
        Vector3 force = moveDirection * moveSpeed;
        rb.AddForce(force, ForceMode.VelocityChange);
    }

    private void CameraRotation()
    {
        cameraTransform.position = cameraRoot.position;

        float mouseX = inputManager.Look.x * cameraSensitivity;
        float mouseY = inputManager.Look.y * cameraSensitivity;

        Vector3 rot = cameraTransform.transform.localRotation.eulerAngles;
        float desiredX = cameraTransform.localEulerAngles.y + mouseX * Time.smoothDeltaTime;

        xRotation -= mouseY * Time.smoothDeltaTime;
        xRotation = Mathf.Clamp(xRotation, topLimit, botLimit);

        cameraTransform.transform.localRotation = Quaternion.Euler(xRotation, desiredX, 0);
        rb.MoveRotation(Quaternion.Euler(0, desiredX, 0));
    }
}
