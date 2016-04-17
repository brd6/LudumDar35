using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

    [SerializeField]
    private Text highScoreText;

    void Start()
    {
        highScoreText.text = GameManager.instance.hightScore.ToString();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            QuitGame();
    }
}
