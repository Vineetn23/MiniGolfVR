using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource ambientSound;
    [SerializeField] private AudioSource gameSounds;

    [SerializeField] private AudioClipsSO audioClipsSO;

    private void OnEnable()
    {
        GolfClub.BallStruck += GolfClub_PlayHitSound;
        GameManager.PlayAmbientSound += GameManager_PlayAmbientSound;
    }
    private void OnDisable()
    {
        GolfClub.BallStruck -= GolfClub_PlayHitSound;
        GameManager.PlayAmbientSound -= GameManager_PlayAmbientSound;
    }

    private void GameManager_PlayAmbientSound()
    {
        ambientSound.clip = audioClipsSO.ambientSound;
        ambientSound.Play();
    }

    private void GolfClub_PlayHitSound()
    {
        gameSounds.clip = audioClipsSO.hitSound;
        gameSounds.Play();
    }
}
