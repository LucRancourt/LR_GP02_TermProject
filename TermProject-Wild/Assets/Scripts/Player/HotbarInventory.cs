using UnityEngine;

public class HotbarInventory : MonoBehaviour
{
    // Variables
    [SerializeField] private Weapon[] _heldWeapons;
    
    
    // Functions
    public Weapon ReturnItem(int index)
    {
        index -= 1;

        if (index >= _heldWeapons.Length) return null;
        
        return _heldWeapons[index];
    }
}
