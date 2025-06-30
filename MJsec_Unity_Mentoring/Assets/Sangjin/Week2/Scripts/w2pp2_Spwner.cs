using UnityEngine;

public class w2pp2_Spwner : MonoBehaviour
{

    [SerializeField] GameObject enemy;
    void Start()
    {
        InvokeRepeating("Spawn", 2f, 5f);
    }
    
    void Spawn()
    {
        float posX = Random.Range(-5f, 5f);
        Vector3 vec = new Vector3(posX, 2, 0);
        Instantiate(enemy, vec, new Quaternion(0,0,0,0));
    }
}
