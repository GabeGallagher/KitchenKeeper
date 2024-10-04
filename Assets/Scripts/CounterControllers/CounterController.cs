using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterController : MonoBehaviour, IKitchenObjectParent
{
    public static event EventHandler OnAnyObjectPlacedHere;

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

        if (kitchenObject != null)
        {
            OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty); 
        }
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
        else if (player.HasKitchenObject() && kitchenObject != null)
        {
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                if (plateKitchenObject.TryAddIngredient(kitchenObject.KitchenObjectSO))
                {
                    kitchenObject.DestroySelf();
                }
            }
            else
            {
                if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().KitchenObjectSO))
                    {
                        player.GetKitchenObject().DestroySelf();
                    }
                }
            }
        }
    }

    public virtual void InteractAlternate(PlayerController player)
    {
        //Debug.LogError("CounterController.InteractAlternate called...");
    }
}
