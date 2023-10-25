using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioClipsSO", menuName = "ScriptableObjects/AudioClipsSO", order = 2)]
public class AudioClipsSO : ScriptableObject
{

    public AudioClip ambientSound;
    public AudioClip hitSound;

}
