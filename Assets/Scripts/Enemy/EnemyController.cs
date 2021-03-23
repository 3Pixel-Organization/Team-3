using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private List<Enemy> Enemies;
    public Enemy EnemyPrefab;
    public List<Material> materials;

    private System.Random random = new System.Random();
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
        EventBroker.InitiateGame += InitGame;
        EventBroker.DeleteEnemy += DeleteEnemy;
    }

    void OnDestroy()
    {
        EventBroker.InitiateGame -= InitGame;
        EventBroker.DeleteEnemy -= DeleteEnemy;
    }

    void InitGame(int countEnemies) => SpawnEnemies(countEnemies, true);

    void SpawnEnemy(Vector3 position)
    {
        Enemy enemy = Instantiate(EnemyPrefab, position, Quaternion.identity);
        enemy.gameObject.GetComponent<MeshRenderer>().material = materials[random.Next(materials.Count)];
        enemy.gameObject.tag = "Enemy";
        Enemies.Add(enemy);
    }

    void SpawnEnemies(int countEnemies, bool purge = false)
    {
        if (purge)
        {
            Enemies.ForEach(x => Destroy(x.gameObject));
            Enemies.Clear();
        }

        for (int i = 0; i < countEnemies; i++)
        {
            // Basic Enemy Spawn Range: -50:50 (x and z axis); y = 0.5
            var startingPosition = new Vector3(Random.Range(-10, 10), 0.5f, Random.Range(-10, 10));
            SpawnEnemy(startingPosition);
        }
    }

    void DeleteEnemy(Enemy enemy)
    {
        Enemies.Remove(enemy);
        if (Enemies.Count == 0)
        {
            EventBroker.CallInitiateGame(5);
        }
    }

}
