using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounterController : CounterController
{
    public static event EventHandler OnAnyObjectTrashed;

    public override void Interact(PlayerController player)
    {
        if (player.HasKitchenObject())
        {
            player.GetKitchenObject().DestroySelf();

            OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);
        }
    }

    new public static void ResetStaticData()
    {
        OnAnyObjectTrashed = null;
    }
}
