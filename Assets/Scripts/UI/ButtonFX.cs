using UnityEngine;

public class ButtonFX : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip buttonHoverSound;
    [SerializeField] private AudioClip buttonClickSound;

    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }


    public void HoverSound()
    {
        if (audioSource == null || buttonHoverSound == null) return;

        audioSource.PlayOneShot(buttonHoverSound);
    }

    public void ClickSound()
    {
        if (audioSource == null || buttonClickSound == null) return;

        audioSource.PlayOneShot(buttonClickSound);
    }
}