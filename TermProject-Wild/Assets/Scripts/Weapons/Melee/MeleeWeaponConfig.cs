using UnityEngine;

[CreateAssetMenu(fileName = "MeleeWeaponConfig", menuName = "Game Configs/Melee Weapon Config")]
public class MeleeWeaponConfig : ScriptableObject
{
    // Variables
    public Vector3 hitboxCenter;
    public Vector3 hitboxExtents;
    
    public LayerMask hitboxMask;
}
