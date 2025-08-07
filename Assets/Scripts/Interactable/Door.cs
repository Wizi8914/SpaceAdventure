using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public string InteractableName => InteractionMessage;

    [SerializeField] string InteractionMessage = "Press [E] to open the door";

    private bool isOpen = false;
    private Animator animator;
    public float openAnimationTime = 1f;
    private bool isAnimating = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Open()
    {
        isOpen = true;
        animator.SetBool("character_nearby", true);
        InteractionMessage = InteractionMessage.Replace("open", "close");
    }

    public void Close()
    {
        isOpen = false;
        animator.SetBool("character_nearby", false);
        InteractionMessage = InteractionMessage.Replace("close", "open");
    }

    public void Interact()
    {
        if (isAnimating) return; // Bloque l'interaction pendant l'animation

        if (isOpen)
        {
            StartCoroutine(AnimateDoor(openAnimationTime));
            Close();
        }
        else
        {
            StartCoroutine(AnimateDoor(openAnimationTime));
            Open();
        }
    }

    private IEnumerator AnimateDoor(float duration)
    {
        isAnimating = true;
        string previousMessage = InteractionMessage;
        InteractionMessage = ""; // Vide le message pendant l'animation
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }

        isAnimating = false;
        // Remet le message approprié selon l'état de la porte
        InteractionMessage = isOpen ? "Press [E] to close the door" : "Press [E] to open the door";
    }

}
