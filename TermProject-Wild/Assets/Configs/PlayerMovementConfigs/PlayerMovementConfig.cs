using UnityEngine;

[CreateAssetMenu(fileName = "NewMovementConfig", menuName = "Game Configs/Movement Config")]
public class PlayerMovementConfig : ScriptableObject
{
    [Header("Movement")]
    public float targetMoveSpeed = 5.0f;
    public float accelerationRate = 10.0f;
    public float decelerationRate = 15.0f;
    public float rateSwitchDirectionMultiplier = 2.0f;


    [Header("Blink")]
    public float blinkDistance = 10.0f;
    public float blinkCooldown = 2.0f;


    [Header("Crouch")]
    public float crouchSpeed = 2.5f;


    [Header("Jump")]
    public float jumpBufferTime = 0.2f;

    public float coyoteTime = 0.2f;

    public float initialJumpForce = 7.0f;
    public float holdtimeJumpForce = 0.2f;
    public float maxJumpHoldTime = 0.3f;


    [Header("CheckDistances")]
    public float groundCheckDistance = 0.2f;

    public float slopeCheckDistance = 0.5f;


    [Header("Camera/Look")]
    public float lookSpeed = 0.6f;
    public float lookSmoothTime = 0.1f;
    public float xCameraBounds = 60.0f;


    [Header("Miscellaneous")]
    public float gravityForce = 10.0f;
    public float gravityMultiplier = 10.0f;

    public float airControlFactor = 1.0f;
}

