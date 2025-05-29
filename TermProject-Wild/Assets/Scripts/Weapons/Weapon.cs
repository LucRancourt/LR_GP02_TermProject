using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    // Variables
    [Header("Default Weapon Details")]
    [SerializeField] private float weaponCooldown = 0.2f;
    private WaitForSeconds _cooldownWait;
    protected bool onCooldown;
    
    
    
    // Functions
    protected virtual void Awake()
    {
        _cooldownWait = new WaitForSeconds(weaponCooldown);
    }
    
    public virtual void Use()
    {
        StartCoroutine(InitiateWeaponCooldown());
    }

    public virtual void StopUsing()
    {
        
    }
    
    IEnumerator InitiateWeaponCooldown()
    {
        onCooldown = true;
        
        yield return _cooldownWait;
        
        onCooldown = false;
    }
}
