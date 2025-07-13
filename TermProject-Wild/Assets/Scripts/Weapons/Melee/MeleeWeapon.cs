using UnityEngine;

public class MeleeWeapon : Weapon
{
    // Variables 
    [SerializeField] private MeleeWeaponConfig meleeConfig;
    
    
    // Functions
    public override void Use()
    {
        if (!CanUse()) return;
        
        base.Use();


        Vector3 hitboxCenter = GetHitboxCenter();

        Collider[] hitTargets = Physics.OverlapBox(hitboxCenter, meleeConfig.hitboxExtents, transform.rotation, meleeConfig.hitboxMask);

        foreach (Collider target in hitTargets)
        {
            if (target.TryGetComponent(out IDamageable damageable))
            {
                Knockback(target.gameObject);
                damageable.TakeDamage(meleeConfig.damage, gameObject);
            }

            Debug.Log(target.name);
            Debug.DrawRay(transform.position, HelpfulFunctions.GetDirection(target.transform.position, transform.position), Color.blue, 5.0f);
        }
    }

    private Vector3 GetHitboxCenter()
    {
        return transform.position
         + meleeConfig.hitboxCenter.x * transform.right
         + meleeConfig.hitboxCenter.y * transform.up
         + meleeConfig.hitboxCenter.z * transform.forward;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(GetHitboxCenter(), meleeConfig.hitboxExtents);
    }

    public void Knockback(GameObject target)
    {
        if (target.TryGetComponent(out Rigidbody targetRb))
        {
            Vector3 knockbackDirection = HelpfulFunctions.GetDirection(target.transform.position, transform.position);

            targetRb.AddForce(knockbackDirection * meleeConfig.knockbackForce, ForceMode.Impulse);
        }
    }
}
