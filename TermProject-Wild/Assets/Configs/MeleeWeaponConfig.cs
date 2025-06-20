using UnityEngine;

[CreateAssetMenu(fileName = "MeleeWeaponConfig", menuName = "Game Configs/Melee Weapon Config")]
public class MeleeWeaponConfig : ScriptableObject
{
    // Variables
    [Header("General Weapon")] 
    public float damage;
    public float knockbackForce;
    
    [Header("Melee Specific")]
    public Vector3 hitboxCenter;
    public Vector3 hitboxExtents;
    
    public LayerMask hitboxMask;

    //public float attackCooldown;
}
