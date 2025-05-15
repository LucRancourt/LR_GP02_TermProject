using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMovementConfig", menuName = "Game Configs/Movement Config")]
public class PlayerMovementConfig : ScriptableObject
{
    public float targetMoveSpeed = 5.0f;
    public float accelerationRate = 10.0f;
    public float decelerationRate = 15.0f;
    
    public float baseJumpForce = 8.0f;
    public float maxJumpHoldTime = 1.0f;
    
    public float gravityMultiplier = 10.0f;
    public float airControlFactor = 1.0f;
    
    public float groundCheckDistance = 0.2f;
    public float groundedTimer = 0.2f;

    public float lookSpeedDivider = 6.0f;
    public float lookSmoothTime = 0.1f;
    public float xCameraBounds = 60.0f;
}

