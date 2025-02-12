using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
        [SerializeField, HideInInspector]
        private PlayerInput playerInput;

        public Vector2 Move { get; private set; }
        public Vector2 Look { get; private set; }

        private InputActionMap currentMap;
        private InputAction moveAction;
        private InputAction lookAction;

        private void OnEnable()
        {
            currentMap.Enable();
        }

        private void OnValidate()
        {
            playerInput = GetComponent<PlayerInput>();
        }

        private void Awake()
        {
            HideCursor();

            currentMap = playerInput.currentActionMap;
            moveAction = currentMap.FindAction("Move");
            lookAction = currentMap.FindAction("Look");

            moveAction.performed += MoveAction;
            lookAction.performed += LookAction;

            moveAction.canceled += MoveAction;
            lookAction.canceled += LookAction;
        }

        private void LookAction(InputAction.CallbackContext context)
        {
            Look = context.ReadValue<Vector2>();
        }

        private void MoveAction(InputAction.CallbackContext context)
        {
            Move = context.ReadValue<Vector2>();
        }

        public static void HideCursor()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnDisable()
        {
            currentMap.Disable();
        }
    }

