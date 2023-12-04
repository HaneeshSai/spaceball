using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    public Transform PlayerPosition;
    public Transform RayPos;
    bool spawned = false;
    public LayerMask targetLayer;
    public int NodeIndex;
    int CurrentRandomIndex;
    List<MazeNode> path = new List<MazeNode>();

    void Start()
    {

    }

    void Update()
    {
        if (GameManager.Instance.enemyStart == true && spawned == false)
        {
            CurrentRandomIndex = Random.Range(0, GameManager.Instance.nodes.Count);
            Vector3 spawnPos = GameManager.Instance.nodes[CurrentRandomIndex].transform.position;
            transform.position = spawnPos;
            spawned = true;

            StartSearch(GameManager.Instance.nodes[CurrentRandomIndex]);
        }
    }

    void StartSearch(MazeNode currentNode)
    {
        List<MazeNode> path = findPath(GameManager.Instance.nodes[CurrentRandomIndex]);
        StartCoroutine(MoveTowards(path));

        // switch (choseMove)
        // {
        //     case 1:
        //         StartCoroutine(RotateTowards(Vector3.forward * 0, 3f));
        //         StartCoroutine(MoveTowards(GameManager.Instance.nodes[CurrentRandomIndex + (int)Mathf.Sqrt(GameManager.Instance.nodes.Count)]));
        //         break;
        //     case 2:
        //         StartCoroutine(RotateTowards(Vector3.forward * 180, 3f));
        //         StartCoroutine(MoveTowards(GameManager.Instance.nodes[CurrentRandomIndex - (int)Mathf.Sqrt(GameManager.Instance.nodes.Count)]));
        //         break;
        //     case 3:
        //         StartCoroutine(RotateTowards(Vector3.forward * 90, 3f));
        //         StartCoroutine(MoveTowards(GameManager.Instance.nodes[CurrentRandomIndex + 1]));
        //         break;
        //     case 4:
        //         StartCoroutine(RotateTowards(Vector3.forward * -90, 3f));
        //         StartCoroutine(MoveTowards(GameManager.Instance.nodes[CurrentRandomIndex - 1]));
        //         break;
        // }
    }


    List<MazeNode> findPath(MazeNode currentNode)
    {
        float playerPosCell = 0;
        float minDistance = float.MaxValue;
        for (int i = 0; i < GameManager.Instance.nodes.Count; i++)
        {
            float distance = Vector3.Distance(PlayerPosition.position, GameManager.Instance.nodes[i].transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                playerPosCell = i;
            }
        }
        List<MazeNode> VistedNodes = new List<MazeNode>();
        List<MazeNode> Completed = new List<MazeNode>();
        VistedNodes.Add(currentNode);
        while (!VistedNodes.Contains(GameManager.Instance.nodes[(int)playerPosCell]))
        {
            CurrentRandomIndex = GameManager.Instance.nodes.IndexOf(VistedNodes[VistedNodes.Count - 1]);
            List<int> possibleMoves = new List<int>();
            if (!VistedNodes[VistedNodes.Count - 1].walls[0].activeSelf && !VistedNodes.Contains(GameManager.Instance.nodes[CurrentRandomIndex + (int)(Mathf.Sqrt(GameManager.Instance.nodes.Count))]) && !Completed.Contains(GameManager.Instance.nodes[CurrentRandomIndex + (int)(Mathf.Sqrt(GameManager.Instance.nodes.Count))]))
            {
                possibleMoves.Add(CurrentRandomIndex + (int)(Mathf.Sqrt(GameManager.Instance.nodes.Count)));
            }
            if (!VistedNodes[VistedNodes.Count - 1].walls[1].activeSelf && !VistedNodes.Contains(GameManager.Instance.nodes[CurrentRandomIndex - (int)(Mathf.Sqrt(GameManager.Instance.nodes.Count))]) && !Completed.Contains(GameManager.Instance.nodes[CurrentRandomIndex - (int)(Mathf.Sqrt(GameManager.Instance.nodes.Count))]))
            {
                possibleMoves.Add(CurrentRandomIndex - (int)(Mathf.Sqrt(GameManager.Instance.nodes.Count)));
            }
            if (!VistedNodes[VistedNodes.Count - 1].walls[2].activeSelf && !VistedNodes.Contains(GameManager.Instance.nodes[CurrentRandomIndex + 1]) && !Completed.Contains(GameManager.Instance.nodes[CurrentRandomIndex + 1]))
            {
                possibleMoves.Add(CurrentRandomIndex + 1);
            }
            if (!VistedNodes[VistedNodes.Count - 1].walls[3].activeSelf && !VistedNodes.Contains(GameManager.Instance.nodes[CurrentRandomIndex - 1]) && !Completed.Contains(GameManager.Instance.nodes[CurrentRandomIndex - 1]))
            {
                possibleMoves.Add(CurrentRandomIndex - 1);
            }
            if (possibleMoves.Count > 0)
            {
                VistedNodes.Add(GameManager.Instance.nodes[possibleMoves[Random.Range(0, possibleMoves.Count)]]);
                // VistedNodes[VistedNodes.Count - 1].SetState(NodeState.CorrectPath);
            }
            else
            {
                Completed.Add(VistedNodes[VistedNodes.Count - 1]);
                // VistedNodes[VistedNodes.Count - 1].SetState(NodeState.WrongPath);
                VistedNodes.RemoveAt(VistedNodes.Count - 1);
            }
        }
        return VistedNodes;
    }


    IEnumerator RotateTowards(Vector3 Direction, float time)
    {
        float elapsedTime = 0f;
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(Direction);

        while (elapsedTime < time)
        {
            // Interpolate the rotation over time using Slerp
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / time);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.rotation = targetRotation;
    }

    IEnumerator MoveTowards(List<MazeNode> path)
    {
        Renderer renderer = GetComponent<Renderer>();

        while (!renderer.isVisible)
        {

        }
    }


}
