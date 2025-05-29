using UnityEngine;

public static class HelpfulFunctions
{
    // Functions
    public static Vector3 GetDirection(Vector3 target, Vector3 self)
    {
        return (target - self).normalized;
    }


}
