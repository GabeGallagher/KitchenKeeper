using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IKitchenObjectParent
{
    public static PlayerController Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public CounterController selectedCounter;
    }

    private InputManager inputManager;
    private Vector3 lastInteractDir;
    private CounterController selectedCounter;
    private bool isWalking;
    private KitchenObject kitchenObject;

    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float moveSpeed, rotateSpeed, playerRadius, playerHeight, interactDistance;
    [SerializeField] private Transform kitchenObjectHoldPoint;

    public bool IsWalking { get => isWalking; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one instance of the Player Controller");
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        inputManager = GameObject.Find("GameInput").GetComponent<InputManager>();
        if (inputManager == null )
        {
            Debug.LogError("Input Manager is null");
        }
        inputManager.OnInteractAction += InputManager_OnInteractAction;
        inputManager.OnInteractAlternateAction += InputManager_OnInteractAlternateAction;
    }

    private void InputManager_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void InputManager_OnInteractAction(object sender, System.EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
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
                if (counter != selectedCounter)
                {
                    SetSelectedCounter(counter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
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
            canMove = moveDir.x !=0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;
                canMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
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

    private void SetSelectedCounter(CounterController selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }

    public Transform GetCounterTopPoint()
    {
        return kitchenObjectHoldPoint;
    }
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null ? true : false;
    }
}
