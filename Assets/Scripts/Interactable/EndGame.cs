using UnityEngine;
using UnityEngine.InputSystem;

public class EndGame : MonoBehaviour, IInteractable
{
    public string InteractableName => InteractionMessage;

    [SerializeField] private string InteractionMessage = "Press [E] to end the game";
    public GameObject endGameUI;

    public void Interact()
    {
        EndGameSequence();
        InteractionMessage = "";
    }

    private void EndGameSequence()
    {
        GameManager.Instance.player.GetComponent<PlayerInput>().enabled = false;
        GameManager.Instance.player.GetComponent<MovementStateManager>().enabled = false;
        GameManager.Instance.player.GetComponent<WeaponClassManager>().weapon[GameManager.Instance.player.GetComponent<WeaponClassManager>().currentWeaponIndex].enabled = false;
        GameManager.Instance.player.GetComponent<WeaponClassManager>().enabled = false;
        GameManager.Instance.player.GetComponent<ActionStateManager>().enabled = false;
        
        GameManager.Instance.EnableUIMode();
        
        endGameUI.SetActive(true);
    }
}
