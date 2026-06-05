using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float timeRemaining = 120f;
    public TextMeshProUGUI timerText;
    public bool IsRunning = false;

    public int finalScore;



    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        timerText = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();
        IsRunning = true;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if(IsRunning) timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0)
        {
            EndGame();
        }

        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        timerText.text = Mathf.Ceil(timeRemaining).ToString();
    }

    void EndGame()
    {
        finalScore = ScoreManager.Instance.score;
        SceneManager.LoadScene("EndGameScene");
        IsRunning = false;
    }

    public void ResetGame()
    {
        finalScore = 0;
        timeRemaining = 120f;
    }

    public void AddTime(float amount)
    {
        timeRemaining += amount;
    }
}