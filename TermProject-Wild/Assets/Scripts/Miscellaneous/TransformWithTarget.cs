using UnityEngine;

public class TransformWithTarget : MonoBehaviour
{
    // Variables
    [SerializeField] private Transform targetTransform;

    [Header("Position Values")]
    private Vector3 newPos;
    [SerializeField] private bool includePosX;
    [SerializeField] private bool includePosY;
    [SerializeField] private bool includePosZ;

    [Header("Rotation Values")]
    private Vector3 newEulerRot;
    [SerializeField] private bool includeRotX;
    [SerializeField] private bool includeRotY;
    [SerializeField] private bool includeRotZ;


    // Functions
    private void FixedUpdate()
    {
        newPos = targetTransform.position;

        newPos.x = includePosX ? newPos.x : transform.position.x;
        newPos.y = includePosY ? newPos.y : transform.position.y;
        newPos.z = includePosZ ? newPos.z : transform.position.z;

        transform.position = newPos;


        newEulerRot = targetTransform.rotation.eulerAngles;

        newEulerRot.x = includeRotX ? newEulerRot.x : transform.rotation.eulerAngles.x;
        newEulerRot.y = includeRotY ? newEulerRot.y : transform.rotation.eulerAngles.y;
        newEulerRot.z = includeRotZ ? newEulerRot.z : transform.rotation.eulerAngles.z;

        transform.rotation = Quaternion.Euler(newEulerRot);
    }
}
