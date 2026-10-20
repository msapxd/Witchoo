using UnityEngine;

public class Witch : PlayableCharacter
{
   public override void SpecialAbility()
    {
        
    }

    public override void ApplyDamage(IDamageable damageable)
    {
        if (damageable is Vines)
       {
            damageable.TakeDamage(1);
            animator?.SetTrigger("IsAttacking");
            Debug.Log("Witch uses magic!");
        }
    }
}
