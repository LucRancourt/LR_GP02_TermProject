using UnityEngine;

public static class HelpfulFunctions
{
    // Functions
    public static Vector2 GetDirection(Vector2 target, Vector2 self)
    {
        return (target - self).normalized;
    }

    public static Vector3 GetDirection(Vector3 target, Vector3 self)
    {
        return (target - self).normalized;
    }



        // Ints
    public static int Clamp(int valueToClamp, int min, int max)
    {
        if (valueToClamp > max)
            return max;
        else if (valueToClamp < min)
            return min;
        else
            return valueToClamp;
    }

    public static void ClampRef(ref int valueToClamp, int min, int max)
    {
        if (valueToClamp > max)
            valueToClamp = max;
        else if (valueToClamp < min)
            valueToClamp = min;
    }

        // Floats
    public static float Clamp(float valueToClamp, float min, float max)
    {
        if (valueToClamp > max)
            return max;
        else if (valueToClamp < min)
            return min;
        else
            return valueToClamp;
    }

    public static void ClampRef(ref float valueToClamp, float min, float max)
    {
        if (valueToClamp > max)
            valueToClamp = max;
        else if (valueToClamp < min)
            valueToClamp = min;
    }
}
