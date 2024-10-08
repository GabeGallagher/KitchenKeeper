using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AudioClipRefSO : ScriptableObject
{
    public AudioClip[] chop;
    public AudioClip[] deliveryFail;
    public AudioClip[] deliverySuccess;
    public AudioClip[] footsteps;
    public AudioClip[] objectPickup;
    public AudioClip[] objectDrop;
    public AudioClip panSizzle;
    public AudioClip[] trash;
    public AudioClip[] warning;
}
