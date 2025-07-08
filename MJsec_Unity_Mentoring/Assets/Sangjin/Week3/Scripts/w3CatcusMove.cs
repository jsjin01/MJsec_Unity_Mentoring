using UnityEngine;

public class w3CatcusMove : MonoBehaviour
{
    [SerializeField] int moveSpeed = 2;

    void Update()
    {
        // 왼쪽으로 이동
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

        // 일정 위치를 지나면 다시 오른쪽으로 이동시킴 (재사용)
        if (transform.position.x < -7f)
        {
            Destroy(gameObject);
        }
    }

        void OnCollisionEnter2D(Collision2D collision)
        {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    public void StopMoving()
    {
        moveSpeed = 0;
    }
}
