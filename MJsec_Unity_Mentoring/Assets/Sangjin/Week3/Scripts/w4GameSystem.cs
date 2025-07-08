using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class w4GameSystem : MonoBehaviour
{
    //static으로 선언하여 다른 데에서 가져다 쓰기 편하게 만들기 위해
    public static w4GameSystem Instance { get; private set; }

    [SerializeField] private float scoreRate = 10f;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text highScoreText;
    [SerializeField] Slider hpbar;
    [SerializeField] GameObject gameover;

    [SerializeField] GameObject b1;
    [SerializeField] GameObject b2;
    [SerializeField] GameObject sp;

    int high_score;
    float score;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    void Start()
    {
        score = 0f;
        high_score = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score : " + high_score;
        UpdateScoreUI();
    }

    void Update()
    {
        // 프레임마다 경과된 시간만큼 점수를 누적
        score += scoreRate * Time.deltaTime;
        // UI에는 정수 형태로 표시
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "SCORE:"+ Mathf.FloorToInt(score).ToString();
        }
    }

    public void HpUpdate(int hp)
    {
        hpbar.value = hp;
    }

    public void GameOver()
    {
        gameover.SetActive(true);
        b1.GetComponent<w3BackGround>().StopBackGround();
        b2.GetComponent<w3BackGround>().StopBackGround();
        sp.GetComponent<w3CactusSpawner>().StopSpawning();

        var spawners = Object.FindObjectsByType<w3CatcusMove>(FindObjectsSortMode.None);
        foreach (var catcus in spawners)
        {
            catcus.StopMoving();  
        }

        high_score = (high_score > (int)score) ? high_score : (int)score;
        PlayerPrefs.SetInt("HighScore", high_score);
    }

    public void ResetCurrentScene()
    {
        var active = SceneManager.GetActiveScene();
        SceneManager.LoadScene(active.buildIndex);
    }
}
