using UnityEngine;

public class w3CactusSpawner : MonoBehaviour
{
    [SerializeField] float spawnSpeed = 2.0f;
    [SerializeField] GameObject[] catcuses;
    [SerializeField] GameObject parent;
    void Start()
    {
        InvokeRepeating("Spawn", 1f, 5f);
    }

    void Spawn()
    {
        if (catcuses == null || catcuses.Length == 0)
        {
            Debug.LogWarning("선인장 프리팹이 설정되지 않았습니다!");
            return;
        }

        int num = Random.Range(0, catcuses.Length);
        GameObject catcus = Instantiate(catcuses[num], new Vector3(4f, -0.8f, 0), Quaternion.identity);
        catcus.transform.SetParent(parent.transform);
    }
}
