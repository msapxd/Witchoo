using System;
using System.Collections; // Required for Coroutines
using UnityEngine;

public abstract class TrapAbstract : MonoBehaviour, IDamageable
{
    public Animator animator;
    public bool isDead = false;
    private int currentHealth = 1;
    public Action OnDeath;

    [Header("Decoration Settings")]
    public Collider2D damageCollider;
    public SpriteRenderer spriteRenderer;

    [Tooltip("Set this to match the length of the trap's death animation in seconds")]
    public float deathAnimationDuration = 0.5f;
    public string decorationSortingLayerName = "Decore";
    public int backgroundSortingOrder = 0;

    public virtual void ApplyDamage(IDamageable damageable)
    {
        damageable.TakeDamage(1);
    }

    public virtual void TakeDamage(int howMuch)
    {
        if (isDead) return;

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

        if (damageCollider != null)
        {
            damageCollider.enabled = false;
        }

        StartCoroutine(DeathSequence());
    }

    private IEnumerator DeathSequence()
    {
        animator?.SetBool("IsDead", true);

        yield return new WaitForSeconds(deathAnimationDuration);

        if (spriteRenderer != null)
        {
            spriteRenderer.sortingLayerName = decorationSortingLayerName;
            spriteRenderer.sortingOrder = backgroundSortingOrder;
        }
    }
}