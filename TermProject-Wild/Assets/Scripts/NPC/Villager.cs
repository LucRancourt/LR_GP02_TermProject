using UnityEngine;

public class Villager : MonoBehaviour, IInteractable
{
    // Variables
    [SerializeField] private string message = "Empty Message";
    
    
    // Functions
    public void Interact()
    {
        Debug.Log(message);
    }
}
