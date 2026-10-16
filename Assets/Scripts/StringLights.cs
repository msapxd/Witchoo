using UnityEngine;

public class StringLights : TrapAbstract
{
    public override void ApplyDamage(IDamageable damageable)
    {
        if (damageable is not Bat)
        {
            damageable.TakeDamage(1);
            animator.SetBool("IsAttacking", true);
        }
    }
}
