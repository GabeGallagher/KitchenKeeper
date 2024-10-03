using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounterController : CounterController
{
    public override void Interact(PlayerController player)
    {
        if (player.HasKitchenObject())
        {
            player.GetKitchenObject().DestroySelf();
        }
    }
}
