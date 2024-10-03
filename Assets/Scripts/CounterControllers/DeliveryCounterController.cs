using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounterController : CounterController
{
    [SerializeField] private GameObject deliveryArrow;

    private void Start()
    {
        deliveryArrow.SetActive(true);
    }

    public override void Interact(PlayerController player)
    {
        if (player.HasKitchenObject() && player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
        {
            DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);

            player.GetKitchenObject().DestroySelf();
        }
    }
}
