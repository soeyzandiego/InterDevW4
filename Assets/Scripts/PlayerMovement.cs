using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float velocityCap = 200f;

    [Header("Jumping")]
    [SerializeField] float castDist = 1f;
    [SerializeField] float jumpForce = 15f;
    [SerializeField] float gravScale = 5f;
    [SerializeField] float gravFall = 40f;

    Rigidbody2D rb;

    float horAxis;
    bool grounded;

    bool jump = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horAxis = Input.GetAxisRaw("Horizontal");

        PlayerDirection();

        if (grounded && Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        //if (Mathf.Abs(horAxis) > 0) { rb.velocity = new Vector2(horAxis * moveSpeed, rb.velocity.y); }


        if (jump)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jump = false;
        }

        if (rb.velocity.y > 0) { rb.gravityScale = gravScale; }
        else if (rb.velocity.y < 0) { rb.gravityScale = gravFall; }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, castDist, LayerMask.GetMask("Ground"));
        Debug.DrawRay(transform.position, Vector2.down * castDist, Color.red);

        if (hit.collider != null) { grounded = true; }
        else { grounded = false; }

        Vector2 moveVec = new Vector2(horAxis * moveSpeed * Time.fixedDeltaTime, 0);
        rb.AddForce(moveVec, ForceMode2D.Impulse);

        float cappedVel = Mathf.Clamp(rb.velocity.x, -velocityCap, velocityCap);
        rb.velocity = new Vector2(cappedVel, rb.velocity.y);
    }

    void PlayerDirection()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        bool facingRight = (mousePos.x > transform.position.x);

        if (facingRight)
        {
            Vector3 rotation = transform.localEulerAngles;
            rotation.y = 0;
            transform.localEulerAngles = rotation;
        }
        else
        {
            Vector3 rotation = transform.localEulerAngles;
            rotation.y = 180f;
            transform.localEulerAngles = rotation;
        }
    }

    public void Knockback(Vector2 dir, float amount)
    {
        rb.AddForce(-dir * amount, ForceMode2D.Impulse);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector2.down * castDist);
    }

}
