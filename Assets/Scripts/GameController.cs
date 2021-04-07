using UnityEngine;

public class GameController : MonoBehaviour
{
    public int InitialEnemyCount = 5;
    public GameObject menu;


    // Start is called before the first frame update
    void Start()
    {
        InitiateGame();
    }

    private void Awake()
    {
        EventBroker.PauseGame += PauseGame;
        EventBroker.ResumeGame += ResumeGame;
    }

    private void OnDisable()
    {
        EventBroker.PauseGame -= PauseGame;
        EventBroker.ResumeGame -= ResumeGame;
    }

    // Update is called once per frame
    private void Update()
    {
        Debug.Log("Number of enemies: " + InitialEnemyCount.ToString());

        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            EventBroker.CallPauseGame();
        }


    }

    private void InitiateGame()
    {
        Cursor.visible = false;
        EventBroker.CallInitiateGame(InitialEnemyCount);
    }

    private void PauseGame()
    {
        menu.SetActive(true);
        Cursor.visible = true;
        Debug.Log("Game Paused!");
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void ResumeGame()
    {
        Debug.Log("Game Resumed!");
        Cursor.visible = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }



}
