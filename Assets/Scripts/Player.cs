using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    // References
    [Header("References")]
    public Transform trans;
    public Transform modelTrans;                 // drag Player/Model here
    public CharacterController characterController;
    public GameObject cam;                       // optional (for your camera switching flow)

    // Respawn / death
    [Header("Respawn")]
    public Transform respawnPoint;               // drag RespawnPoint here (per level)
    public float respawnDelay = 1.0f;

    // Movement (lecture-style)
    [Header("Movement")]
    [Tooltip("Units moved per second at maximum speed.")]
    public float movespeed = 24f;

    [Tooltip("Time, in seconds, to reach maximum speed.")]
    public float timeToMaxSpeed = 0.26f;

    [Tooltip("Time, in seconds, to go from maximum speed to stationary.")]
    public float timeToLoseMaxSpeed = 0.2f;

    [Tooltip("Multiplier for momentum when attempting to move opposite current direction.")]
    public float reverseMomentumMultiplier = 2.2f;

    [Tooltip("How quickly the model rotates to face movement direction.")]
    public float rotationSlerp = 0.18f;

    // Gravity / grounding
    [Header("Gravity")]
    [Tooltip("Downward acceleration (negative number recommended, e.g. -25).")]
    public float gravity = -25f;

    [Tooltip("Small downward force when grounded so the controller stays snapped to the floor.")]
    public float groundStickForce = -2f;

    private Vector3 movementVelocity = Vector3.zero;   // XZ velocity (lecture)
    private float verticalVelocity = 0f;               // Y velocity (gravity)
    private bool dead;

    private float VelocityGainPerSecond => (timeToMaxSpeed <= 0.0001f) ? movespeed : movespeed / timeToMaxSpeed;
    private float VelocityLossPerSecond => (timeToLoseMaxSpeed <= 0.0001f) ? movespeed : movespeed / timeToLoseMaxSpeed;

    private void Awake()
    {
        if (trans == null) trans = transform;

        if (characterController == null)
            characterController = GetComponent<CharacterController>();

        if (modelTrans == null)
        {
            Transform m = trans.Find("Model");
            modelTrans = (m != null) ? m : trans;
        }
    }

    private void Update()
    {
        if (dead) return;

        ApplyGravity();
        Movement();
    }

    // Called by Hazard.cs
    public void Die()
    {
        if (dead) return;
        dead = true;

        Debug.Log("Player died!");

        // Stop movement immediately
        movementVelocity = Vector3.zero;
        verticalVelocity = 0f;

        // Disable update loop during respawn delay
        enabled = false;
        Invoke(nameof(Respawn), respawnDelay);
    }

    private void Respawn()
    {
        // Teleport to respawn point if set
        if (respawnPoint != null)
            trans.position = respawnPoint.position;
        else
            trans.position = new Vector3(0, 2, 0); // fallback

        // Reset velocities
        movementVelocity = Vector3.zero;
        verticalVelocity = 0f;

        dead = false;
        enabled = true;
    }

    private void ApplyGravity()
    {
        // Keep the controller glued to the ground when grounded
        if (characterController.isGrounded)
        {
            // if falling downward already, clamp to stick force
            if (verticalVelocity < 0f) verticalVelocity = groundStickForce;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        // Apply only vertical motion here
        characterController.Move(Vector3.up * verticalVelocity * Time.deltaTime);
    }

    private void Movement()
    {
        var kb = Keyboard.current;
        if (kb == null) return;

        bool forward = kb.wKey.isPressed || kb.upArrowKey.isPressed;
        bool backward = kb.sKey.isPressed || kb.downArrowKey.isPressed;
        bool right = kb.dKey.isPressed || kb.rightArrowKey.isPressed;
        bool left = kb.aKey.isPressed || kb.leftArrowKey.isPressed;

        // Z (forward/back)
        if (forward)
        {
            if (movementVelocity.z >= 0)
                movementVelocity.z = Mathf.Min(movespeed, movementVelocity.z + VelocityGainPerSecond * Time.deltaTime);
            else
                movementVelocity.z = Mathf.Min(0, movementVelocity.z + VelocityGainPerSecond * reverseMomentumMultiplier * Time.deltaTime);
        }
        else if (backward)
        {
            if (movementVelocity.z > 0)
                movementVelocity.z = Mathf.Max(0, movementVelocity.z - VelocityGainPerSecond * reverseMomentumMultiplier * Time.deltaTime);
            else
                movementVelocity.z = Mathf.Max(-movespeed, movementVelocity.z - VelocityGainPerSecond * Time.deltaTime);
        }
        else
        {
            if (movementVelocity.z > 0)
                movementVelocity.z = Mathf.Max(0, movementVelocity.z - VelocityLossPerSecond * Time.deltaTime);
            else
                movementVelocity.z = Mathf.Min(0, movementVelocity.z + VelocityLossPerSecond * Time.deltaTime);
        }

        // X (left/right)
        if (right)
        {
            if (movementVelocity.x >= 0)
                movementVelocity.x = Mathf.Min(movespeed, movementVelocity.x + VelocityGainPerSecond * Time.deltaTime);
            else
                movementVelocity.x = Mathf.Min(0, movementVelocity.x + VelocityGainPerSecond * reverseMomentumMultiplier * Time.deltaTime);
        }
        else if (left)
        {
            if (movementVelocity.x > 0)
                movementVelocity.x = Mathf.Max(0, movementVelocity.x - VelocityGainPerSecond * reverseMomentumMultiplier * Time.deltaTime);
            else
                movementVelocity.x = Mathf.Max(-movespeed, movementVelocity.x - VelocityGainPerSecond * Time.deltaTime);
        }
        else
        {
            if (movementVelocity.x > 0)
                movementVelocity.x = Mathf.Max(0, movementVelocity.x - VelocityLossPerSecond * Time.deltaTime);
            else
                movementVelocity.x = Mathf.Min(0, movementVelocity.x + VelocityLossPerSecond * Time.deltaTime);
        }

        // Apply XZ movement
        if (movementVelocity.x != 0 || movementVelocity.z != 0)
        {
            Vector3 move = new Vector3(movementVelocity.x, 0f, movementVelocity.z);
            characterController.Move(move * Time.deltaTime);

            // Rotate model toward movement direction (flat)
            Vector3 dir = new Vector3(move.x, 0f, move.z);
            if (dir.sqrMagnitude > 0.0001f)
            {
                Quaternion targetRot = Quaternion.LookRotation(dir.normalized, Vector3.up);
                modelTrans.rotation = Quaternion.Slerp(modelTrans.rotation, targetRot, rotationSlerp);
            }
        }
    }
}
