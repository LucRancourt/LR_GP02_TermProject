using UnityEngine;

public class HitscanWeapon : RangedWeapon
{
    // Variables
    [SerializeField] private float range = 100.0f;
    
    [SerializeField] private LayerMask targetLayer;
    
    
    // Functions
    public override void Use()
    {
        if (!CanFire()) return;

        base.Use();

        Debug.DrawRay(muzzle.transform.position, muzzle.transform.forward * range, Color.blue, 5.0f);
    }
}
