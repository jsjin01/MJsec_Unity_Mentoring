using UnityEngine;

public class w3CatcusMove : MonoBehaviour
{
    [SerializeField] int moveSpeed = 2;

    void Update()
    {
        // �������� �̵�
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

        // ���� ��ġ�� ������ �ٽ� ���������� �̵���Ŵ (����)
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
