using UnityEngine;

public class w3BackGround : MonoBehaviour
{
    [SerializeField] float scrollSpeed = 2f;          // �̵� �ӵ�


    void Start()
    {
    }

    void Update()
    {
        // �������� �̵�
        transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);

        // ���� ��ġ�� ������ �ٽ� ���������� �̵���Ŵ (����)
        if (transform.position.x < -15.5f)
        {
            transform.position = new Vector3(0f, transform.position.y , 0);
        }
    }
}
