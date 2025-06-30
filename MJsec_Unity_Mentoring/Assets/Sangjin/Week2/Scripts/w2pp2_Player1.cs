using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class w2pp2_Player : MonoBehaviour
{
    [Header("예시")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 7f;

    bool isGrounded = false;
    bool isDashable = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");

        // 좌우 이동: x만 제어, y는 중력 유지
        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);

        // 점프: 땅에 있을 때만 위쪽 힘 추가
        if(Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.AddForce(Vector2.up * rb.gravityScale * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && isDashable)
        {
            rb.transform.position += new Vector3 (moveX * 3, 0f);
            StartCoroutine(Cooltime());
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
        }

        if(collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Debug.Log("적 제거!!!");
        }
    }

    IEnumerator Cooltime()
    {
        isDashable = false;
        yield return new WaitForSeconds(3f);
        isDashable = true ;
    }
}

