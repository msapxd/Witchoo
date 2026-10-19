using System;
using UnityEngine;

public abstract class TrapAbstract : MonoBehaviour, IDamageable
{
    public Animator animator;
    public bool isDead = false;
    private int currentHealth = 1;
    public Action OnDeath;

    public virtual void ApplyDamage(IDamageable damageable)
    {
        damageable.TakeDamage(1);
    }

    public virtual void TakeDamage(int howMuch)
    {
        currentHealth = Mathf.Max(0, currentHealth - howMuch);

        if (currentHealth == 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        isDead = true;
        OnDeath?.Invoke();
        AnimateDeath();
        //Destroy(gameObject);
    }

    public void AnimateDeath()
    {
        animator?.SetBool("IsDead", true);
        //in animation, we need to hide the object
    }
}
