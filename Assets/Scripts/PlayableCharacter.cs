
using System;
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
            Debug.Log("restart room");
        }
    }

    public virtual void Die()
    {
        // Later, LevelManager will handle game over.
        animator?.SetBool("IsDead", true);
        levelManager.GameOver();
    }

    public Animator GetAnimator()
    {
        return animator;
    }
}
