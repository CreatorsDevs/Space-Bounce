using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(50)]

public class GameManager : MonoBehaviour
{
    // PUBLIC VALUES
    public GameObject[] obstaclePrefab;
    public GameObject player;
    public int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI exitAgainText;
    public GameObject instructionsPanelUI; 
    public GameObject playPanelUI;
    public GameObject restartPanelUI;  
    public GameObject scoreUI;
    public GameObject yesButton;
    public GameObject noButton;
    public GameObject quitPanelUI;
    public GameObject resetButton;
    public GameObject SettingsPanelUI;
    public bool playingGame = false;
    

    // PRIVATE VALUES
    private PlayerController playerControllerScript;
    private bool tryingToExit;
    private Vector3 capitalizerSpawnPos;
    private Vector3 obstacleSpawnPos;
    private float spawnrate = 1.0f;


    // GAMEOBJECTS   
    GameObject Capitalizer;
    GameObject Obstacle;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 240;
        playerControllerScript = GameObject.FindObjectOfType<PlayerController>();
        score = 0;
        UpdateScore(0);
    }   

    // Update is called once per frame
    void Update()
    {       
        float centerPoint = ScalingScript.width / 2;
        float min = -centerPoint;
        float max = centerPoint;
        float capitalizerRandomPos = (ScalingScript.height + Random.Range(8.0f,10.0f));
        float obstacleRandomPos = (ScalingScript.height + Random.Range(12.0f,14.0f));
        while(capitalizerRandomPos == obstacleRandomPos)
            capitalizerRandomPos = (ScalingScript.height + Random.Range(5.0f,8.0f));
        capitalizerSpawnPos = new Vector3(Random.Range(min+0.5f, max-0.5f), capitalizerRandomPos , 0);
        obstacleSpawnPos = new Vector3(Random.Range(min+0.5f, max-0.5f), obstacleRandomPos, 0);

        bestScoreText.text = PlayerPrefs.GetInt("BestScore", 0).ToString(); //Getting best score

       if (Application.platform == RuntimePlatform.Android) 
        {
            if (Input.GetKey(KeyCode.Escape)) // If User presses back button give hint to quit the application.
            {
                if(!tryingToExit)
                {
                    tryingToExit = true;
                    ExitAgainFunction();
                }
                else  // If User presses back button again then quit the application.
                {
                    Application.Quit();
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }
        
    }

    IEnumerator SpawnObject()
    {
        while(!PlayerController.gameOver)
        {
            yield return new WaitForSeconds(spawnrate);
            if(PlayerController.isGameActive)
            {
                Capitalizer = ObjectPool.SharedInstance.GetCapitalizedObject();
                Obstacle = ObjectPool.SharedInstance.GetObstacleObject();
                if(Capitalizer != null)
                {
                    Capitalizer.transform.position = capitalizerSpawnPos;
                    Capitalizer.transform.rotation = obstaclePrefab[0].transform.rotation;
                    Capitalizer.SetActive(true);
                }
                if(Obstacle != null)
                {
                    Obstacle.transform.position = obstacleSpawnPos;
                    Obstacle.transform.rotation = obstaclePrefab[1].transform.rotation;
                    Obstacle.SetActive(true);
                }
            }
        }
    }

    public void PlayGame()
    {
        playingGame = true;
        PlayerController.isGameActive = true;
        playerControllerScript.isScreenPressed = true;
        playPanelUI.gameObject.SetActive(false);

        instructionsPanelUI.gameObject.SetActive(true);
        StartCoroutine(InstructionPanelTimer());
        StartCoroutine(SpawnObject()); // for object pooling
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = score.ToString();
        currentScoreText.text = score.ToString();

        if(score > PlayerPrefs.GetInt("BestScore", 0))
        {
             PlayerPrefs.SetInt("BestScore", score); // Setting best score
        }
    }

    public void GameOver()
    {
        PlayerController.isGameActive = false;
        PlayerController.gameOver = true;
        restartPanelUI.gameObject.SetActive(true);
        scoreUI.gameObject.SetActive(false);
        StopCoroutine(SpawnObject());
        StopCoroutine(ScoreTextAnimation());
    }

    public void Reset() 
    {
        PlayerPrefs.DeleteAll();
    }

    public void NoButton()
    {
        quitPanelUI.gameObject.SetActive(false);
        restartPanelUI.gameObject.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void QuitButton()
    {
        quitPanelUI.gameObject.SetActive(true);
        restartPanelUI.gameObject.SetActive(false);
    }

    public void AnimateScoreText()
    {
        StartCoroutine(ScoreTextAnimation());
    }

    IEnumerator ScoreTextAnimation()
    {
        scoreUI.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        scoreUI.gameObject.SetActive(false);
        StopCoroutine(ScoreTextAnimation());
    }

    public void ExitAgainFunction()
    {
        StartCoroutine(ExitAgainIEnumerator());
    }

    IEnumerator ExitAgainIEnumerator()
    {
            exitAgainText.gameObject.SetActive(true);

                yield return new WaitForSeconds(2.0f);

            exitAgainText.gameObject.SetActive(false);
            tryingToExit = false;

        StopCoroutine(ExitAgainIEnumerator());
    }

    public void PlayButtonSound()
    {
        //FindObjectOfType<AudioManager>().Play("Button");
        AudioManager.instance.Play("Button");
    }

    public void SettingsOpen()
    {
        SettingsPanelUI.gameObject.SetActive(true);
    }

    public void SettingsClose()
    {
        SettingsPanelUI.gameObject.SetActive(false);
    }

    IEnumerator InstructionPanelTimer()
    {
        yield return new WaitForSeconds(1.5f);
        instructionsPanelUI.gameObject.SetActive(false);
        StopCoroutine(InstructionPanelTimer());
    }

}
