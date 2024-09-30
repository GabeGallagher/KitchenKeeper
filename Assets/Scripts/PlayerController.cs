using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private InputManager inputManager;

    private Vector3 lastInteractDir;

    [SerializeField] private LayerMask layerMask;

    [SerializeField] private float moveSpeed, rotateSpeed, playerRadius, playerHeight, interactDistance;

    private bool isWalking;

    public bool IsWalking { get => isWalking; }

    void Start()
    {
        inputManager = GameObject.Find("GameInput").GetComponent<InputManager>();
        if (inputManager == null )
        {
            Debug.LogError("Input Manager is null");
        }
    }

    private void Update()
    {
        HandleMovement();

        HandleInteractions();
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = inputManager.GetNormalizedMovementVector();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHitInfo, interactDistance, layerMask))
        {
            if (raycastHitInfo.transform.TryGetComponent(out CounterController counter))
            {
                counter.Interact();
            }
        }
        else
        {
            Debug.Log("-");
        }
    }

    private void HandleMovement()
    {
        Vector2 inputVector = inputManager.GetNormalizedMovementVector();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
                if (canMove)
                {
                    moveDir = moveDirZ;
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }
        isWalking = moveDir != Vector3.zero;

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }
}
