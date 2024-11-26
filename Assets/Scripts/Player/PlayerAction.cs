using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAction : MonoBehaviour
{
    InputActions action;
    InputAction moveAction, fireAction;
    private float movementSpeed = 20f;
    [SerializeField]
    private GameObject shootingPoint;
    [SerializeField]
    private GameObject bullet;

    private void Awake()
    {
        action = new InputActions();
        moveAction = action.Player.Move;
        moveAction.Enable();

        fireAction = action.Player.Fire;
        fireAction.Enable();
        fireAction.performed += OnFirePerformed;
    }
    void Start()
    {

    }

    void Update()
    {
        Vector3 keyboardVector = moveAction.ReadValue<Vector3>();
        Move(keyboardVector.x, keyboardVector.z);
    }

    void Move(float x, float z)
    {
        Vector3 moveDirection = new Vector3(x, 0, z);
        transform.Translate(moveDirection * movementSpeed * Time.deltaTime, Space.Self);
    }

    void OnFirePerformed(InputAction.CallbackContext context)
    {
        if (bullet != null && shootingPoint != null)
        {
            Instantiate(bullet, shootingPoint.transform.position, shootingPoint.transform.rotation);
        }
    }
}