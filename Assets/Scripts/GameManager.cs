using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary> Manages the state of the whole application </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] private string gameScene;
    [SerializeField] private Text highScore, getReady;

    bool start;

    void Start()
    {
        highScore.text = "HIGH SCORE: " + PlayerPrefs.GetInt("HS");
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q))
            Application.Quit();

        if (start)
            getReady.transform.position = Vector3.MoveTowards(getReady.transform.position, getReady.transform.parent.position, 2000 * Time.deltaTime);
    }

    public void Play()
    {
        start = true;
        StartCoroutine(LoadScene(gameScene));
    }

    IEnumerator LoadScene(string sceneName)
    {
        Debug.Log("Loading game!");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(sceneName);
    }
}