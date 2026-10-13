
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public HealthView healthView;
    public GameObject[] characters;

    // Which character is currently active?
    private int currentCharacter = 0;

    private void Start()
    {
        SwitchToCharacter(currentCharacter);
    }

    public void SwitchCharacter(InputAction.CallbackContext context)
{
    if (!context.performed)
    {
        return;
    }

    currentCharacter++;

    if (currentCharacter >= characters.Length)
    {
        currentCharacter = 0;
    }

    SwitchToCharacter(currentCharacter);
}
    private void SwitchToCharacter(int characterIndex)
    {
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].SetActive(i == characterIndex);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HealthPickup"))
        {
            if (PlayerData.currentHealth < PlayerData.maxHealth)
            {
                PlayerData.currentHealth++;
                healthView.UpdateHealth();
                Destroy(other.gameObject);
            }
        }
    }
}
