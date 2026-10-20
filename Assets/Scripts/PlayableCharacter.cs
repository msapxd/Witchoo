
using System;
using System.Collections;
using UnityEngine;

public abstract class PlayableCharacter : MonoBehaviour, IDamageable
{
    public  LevelManager levelManager;
    public int speed = 5;

    public int maxHealth => PlayerData.maxHealth;
    public int currentHealth => PlayerData.currentHealth;
    public Animator animator;

    public virtual void Movement()
    {
        // Handled by PlayerMovement
    }

    public abstract void SpecialAbility();

    public abstract void ApplyDamage(IDamageable damageable);

    public virtual void TakeDamage(int howMuch)
    {
        PlayerData.currentHealth =
            Mathf.Max(0, PlayerData.currentHealth - howMuch);

        if (currentHealth == 0)
        {
            Die();
        }
        else
        {
            levelManager.LostLife();
        }
    }

    public virtual void Die()
    {
        //hand the sequence over to the Coroutine
        StartCoroutine(DeathSequence());
    }

    private IEnumerator DeathSequence()
    {
        //body falling animation
        animator?.SetBool("IsDead", true);

        //wait for body to hit the floor
        yield return new WaitForSeconds(0.5f);

        //spawn ghost effect at body's coordinates
        if (VFXManager.Instance != null)
        {
            VFXManager.Instance.PlayVFX("Ghost", transform.position, 2.0f);
        }

        //wait for  ghost to finish floating up
        yield return new WaitForSeconds(1.0f);

        //trigger Game Over fade
        levelManager.GameOver();
    }

    public Animator GetAnimator()
    {
        return animator;
    }
}
