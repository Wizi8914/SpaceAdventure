using UnityEngine;

public class HealPost : MonoBehaviour, IInteractable
{
    public string InteractableName => InteractionMessage;

    [SerializeField] string InteractionMessage = "Press [E] to gain {healAmount} hp";

    public float healAmount = 20f; // Amount of health to restore
    public GameObject cross;
    public float spinSpeed = 50f;

    private bool isTaken = false;

    private void Start()
    {
        InteractionMessage = InteractionMessage.Replace("{healAmount}", healAmount.ToString());
    }

    private void Update()
    {
        Vector3 currentCrossRotation = cross.transform.rotation.eulerAngles;
        currentCrossRotation.y += spinSpeed * Time.deltaTime;
        cross.transform.rotation = Quaternion.Euler(currentCrossRotation);
    }

    public void Interact()
    {
        PlayerHealth playerHealth = GameManager.Instance.player.GetComponent<PlayerHealth>();

        if (playerHealth != null && !isTaken)
        {
            playerHealth.Heal(healAmount);
            isTaken = true;
            cross.SetActive(false);
            InteractionMessage = "";
        }
    }
}

