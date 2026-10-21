using UnityEngine;

public class Cat : PlayableCharacter
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
        if (damageable is Pumpkin)
       {
            damageable.TakeDamage(1);
            animator?.SetTrigger("IsAttacking");
            audioManager.PlaySfx(audioManager.catMeow);
            Debug.Log("Cat lights up the pumpkin!");
       }
    }
}