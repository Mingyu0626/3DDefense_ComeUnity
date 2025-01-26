using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCameraWalk : MonoBehaviour
{
    [SerializeField] private int movementDirection; // 0(앞), 1(뒤), 2(왼쪽), 3(오른쪽)
    private Vector3 moveVector = new Vector3(0, 0, 0);
    private Vector3 OriginalPosition;
    private float movementSpeed = 0.5f; 
    private void Awake()
    {
        OriginalPosition = transform.position;
        InitmoveVector();
    }
    private void LateUpdate()
    {
        transform.Translate(moveVector * movementSpeed * Time.deltaTime, Space.Self);
    }
    private void OnDisable()
    {
        transform.position = OriginalPosition;
    }
    private void InitmoveVector()
    {
        if (movementDirection == 0) moveVector.z = 1;
        else if (movementDirection == 1) moveVector.z = -1;
        else if (movementDirection == 2) moveVector.x = -1;
        else if (movementDirection == 3) moveVector.x = 1;
        else moveVector.x = 1;
    }
}
