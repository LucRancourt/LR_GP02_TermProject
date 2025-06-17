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
    
    public float slideSpeed = 10.0f;

    public float crouchSpeed = 2.5f;
    
    public float initialJumpForce = 7.0f;
    public float holdtimeJumpForce = 0.2f;
    public float maxJumpHoldTime = 0.3f;
    
    public float gravityMultiplier = 10.0f;
    public float airControlFactor = 1.0f;
    
    public float groundCheckDistance = 0.2f;
    public float groundedTimer = 0.2f;

    public float slopeCheckDistance = 0.5f;

    public float lookSpeed = 0.6f;
    public float lookSmoothTime = 0.1f;
    public float xCameraBounds = 60.0f;
}

