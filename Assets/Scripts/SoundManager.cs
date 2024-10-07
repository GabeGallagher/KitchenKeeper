using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public AudioClipRefSO audioClipRefSO;

    private DeliveryCounterController deliveryCounter;

    private float volume = 1f;

    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";

    public float Volume { get => volume; }

    private void Awake()
    {
        Instance = this;

        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1f);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;

        DeliveryManager.Instance.OnRecipeFail += DeliveryManager_OnRecipeFail;

        deliveryCounter = DeliveryCounterController.Instance;

        CuttingCounterController.OnAnyCut += CuttingCounterController_OnAnyCut;

        PlayerController.Instance.OnPickedUpSomething += Player_OnPickedUpSomething;

        CounterController.OnAnyObjectPlacedHere += CounterController_OnAnyObjectPlacedHere;

        TrashCounterController.OnAnyObjectTrashed += TrashCounterController_OnAnyObjectTrashed;
    }

    private void TrashCounterController_OnAnyObjectTrashed(object sender, System.EventArgs e)
    {
        TrashCounterController trash = (TrashCounterController)sender;

        PlaySound(audioClipRefSO.trash, trash.transform.position);
    }

    private void CounterController_OnAnyObjectPlacedHere(object sender, System.EventArgs e)
    {
        CounterController counter = (CounterController)sender;

        PlaySound(audioClipRefSO.objectDrop, counter.transform.position);
    }

    private void Player_OnPickedUpSomething(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefSO.objectPickup, PlayerController.Instance.transform.position);
    }

    private void CuttingCounterController_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounterController cuttingCounter = (CuttingCounterController)sender;
        PlaySound(audioClipRefSO.chop, cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeFail(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefSO.deliveryFail, deliveryCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeCompleted(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefSO.deliverySuccess, deliveryCounter.transform.position);
    }

    public void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volumeMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClipArray[Random.Range(0, audioClipArray.Length - 1)], position, volume * volumeMultiplier);
    }

    public void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume * volumeMultiplier);
    }

    public void ChangeVolume()
    {
        volume += 0.1f;

        if(volume > 1f)
        {
            volume = 0f;
        }
        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, volume);

        PlayerPrefs.Save();
    }
}
