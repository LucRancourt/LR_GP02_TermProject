using UnityEngine;

public class PointManager : MonoBehaviour
{
    // Variables
    private int _numberOfPoints = 0;


    // Functions
    private void ResetPoints()
    {
        _numberOfPoints = 0;
    }

    public void AddPoints(int value)
    {
        _numberOfPoints += value;
    }
}
