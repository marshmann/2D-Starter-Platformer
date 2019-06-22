using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed = 40f;
    public CharacterController2D controller;

    private Animator animator;
    private SpriteRenderer sprite;
    private Rigidbody2D rb;

    private float horizontalMove = 0f;
    private bool jump = false;

    private void Update() {
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;

        if (Input.GetButtonDown("Jump")) jump = true;

        if(transform.position.x < -9 || transform.position.x > 9 || transform.position.y < -5) {
            transform.position = new Vector2(0, 5);
        }
    }

    private void FixedUpdate() {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;

        if (!Input.GetButton("Horizontal") || !controller.m_Grounded) animator.SetBool("PlayerRun", false);
        else animator.SetBool("PlayerRun", true);

        if (!controller.m_Grounded && rb.velocity.y < 0) {
            animator.SetBool("Fall", true);
            animator.SetBool("Jump", false);
        }
        else if (controller.m_Grounded && rb.velocity.y == 0) {
            animator.SetBool("Fall", false);
            animator.SetBool("Jump", false);
        }
    }

    private void Start(){
        rb = transform.GetComponent<Rigidbody2D>();
        sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        animator = transform.GetChild(0).GetComponent<Animator>();
    }
}
