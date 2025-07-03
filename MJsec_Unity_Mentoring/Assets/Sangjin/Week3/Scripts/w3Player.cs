using UnityEngine;

public class w3Player : MonoBehaviour
{
    [SerializeField]Rigidbody2D rb;
    [SerializeField]Animator anit;

    int hp = 3;
    bool isGrounded = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * rb.gravityScale * 7, ForceMode2D.Impulse);
            isGrounded = false;
            anit.SetBool("jump", true);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
            anit.SetBool("jump", false);
        }
        
        if (collision.gameObject.CompareTag("Cactus"))
        {
            if (hp == 0)
            {
                Debug.Log($"사망");
                anit.SetTrigger("die");
            }
            else
            {
                anit.SetTrigger("hit");
                hp -= 1;
                Debug.Log($"남은 체력 : {hp}");
            }
        }
    }
}
