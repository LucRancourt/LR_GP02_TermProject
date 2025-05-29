using UnityEngine;

public class MeleeWeapon : Weapon
{
    // Variables 
    [SerializeField] private MeleeWeaponConfig meleeConfig;  // M<-- can this be applied to the Weapon class or do I just make 1 for Melee and 1 for Ranged? (and just copy Weapon stuff into those?)
    
    
    // Functions
    public override void Use()
    {
        if (!CanUse()) return;
        
        base.Use();

        Vector3 hitboxCenter = GetHitboxCenter();

        Collider[] hitTargets = Physics.OverlapBox(hitboxCenter, meleeConfig.hitboxExtents, transform.rotation, meleeConfig.hitboxMask);

        foreach (Collider target in hitTargets)
        {
            Debug.DrawRay(transform.position, HelpfulFunctions.GetDirection(target.transform.position, transform.position), Color.blue, 5.0f);
            
            // check for "IsHittable" -> Then HIT <- make Interface so you can call same function on all

            
            // Knockback is b (target) - a (me)  . Normalize = Direction
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

    public override void StopUsing()
    {
        
    }
}
