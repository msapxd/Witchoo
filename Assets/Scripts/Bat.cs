using UnityEngine;

public class Bat : PlayableCharacter
{
    public override void SpecialAbility()
    {
        Debug.Log("Bat hangs lights!");
    }

    public override void ApplyDamage(IDamageable damageable)
    {
        //if (damageable is StringLights)
        //{
            damageable.TakeDamage(1);
        //}
    }
}
