using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary> Manages the state of the level </summary>
public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public int Score { get; set; }

    [Header("Objects")]
    [SerializeField] private GameObject[] platformPrefabs;
    [SerializeField] private GameObject spawnOrigin;
    [SerializeField] private Text scoreText, HiscoreText;
    [Header("Values")]
    public float platformSpeed = .05f;
    public float maxPlatformSpeed = .1f;
    [SerializeField] private float maxSpawnInterval, minSpawnInterval, platformLifetime, acceleration = .0002f;
    [Space]
    [SerializeField] private string HomeSceneName;
    [Header("Menu items")]
    [SerializeField] private GameObject escPopup;
    [SerializeField] private GameObject failPopup;

    GameObject[] platforms;
    float initialSpeed, initialMaxInterval, initialMinInterval, spawnTimer;
    bool fail;

    void Start()
    {
        instance = this;
        initialSpeed = platformSpeed;
        initialMaxInterval = maxSpawnInterval;
        initialMinInterval = minSpawnInterval;
        Spawn();
        SaveHighScore();
    }

    void Update()
    {
        scoreText.text = "SCORE: " + Score;

        if (Input.GetKeyUp(KeyCode.Escape) && !fail)
            Pause();


        platformSpeed += acceleration * Time.deltaTime;

        if (platformSpeed >= maxPlatformSpeed)
            platformSpeed = maxPlatformSpeed;

        spawnTimer += Time.deltaTime;
        maxSpawnInterval -= acceleration * ((initialMaxInterval - initialMinInterval)/initialSpeed) * Time.deltaTime;

        if (maxSpawnInterval < minSpawnInterval)
            maxSpawnInterval = minSpawnInterval;

        if (spawnTimer > maxSpawnInterval)
        {
            Spawn();
            spawnTimer = 0;
        }
    }

    public void Pause()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        escPopup.active = !escPopup.active;
    }

    public void Fail()
    {
        fail = true;
        Time.timeScale = 0;
        SaveHighScore();
        failPopup.SetActive(true);
    }

    public void Reset()
    {
        Time.timeScale = 1;
        platformSpeed = initialSpeed;
        Score = 0;
        fail = false;
        SaveHighScore();
        failPopup.SetActive(false);

        platforms = GameObject.FindGameObjectsWithTag("Platform");

        foreach (var platform in platforms)
        {
            Destroy(platform);
        }

        platformSpeed = initialSpeed;
        maxSpawnInterval = initialMaxInterval;
        minSpawnInterval = initialMinInterval;
        spawnTimer = 0;
        Spawn();
    }

    public void Exit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(HomeSceneName);
    }

    void Spawn()
    {
        var newPlatform = Instantiate(platformPrefabs[Random.Range(0, platformPrefabs.Length)]);
        newPlatform.transform.position = spawnOrigin.transform.position + Vector3.up;
        Destroy(newPlatform, platformLifetime);
    }

    public void IncrementScore()
    {
        Score++;
    }

    void SaveHighScore()
    {
        if (Score > PlayerPrefs.GetInt("HS"))
        {
            PlayerPrefs.SetInt("HS", Score);
            HiscoreText.text = "YOUR NEW HIGH SCORE IS: " + PlayerPrefs.GetInt("HS") + "!!!";
            HiscoreText.color = Color.red;
        }
        else
        {
            HiscoreText.text = "HIGH SCORE: " + PlayerPrefs.GetInt("HS");
            HiscoreText.color = Color.yellow;
        }
    }

    void OnApplicationQuit()
    {
        SaveHighScore();
    }
}
