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
        if (transform.position.x < -12f)
        {
            transform.position = new Vector3(10f, transform.position.y , 0);
        }
    }

    public void StopBackGround()
    {
        scrollSpeed = 0;
    }
}
