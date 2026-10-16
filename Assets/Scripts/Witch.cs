using UnityEngine;

public class Witch : PlayableCharacter
{
   public override void SpecialAbility()
    {
        Debug.Log("Witch uses magic!");
    }

    public override void ApplyDamage(IDamageable damageable)
    {
        if (damageable is Vines)
       {
             damageable.TakeDamage(1);
            animator.SetBool("IsAttacking", true);
       }
    }
}
