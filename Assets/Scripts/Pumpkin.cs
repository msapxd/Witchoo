using UnityEngine;

public class Pumpkin : TrapAbstract
{
    public override void ApplyDamage(IDamageable damageable)
    {
        if (damageable is not Cat)
        {
            damageable.TakeDamage(1);
            animator?.SetBool("IsAttacking", true);
        }
    }
}
