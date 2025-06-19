using UnityEngine;

public class Fracture : MonoBehaviour
{
    // Variables
    [TooltipAttribute("Only works if you have a second version of the Object that is pre-fractured")]
    [SerializeField] private GameObject fracturedVersion;
    [SerializeField] private float breakForce;
    
    
    // Functions
    void OnCollisionEnter(Collision collision)
    {
        GameObject fract = Instantiate(fracturedVersion, transform.position, transform.rotation);

        foreach (Rigidbody rb in fract.GetComponentsInChildren<Rigidbody>())
            rb.AddForce((rb.transform.position - transform.position).normalized * breakForce);

        Destroy(this.gameObject);
    }
}
