using UnityEngine;

public class Bat : PlayableCharacter
{
    public override void SpecialAbility()
    {
        
    }

    public override void ApplyDamage(IDamageable damageable)
    {
        if (damageable is StringLights)
        {
            damageable.TakeDamage(1);
            animator?.SetBool("IsAttacking", true);
            Debug.Log("Bat hangs lights!");
        }
    }
}
