using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// file: SoundManager.cs
/// description: Manages audio across all scenes
/// author: Nathan Ballay
/// </summary>
public class SoundManager : MonoBehaviour
{
    // Audio Source
    private AudioSource audioSource;

    // Audio Clips
    [SerializeField] AudioClip gameOverSound;
    [SerializeField] AudioClip gameVictorySound;
    [SerializeField] AudioClip grabSound;
    [SerializeField] AudioClip minigameWinSound;
    [SerializeField] AudioClip minigameLoseSound;
    [SerializeField] AudioClip sparkleSound;
    [SerializeField] AudioClip wrongChoiceSound;
    [SerializeField] AudioClip boilingSound;
    [SerializeField] AudioClip slashSound;
    [SerializeField] AudioClip splatSound;
    [SerializeField] AudioClip treeDecorationSound;
    [SerializeField] AudioClip uhOhSound;
    [SerializeField] AudioClip vacuumSound;
    [SerializeField] AudioClip vacuumSuckSound;
    [SerializeField] AudioClip dingDongSound;
    [SerializeField] AudioClip buttonHoverSound;
    [SerializeField] AudioClip mainMenuSound;
    [SerializeField] AudioClip mainGameplaySound;

    // Variables
    private int numSoundManagers;

    // Start is called before the first frame update
    void Start()
    {
        numSoundManagers = FindObjectsOfType<SoundManager>().Length;
        if (numSoundManagers != 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Stop all current audio being played
    /// </summary>
    public void StopAllAudio()
    {
        audioSource.Stop();
    }

    /// <summary>
    /// Play the Game Victory sound
    /// </summary>
    public void PlayGameVictorySound()
    {
        audioSource.PlayOneShot(gameVictorySound);
    }

    /// <summary>
    /// Play the Game Over sound
    /// </summary>
    public void PlayGameOverSound()
    {
        audioSource.PlayOneShot(gameOverSound);
    }

    /// <summary>
    /// Play the Grab sound
    /// </summary>
    public void PlayGrabSound()
    {
        audioSource.PlayOneShot(grabSound);
    }

    /// <summary>
    /// Play the Minigame Win sound
    /// </summary>
    public void PlayMinigameWinSound()
    {
        audioSource.PlayOneShot(minigameWinSound);
    }

    /// <summary>
    /// Play the Minigame Lose sound
    /// </summary>
    public void PlayMinigameLoseSound()
    {
        audioSource.PlayOneShot(minigameLoseSound);
    }

    /// <summary>
    /// Play the Sparkle sound
    /// </summary>
    public void PlaySparkleSound()
    {
        audioSource.PlayOneShot(sparkleSound, 0.6f);
    }

    /// <summary>
    /// Play the Wrong Choice sound
    /// </summary>
    public void PlayWrongChoiceSound()
    {
        audioSource.PlayOneShot(wrongChoiceSound);
    }

    public void PlayBoilingSound()
    {
        audioSource.PlayOneShot(boilingSound);
    }

    public void PlaySlashSound()
    {
        audioSource.PlayOneShot(slashSound);
    }

    public void PlaySplatSound()
    {
        audioSource.PlayOneShot(splatSound);
    }

    public void PlayTreeDecorationSound()
    {
        audioSource.PlayOneShot(treeDecorationSound);
    }

    public void PlayUhOhSound()
    {
        audioSource.PlayOneShot(uhOhSound);
    }

    public void PlayVacuumSound()
    {
        audioSource.PlayOneShot(vacuumSound);
    }

    public void PlayVacuumSuckSound()
    {
        audioSource.PlayOneShot(vacuumSuckSound);
    }

    public void PlayDingDongSound()
    {
        audioSource.PlayOneShot(dingDongSound);
    }

    public void PlayButtonHoverSound()
    {
        audioSource.PlayOneShot(buttonHoverSound);
    }

    public void PlayMainMenuSound()
    {
        audioSource.clip = mainMenuSound;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayMainGameplaySound()
    {
        audioSource.clip = mainGameplaySound;
        audioSource.loop = true;
        audioSource.Play();
    }
}
