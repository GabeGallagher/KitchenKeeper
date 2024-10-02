using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : CounterController
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipes;
    public override void Interact(PlayerController player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (HasRecipeWithInput(player.GetKitchenObject().KitchenObjectSO))
                {
                    player.GetKitchenObject().KitchenObjectParent = this;
                }
            }
        }
        else
        {
            if (!player.HasKitchenObject())
            {
                kitchenObject.KitchenObjectParent = player;
            }
        }
    }

    public override void InteractAlternate(PlayerController player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(kitchenObject.KitchenObjectSO))
        {
            KitchenObjectSO outputKitchenObjectSO = GetOutputFromInput(GetKitchenObject().KitchenObjectSO);

            kitchenObject.DestroySelf();

            KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
        }
    }

    private KitchenObjectSO GetOutputFromInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipes)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return cuttingRecipeSO.output;
            }
        }
        return null;
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipes)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return true;
            }
        }
        return false;
    }
}
