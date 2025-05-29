using UnityEngine;

public class HotbarInventory : MonoBehaviour
{
    // Variables
    private Weapon[] _heldWeapons;
    
    
    // Functions
    Weapon ReturnItem(int index)
    {
        if (index >= _heldWeapons.Length) return null;
        
        return _heldWeapons[index];
    }
}
