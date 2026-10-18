using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    [SerializeField] private Animator animator;
    private Vector3 baseScale;
    public bool isDialogueActive = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        baseScale = transform.localScale;
    }

    void Update()
    {
        if (DialogueManager.isActive)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        rb.linearVelocity = moveInput * moveSpeed;

        if (moveInput.x < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(baseScale.x), baseScale.y, baseScale.z);
        }
        else if (moveInput.x > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(baseScale.x), baseScale.y, baseScale.z);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (DialogueManager.isActive)
        {
            moveInput = Vector2.zero; 
            animator.SetBool("IsWalking", false); 
            return;
        }
        animator.SetBool("IsWalking", true);
        if (context.canceled)
        {
            animator.SetBool("IsWalking", false);
            animator.SetFloat("LastInputX", moveInput.x);
            animator.SetFloat("LastInputY", moveInput.y);
        }
        moveInput = context.ReadValue<Vector2>();
        animator.SetFloat("InputX", moveInput.x);
        animator.SetFloat("InputY", moveInput.y);
    }

    public void SetAnimator(Animator newAnimator)
    {
        animator = newAnimator;
    }
}