using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI interactionText;
    [SerializeField] IInteractable currentTargetInteractable;
    private PlayerInput playerInput;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    public void Update()
    {

        UpdateInteractionText();
        CheckForInteractionInput();
    }

    private void OnTriggerEnter(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();

        if (interactable != null)
        {
            currentTargetInteractable = interactable;
            UpdateInteractionText();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<IInteractable>() == currentTargetInteractable)
        {
            currentTargetInteractable = null;
            UpdateInteractionText();
        }
    }

    private void UpdateInteractionText()
    {
        if (currentTargetInteractable != null)
        {
            interactionText.text = currentTargetInteractable.InteractableName;
        }
        else
        {
            interactionText.text = "";
        }
    }

    private void CheckForInteractionInput()
    {
        if (playerInput.actions["Interact"].WasPressedThisFrame() && currentTargetInteractable != null)
        {
            currentTargetInteractable.Interact();
        }
    }
}
