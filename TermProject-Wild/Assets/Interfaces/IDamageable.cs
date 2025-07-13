using UnityEngine;


public interface IDamageable
{
    public void TakeDamage(float damage, GameObject caller);
    public void TakeDamage(float damage);
}
