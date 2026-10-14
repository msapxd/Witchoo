
using UnityEngine;

public abstract class PlayableCharacter : MonoBehaviour, IDamageable
{
    public int speed = 5;

    public int maxHealth => PlayerData.maxHealth;
    public int currentHealth => PlayerData.currentHealth;

    public virtual void Movement()
    {
        // Handled by PlayerMovement
    }

    public abstract void SpecialAbility();

    public virtual void ApplyDamage(IDamageable damageable)
{
    damageable.TakeDamage(1);
}

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
    }
}
