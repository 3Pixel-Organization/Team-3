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
        
    }

    void InitiateGame()
    {
        EventBroker.CallInitiateGame(InitialEnemyCount);
    }
}
