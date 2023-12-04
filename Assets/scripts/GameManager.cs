using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int EnemyCount;
    public static GameManager Instance;
    [SerializeField] MazeNode Node;
    public List<MazeNode> nodes = new List<MazeNode>();
    public bool enemyStart = false;

    // Add your GameManager-specific variables and methods here

    private void Awake()
    {
        // Ensure there's only one instance of GameManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
