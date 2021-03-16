using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private List<Enemy> Enemies;
    public Enemy EnemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Enemies = new List<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        EventBroker.InitiateGame += SpawnEnemies;
    }

    void OnDestroy()
    {
        EventBroker.InitiateGame -= SpawnEnemies;
    }

    void SpawnEnemy(Vector3 position)
    {
        Enemy enemy = Instantiate(EnemyPrefab, position, Quaternion.identity);
        Enemies.Add(enemy);
    }

    //
    void SpawnEnemies(int countEnemies)
    {
        for(int i = 0; i < countEnemies; i++)
        {
            // Basic Enemy Spawn Range: -50:50 (x and z axis); y = 0.5
            var startingPosition = new Vector3(Random.Range(-50, 51), 0.5f, Random.Range(-50, 51));
            SpawnEnemy(startingPosition);
        }
    }

}
