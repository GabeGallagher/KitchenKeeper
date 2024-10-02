using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterController : CounterController
{
    [SerializeField] protected KitchenObjectSO kitchenObjectSO;

    public event EventHandler OnPlayerGrabObject;

    public override void Interact(PlayerController player)
    {
        if (!player.HasKitchenObject())
        {
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);

            OnPlayerGrabObject?.Invoke(this, EventArgs.Empty); 
        }
    }
}
