using UnityEngine;

public class GameController : MonoBehaviour
{
    public int InitialEnemyCount = 5;

    // Start is called before the first frame update
    void Start()
    {
        InitiateGame();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Number of enemies: " + InitialEnemyCount.ToString());
    }

    void InitiateGame()
    {
        EventBroker.CallInitiateGame(InitialEnemyCount);
    }
}
