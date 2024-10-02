using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent kitchenObjectParent;

    public KitchenObjectSO KitchenObjectSO { get => kitchenObjectSO; }

    public IKitchenObjectParent KitchenObjectParent { get => kitchenObjectParent; set { SetKitchenObjectParent(value); } }

    private void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        if(this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.ClearKitchenObject();
        }
        this.kitchenObjectParent = kitchenObjectParent;

        if(kitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("Kitchen Object Parent already has kitchen object");
        }
        kitchenObjectParent.SetKitchenObject(this);

        transform.parent = kitchenObjectParent.GetCounterTopPoint();
        transform.localPosition = Vector3.zero;
    }
}
