using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterController : MonoBehaviour
{
    public void Interact()
    {
        Debug.Log("interacted with " + this.gameObject.name);
    }
}
