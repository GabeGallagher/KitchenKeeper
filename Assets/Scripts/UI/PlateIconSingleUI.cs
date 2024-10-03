using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateIconSingleUI : MonoBehaviour
{
    [SerializeField] private Image img;

    public void SetKitchenObjectSO(KitchenObjectSO obj)
    {
        img.sprite = obj.sprite;
    }
}
