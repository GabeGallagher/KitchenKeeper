using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterController : CounterController
{
    public event EventHandler OnPlayerGrabObject;
    public override void Interact(PlayerController player)
    {
        if (!player.HasKitchenObject())
        {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);

            kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();

            kitchenObject.KitchenObjectParent = player;

            OnPlayerGrabObject?.Invoke(this, EventArgs.Empty); 
        }
    }
}
