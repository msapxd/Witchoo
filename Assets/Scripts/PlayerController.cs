using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;
using UnityEngine.VFX;

public class PlayerController : MonoBehaviour
{
    public HealthView healthView;
    public PlayableCharacter[] characters;
    public PlayerMovement playerMovement;
    //tweak in edditor until the character can't be seen behind the poof
    public float swapDelay = 0.15f;
    // Which character is currently active?
    private int currentCharacter = 0;

    private void Start()
    {
        //add random character
        SwitchToCharacter(currentCharacter);
    }

    public void SwitchCharacter(InputAction.CallbackContext context)
    {
        if (DialogueManager.isActive) return;

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

    private void SwitchToCharacter(int characterIndex, bool playEffect = true)
    {
        if (playEffect)
        {
            VFXManager.Instance.PlayVFX("Poof", transform.position, 0.5f, transform);
            StartCoroutine(DelayedSwap(characterIndex));
        }
        else
        {
            ApplyCharacterSwap(characterIndex);
        }
    }
    private IEnumerator DelayedSwap(int characterIndex)
    {
        foreach (var character in characters)
        {
            character.gameObject.SetActive(false);
        }
        //Wait for 2 frames 
        yield return new WaitForSeconds(swapDelay);

        ApplyCharacterSwap(characterIndex);
    }
    private void ApplyCharacterSwap(int characterIndex)
    {
            for (int i = 0; i < characters.Length; i++)
        {
            characters[i].gameObject.SetActive(i == characterIndex);
            if (i == characterIndex)
            {
                playerMovement.SetAnimator(characters[i].GetAnimator());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HealthPickup"))
        {
            if (PlayerData.currentHealth < PlayerData.maxHealth)
            {
                PlayerData.currentHealth++;
                VFXManager.Instance.PlayVFX("Heal", transform.position, 1.0f, transform);
                Destroy(other.gameObject);
            }
        }

        TrapAbstract trapAbstract = other.GetComponent<TrapAbstract>();
        if (trapAbstract != null)
        {
            characters[currentCharacter].ApplyDamage(trapAbstract);
            trapAbstract.ApplyDamage(characters[currentCharacter]);
        }

        healthView.UpdateHealth();
    }
}
