using UnityEngine;

public class Cat : PlayableCharacter
{
    public override void SpecialAbility()
    {
        Debug.Log("Cat lights up the pumpkin!");
    }

    public override void ApplyDamage(IDamageable damageable)
    {
        //if (damageable is Pumpkin)
       // {
        damageable.TakeDamage(1);
       // }
    }
}