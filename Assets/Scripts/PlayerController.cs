
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class PlayerController : MonoBehaviour
{
    public HealthView healthView;
    public PlayableCharacter[] characters;
    public PlayerMovement playerMovement;
    

    // Which character is currently active?
    private int currentCharacter = 0;

    private void Start()
    {
        //add random character
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
            playerMovement.SetAnimator(characters[i].GetAnimator());
            characters[i].gameObject.SetActive(i == characterIndex);
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
