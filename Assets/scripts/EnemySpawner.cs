using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform PlayerPos;
    public float timer = 0f;
    public int round = 1;
    [SerializeField] MazeGenerator MazeGanerator;
    [SerializeField] enemyController enemy;

    void Start()
    {

    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > round * 10 && GameManager.Instance.EnemyCount < 6)
        {
            timer = 0;
            round += 1;
            for (int i = 0; i < round * 5; i++)
            {
                int RandomIndex = Random.Range(0, GameManager.Instance.nodes.Count);
                Vector3 randomPos = GameManager.Instance.nodes[RandomIndex].transform.position;
                Instantiate(enemyPrefab, randomPos, Quaternion.identity);
                GameManager.Instance.EnemyCount++;
                enemy.NodeIndex = RandomIndex;
            }
        }
    }
}
