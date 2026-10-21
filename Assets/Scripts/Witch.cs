using UnityEngine;

public class Witch : PlayableCharacter
{
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    public override void SpecialAbility()
    {
        
    }

    public override void ApplyDamage(IDamageable damageable)
    {
        if (damageable is Vines)
       {
            damageable.TakeDamage(1);
            animator?.SetTrigger("IsAttacking");
            audioManager.PlaySfx(audioManager.witchWand);
            Debug.Log("Witch uses magic!");
        }
    }
}
