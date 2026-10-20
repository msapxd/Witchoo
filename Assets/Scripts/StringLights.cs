using UnityEngine;

public class StringLights : TrapAbstract
{
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    public override void ApplyDamage(IDamageable damageable)
    {
        if (damageable is not Bat)
        {
            damageable.TakeDamage(1);
            audioManager.PlaySfx(audioManager.hurt);
            animator?.SetBool("IsAttacking", true);
        }
    }
}
