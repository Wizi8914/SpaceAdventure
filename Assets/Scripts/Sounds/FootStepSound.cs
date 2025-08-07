using System.Collections.Generic;
using UnityEngine;

public class FootStepSound : MonoBehaviour
{
    AudioSource audioSource; // Reference to the AudioSource component
    public List<AudioClip> footstepSounds; // Array of footstep sound clips

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
    }

    // Play a random footstep sound
    public void PlayFootstepSound()
    {
        if (footstepSounds.Count > 0)
        {
            AudioClip clip = footstepSounds[Random.Range(0, footstepSounds.Count)];

            MovementStateManager actionStateManager = GetComponent<MovementStateManager>();
            if (actionStateManager)
            {
                if (actionStateManager.currentState == actionStateManager.Walk)
                {
                    audioSource.volume = Random.Range(0.5f, 0.8f);
                }
                else if (actionStateManager.currentState == actionStateManager.Run)
                {
                    audioSource.volume = Random.Range(0.7f, 1f);
                }
                else if (actionStateManager.currentState == actionStateManager.Crouch)
                {
                    audioSource.volume = Random.Range(0.3f, 0.6f);
                }
            }
            else
            {
                audioSource.volume = Random.Range(0.5f, 1f); // Default volume if no state manager is found
            }

            audioSource.pitch = Random.Range(0.8f, 1.2f);

            audioSource.PlayOneShot(clip);
        }
    }
}
