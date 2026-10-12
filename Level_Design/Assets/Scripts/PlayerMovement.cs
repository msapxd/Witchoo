using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class TopDownPlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 5f;
    public float sprintSpeed = 9f;

    [Header("Options")]
    public bool rotateToMovement = false;   // Rotate sprite to face movement direction
    public bool noClip = false;             // Toggle with N to walk through walls

    Rigidbody2D rb;
    Collider2D[] colliders;
    Vector2 input;
    bool sprinting;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.freezeRotation = true;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        colliders = GetComponentsInChildren<Collider2D>();
    }

    void Update()
    {
        var kb = Keyboard.current;
        if (kb == null) return;

        input = Vector2.zero;
        if (kb.wKey.isPressed || kb.upArrowKey.isPressed) input.y += 1f;
        if (kb.sKey.isPressed || kb.downArrowKey.isPressed) input.y -= 1f;
        if (kb.dKey.isPressed || kb.rightArrowKey.isPressed) input.x += 1f;
        if (kb.aKey.isPressed || kb.leftArrowKey.isPressed) input.x -= 1f;
        input = Vector2.ClampMagnitude(input, 1f); // no faster diagonals

        sprinting = kb.leftShiftKey.isPressed;

        if (kb.nKey.wasPressedThisFrame)
        {
            noClip = !noClip;
            foreach (var c in colliders) c.enabled = !noClip;
        }
    }

    void FixedUpdate()
    {
        float speed = sprinting ? sprintSpeed : walkSpeed;

        if (noClip)
            rb.position += input * speed * Time.fixedDeltaTime;
        else
            rb.linearVelocity = input * speed;

        if (rotateToMovement && input.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;
        }
    }
}