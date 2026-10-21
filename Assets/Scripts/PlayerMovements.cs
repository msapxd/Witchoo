using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    [SerializeField] private Animator animator;
    private Vector3 baseScale;
    private bool wasAttacking = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        baseScale = transform.localScale;
    }

    void Update()
    {
        bool isAttacking = animator != null && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack");

        if (wasAttacking && !isAttacking)
        {
            //if you are still holding movement keys when the attack ends, force the walk state on!
            bool isHoldingKey = moveInput != Vector2.zero;
            if (animator != null)
            {
                animator.SetBool("IsWalking", isHoldingKey);
            }
        }
        wasAttacking = isAttacking;

        //if dialogue, death, or attack is active, freeze position AND force the walking animation off!
        if (DialogueManager.isActive || animator == null || !animator.gameObject.activeInHierarchy || animator.GetBool("IsDead") || isAttacking)
        {
            rb.linearVelocity = Vector2.zero;

            if (animator != null)
            {
                animator.SetBool("IsWalking", false);
            }
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
        bool isAttacking = animator != null && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack");
        if (DialogueManager.isActive || (animator != null && animator.GetBool("IsDead")) || isAttacking)
        {
            moveInput = Vector2.zero;

            if (animator != null) animator.SetBool("IsWalking", false);
            return;
        }

        moveInput = context.ReadValue<Vector2>();

        if (animator == null || !animator.gameObject.activeInHierarchy)
        {
            return;
        }

        animator.SetBool("IsWalking", true);
        //audioManager.PlaySfx(audioManager.witchWalk);
        if (context.canceled)
        {
            animator.SetBool("IsWalking", false);
            animator.SetFloat("LastInputX", moveInput.x);
            animator.SetFloat("LastInputY", moveInput.y);
        }
        animator.SetFloat("InputX", moveInput.x);
        animator.SetFloat("InputY", moveInput.y);
    }

    public void SetAnimator(Animator newAnimator)
    {
        animator = newAnimator;

        if (animator != null)
        {
            bool isMoving = moveInput != Vector2.zero;
            animator.SetBool("IsWalking", isMoving);
            animator.SetFloat("InputX", moveInput.x);
            animator.SetFloat("InputY", moveInput.y);
        }
    }
}