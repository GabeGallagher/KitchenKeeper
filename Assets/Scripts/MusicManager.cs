using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance {  get; private set; }
    private AudioSource audioSource;
    private float volume = .3f;

    public float Volume { get => volume; }

    private void Awake()
    {
        Instance = this;

        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeVolume()
    {
        volume += 0.1f;

        if (volume > 1f)
        {
            volume = 0f;
        }
        audioSource.volume = volume;
    }
}
