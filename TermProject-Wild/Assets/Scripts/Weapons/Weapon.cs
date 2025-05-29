using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    // Variables
    [Header("Default Weapon Details")]
    [SerializeField] protected float damage = 1.0f;
    [SerializeField] private float weaponCooldown = 0.2f;
    private WaitForSeconds _cooldownWait;
    private bool _onCooldown;
    
    
    
    // Functions
    protected virtual void Awake()
    {
        _cooldownWait = new WaitForSeconds(weaponCooldown);
    }
    
    public virtual void Use()
    {
        //if (onCooldown) return;   <- try to get working
        
        StartCoroutine(InitiateWeaponCooldown());
    }
    
    IEnumerator InitiateWeaponCooldown()
    {
        _onCooldown = true;
        
        yield return _cooldownWait;
        
        _onCooldown = false;
    }

    public virtual void StopUsing()
    {
        
    }

    protected bool CanUse()
    {
        return !_onCooldown;
    }
}
