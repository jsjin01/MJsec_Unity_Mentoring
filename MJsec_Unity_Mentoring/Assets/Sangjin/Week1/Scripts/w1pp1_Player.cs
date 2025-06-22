using UnityEngine;

public class w1pp1_Player : MonoBehaviour
{
    [Header("예시")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] int speed = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector2 move = new Vector2(moveX, moveY);

        rb.linearVelocity = move.normalized * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("wall"))
        {
            Debug.Log("벽 충돌!!!!");
        }
    }
}
