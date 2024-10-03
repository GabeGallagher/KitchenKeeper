using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private GameObject stoveOnGameObject, sizzlingParticles;

    private void Start()
    {
        GetComponentInParent<StoveCounterController>().OnStateChanged += StoveCounterVisual_OnStateChanged;
    }

    private void StoveCounterVisual_OnStateChanged(object sender, StoveCounterController.OnStateChangeEventArgs e)
    {
        bool showVisual = e.state == StoveCounterController.State.Frying || e.state == StoveCounterController.State.Fried;
        stoveOnGameObject.SetActive(showVisual);
        sizzlingParticles.SetActive(showVisual);
    }
}
