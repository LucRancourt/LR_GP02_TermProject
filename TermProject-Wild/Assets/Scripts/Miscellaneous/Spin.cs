using UnityEngine;

public class Spin : MonoBehaviour
{
    private void FixedUpdate()
    {
        Vector3 newRot = transform.rotation.eulerAngles;

        newRot.y += 20.0f * Time.fixedDeltaTime;

        transform.rotation = Quaternion.Euler(newRot);
    }
}
