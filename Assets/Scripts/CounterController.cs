using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterController : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private Transform counterTopPoint;

    protected KitchenObject kitchenObject;

    public Transform GetCounterTopPoint()
    {
        return counterTopPoint;
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

    public virtual void Interact(PlayerController player)
    {
        if(!HasKitchenObject() && player.HasKitchenObject())
        {
            player.GetKitchenObject().KitchenObjectParent = this;
        }
        else if (!player.HasKitchenObject() && kitchenObject != null)
        {
            kitchenObject.KitchenObjectParent = player;
        }
    }

    public virtual void InteractAlternate(PlayerController player)
    {
        Debug.LogError("CounterController.InteractAlternate called...");
    }
}
