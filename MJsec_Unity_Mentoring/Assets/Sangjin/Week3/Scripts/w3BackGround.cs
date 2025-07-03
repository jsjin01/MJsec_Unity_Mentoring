using UnityEngine;

public class w3BackGround : MonoBehaviour
{
    [SerializeField] float scrollSpeed = 2f;          // 이동 속도


    void Start()
    {
    }

    void Update()
    {
        // 왼쪽으로 이동
        transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);

        // 일정 위치를 지나면 다시 오른쪽으로 이동시킴 (재사용)
        if (transform.position.x < -15.5f)
        {
            transform.position = new Vector3(0f, transform.position.y , 0);
        }
    }
}
