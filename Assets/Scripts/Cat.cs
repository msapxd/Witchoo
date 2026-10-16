using UnityEngine;

public class Cat : PlayableCharacter
{
    public override void SpecialAbility()
    {
        
    }

    public override void ApplyDamage(IDamageable damageable)
    {
        if (damageable is Pumpkin)
       {
            damageable.TakeDamage(1);
            animator?.SetBool("IsAttacking", true);
            Debug.Log("Cat lights up the pumpkin!");
       }
    }
}