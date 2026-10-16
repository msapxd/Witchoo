using Unity.Multiplayer.PlayMode;
using UnityEngine;

public class Vines : TrapAbstract
{
    public override void ApplyDamage(IDamageable damageable)
    {
        if (damageable is not Witch)
        {
            damageable.TakeDamage(1);
            animator.SetBool("IsAttacking", true);
        }
    }
}
