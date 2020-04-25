using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntitySpawner : MonoBehaviour
{
    private static GameObject[] enemySpawnPoints;
    [SerializeField]
    public Enemy testEnemy;
    static Enemy _testEnemy;
    static GameObject enemies;
    private void Awake()
    {
        _testEnemy = testEnemy;
    }
    public static void SpawnEnemy(Enemy enemy, Vector3 position)
    {
        GameObject go = Instantiate(enemy.prefab, position, Quaternion.identity);
        go.AddComponent<EnemyConnector>().enemy = Instantiate(enemy);
        SceneManager.MoveGameObjectToScene(go, SceneManager.GetSceneAt(1));
    }
    public static void SpawnAllEnemies() {
        enemySpawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawnPoint");
        int randInt;
        print("SpawningEnemies... " + enemySpawnPoints.Length);
        for(int i = 0; i < enemySpawnPoints.Length; i++)
        {
            randInt = Random.Range(0, 100);
            if(randInt < 60)
            {
                for(int c = 1; c <= Random.Range(3, 7); c++)
                {
                    SpawnEnemy(_testEnemy, enemySpawnPoints[i].transform.position + enemySpawnPoints[i].transform.forward / c);
                }
            }
        }
    }
}
