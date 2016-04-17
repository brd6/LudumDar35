using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour
{
    public static GamePlayController instance;
    public static int pauseCp = 0;

    public GameObject[] players;
    public int currentPlayer;
    public bool isGameOver = false;
    public AudioClip okSound;
    public AudioClip failSound;
    public Sprite pauseActivedImage;
    public Sprite pauseDeactivateImage;


    [SerializeField]
    private Text lifeText;

    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private GameObject gameOverPanel;

    void Awake()
    {
        MakeInstance();

        players[currentPlayer].GetComponent<Renderer>().enabled = true;
        players[currentPlayer].GetComponent<BoxCollider2D>().enabled = true;
        gameOverPanel.SetActive(false);

    }

    public void UpdateUI()
    {
        lifeText.text = GameManager.instance.playerLife.ToString();
        scoreText.text = GameManager.instance.score.ToString();

    }

    void MakeInstance()
    {
        if (instance == null)
            instance = this;
    }

    // Use this for initialization
    void Start()
    {

        //LifeText.enabled = false;
        lifeText.text = GameManager.instance.playerLife + "";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            QuitGame();
    }

    public void PauseGame()
    {
        GameObject pauseBtn;

        pauseBtn = GameObject.Find("Pause Button");

        if (pauseCp % 2 == 0)
        {
            pauseBtn.GetComponent<Image>().sprite = pauseDeactivateImage;
            Time.timeScale = 0f;
        }
        else
        {
            pauseBtn.GetComponent<Image>().sprite = pauseActivedImage;
            Time.timeScale = 1f;
        }
        pauseCp++;

    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
        StartCoroutine(GameOverWait());
        QuitGame();
    }

    public static IEnumerator GameOverWait()
    {
        yield return new WaitForSeconds(2f);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        GameManager.instance.score = 0;
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayerShapeShift()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<Renderer>().enabled = false;
            players[i].GetComponent<BoxCollider2D>().enabled = false;

        }

        players[currentPlayer].GetComponent<Renderer>().enabled = true;
        players[currentPlayer].GetComponent<BoxCollider2D>().enabled = true;

        //print("Current shape: " + currentPlayer + " - " + players[currentPlayer].name);

        currentPlayer += 1;
        if (currentPlayer > 2)
            currentPlayer = 0;
    }

    void ResetPlayerPos()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (i != currentPlayer)
            {
               players[i].transform.position = players[currentPlayer].transform.position;
            }
        }
    }
}
