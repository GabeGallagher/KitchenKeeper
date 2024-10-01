using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private CounterController clearCounter;
    [SerializeField] private GameObject visualGameObject;

    void Start()
    {
        PlayerController.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged; 
    }

    private void Player_OnSelectedCounterChanged(object sender, PlayerController.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == clearCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Show()
    {
        visualGameObject.SetActive(true);
    }

    private void Hide()
    {
        visualGameObject.SetActive(false);
    }
}
