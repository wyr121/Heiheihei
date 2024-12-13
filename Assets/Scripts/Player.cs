using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance { get; private set; }


    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter seletedCounter;
    }


    [SerializeField] private float moveSpeed;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterslayerMasks;
    [SerializeField] private Transform kitchenObjectHoldPoint;


    private Vector3 lastInteractDir;
    private BaseCounter seletedCounter;
    private KitchenObject kitchenObject;
    private bool isWalking;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Player Instance 只能存在一个 !");
        }
        Instance = this;
    }

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (seletedCounter != null)
        {
            seletedCounter.Interact(this);
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    public void HandleInteractions()
    {
        var inputVector = gameInput.GetMovementVectorNormalized();

        var moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        var interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractDir, out var raycastHit, interactDistance, counterslayerMasks))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (baseCounter != seletedCounter)
                {
                    SetSeletedCounter(baseCounter);
                }
            }
            else
            {
                SetSeletedCounter(null);
            }
        }
        else
        {
            SetSeletedCounter(null);
        }
    }

    public void HandleMovement()
    {
        var inputVector = gameInput.GetMovementVectorNormalized();

        var moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        var moveDistance = moveSpeed * Time.deltaTime;
        var playerRadius = .7f;
        var playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            var moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                if (!canMove)
                {
                    var moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                    canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
                    if (canMove)
                    {
                        moveDir = moveDirZ;
                    }
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }


        isWalking = moveDir != Vector3.zero;
        var rotationSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);
    }

    private void SetSeletedCounter(BaseCounter seletedCounter)
    {
        this.seletedCounter = seletedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            seletedCounter = seletedCounter
        });
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
