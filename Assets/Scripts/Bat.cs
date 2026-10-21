using UnityEngine;

public class Bat : PlayableCharacter
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
        if (damageable is StringLights)
        {
            damageable.TakeDamage(1);
            animator?.SetTrigger("IsAttacking");
            audioManager.PlaySfx(audioManager.batShreik);
            Debug.Log("Bat hangs lights!");
        }
    }
}
