using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerStatsData PlayerStatsData;
    private float moveInput;

    [Header("Ground check")]
    public LayerMask groundLayer;
    [Range(0, 1)]
    public float GroundRayCastBuffer = 0.2f;

    private Rigidbody2D rb;
    private CapsuleCollider2D playerCollider;

    private bool isGrounded;
    private float currentSpeed;
    private bool earlyReleaseJump;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        // Di chuyển trái phải
        moveInput = Input.GetAxisRaw("Horizontal");

        // Kiểm tra nhảy
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            HandleJump();
        }
        if (rb.velocity.y <= 0)
        {
        }

        // Kiểm tra khi nhả nút nhảy sớm, cho rơi sớm (cho vận tốc giảm nhanh về 0 luôn).
        if (Input.GetButtonUp("Jump"))
        {
            earlyReleaseJump = true;
        }
        if (earlyReleaseJump)
        {
            HandleEarlyReleaseJump();
        }

        // kiểm tra ground
        if (IsGrounded())
        {
            earlyReleaseJump = false;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void FixedUpdate()
    {
        HandleMovement();

        HandleInAir();
    } 

    public void HandleMovement()
    {
        if (moveInput != 0)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, moveInput * PlayerStatsData.moveSpeed, PlayerStatsData.acceleration * Time.deltaTime);
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, PlayerStatsData.deceleration * Time.deltaTime);
        }

        rb.velocity = new Vector2(currentSpeed, rb.velocity.y);

    }

    public void HandleInAir()
    {
        if (!IsGrounded() && rb.velocity.y <= 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.MoveTowards(rb.velocity.y, PlayerStatsData.maxFallSpeed, PlayerStatsData.fallAcceleration * Time.deltaTime));
        }
    }
    public void HandleJump()
    {
        // Tăng vận tốc nhảy dần dần
        if (rb.velocity.y < PlayerStatsData.jumpForce)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Lerp(rb.velocity.y, PlayerStatsData.jumpForce,
                PlayerStatsData.jumpAcceleration));
        }

        // Đặt lại trạng thái nhảy sau khi đạt đến tốc độ tối đa
        if (rb.velocity.y >= PlayerStatsData.jumpForce)
        {
        }

    }

    public void HandleEarlyReleaseJump()
    {
        if (rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.MoveTowards(rb.velocity.y, 0, PlayerStatsData.fallAcceleration * 2 * Time.deltaTime));
        }
    }

    private bool IsGrounded()
    {
        // Kiểm tra xem nhân vật có đứng trên mặt đất hay không
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, playerCollider.size.y/2 + GroundRayCastBuffer, groundLayer);

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        // Vẽ một đường ray để hiển thị khoảng cách kiểm tra mặt đất
        Gizmos.color = Color.red;
        if (isGrounded) Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * (1 + GroundRayCastBuffer));
    }
}
