using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField] MazeNode Node;
    [SerializeField] Vector2Int size;
    //public List<MazeNode> GameManager.Instance.nodes = new List<MazeNode>();
    [SerializeField] List<MazeNode> CurrentPath = new List<MazeNode>();
    [SerializeField] List<MazeNode> CompletedNodes = new List<MazeNode>();
    [SerializeField] MazeNode CurrentNode;
    [SerializeField] MazeNode NextNode;



    void Start()
    {
        generateAllNodes(size);
    }

    void generateAllNodes(Vector2Int size)
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                Vector3 nodePos = new Vector3((i * 6.4f) - (size.x / 2f), (j * 6.4f) - (size.y / 2f), 0);
                MazeNode newNode = Instantiate(Node, nodePos, Quaternion.identity, transform);
                GameManager.Instance.nodes.Add(newNode);
            }
        }
        CurrentPath.Add(GameManager.Instance.nodes[size.x - 1]);
        GameManager.Instance.nodes[size.x - 1].EntryExit(1);
        int[] corners = { 0, size.x * size.x - 1, size.x * size.x - size.x };
        int randomCorner = corners[Random.Range(0, 3)];

        if (randomCorner == 0)
        {
            GameManager.Instance.nodes[0].EntryExit(1);
        }
        else
        {
            GameManager.Instance.nodes[corners[2]].EntryExit(0);
        }

        // CurrentPath[0].SetState(NodeState.Current);
        CurrentNode = CurrentPath[CurrentPath.Count - 1];

        while (CompletedNodes.Count < GameManager.Instance.nodes.Count)
        {
            CurrentNode = CurrentPath[CurrentPath.Count - 1];
            int currentNodeIndex = GameManager.Instance.nodes.IndexOf(CurrentPath[CurrentPath.Count - 1]);
            Vector2Int currentIndexXY = new Vector2Int(currentNodeIndex / size.x, currentNodeIndex % size.x);

            List<int> possibleDirections = new List<int>();
            List<int> possibleNextNodes = new List<int>();

            //everynode other than the left right wall GameManager.Instance.nodes
            if (currentIndexXY.x < size.x - 1)
            {
                //checking the right node of current node
                if (!CurrentPath.Contains(GameManager.Instance.nodes[currentNodeIndex + size.x]) && !CompletedNodes.Contains(GameManager.Instance.nodes[currentNodeIndex + size.x]))
                {
                    possibleDirections.Add(0);
                    possibleNextNodes.Add(currentNodeIndex + size.x);
                }
            }

            if (currentIndexXY.x > 0)
            {
                //checking the left node of current node
                if (!CurrentPath.Contains(GameManager.Instance.nodes[currentNodeIndex - size.x]) && !CompletedNodes.Contains(GameManager.Instance.nodes[currentNodeIndex - size.x]))
                {
                    possibleDirections.Add(1);
                    possibleNextNodes.Add(currentNodeIndex - size.x);
                }
            }

            if (currentIndexXY.y > 0)
            {
                //checking the left node of current node
                if (!CurrentPath.Contains(GameManager.Instance.nodes[currentNodeIndex - 1]) && !CompletedNodes.Contains(GameManager.Instance.nodes[currentNodeIndex - 1]))
                {
                    possibleDirections.Add(2);
                    possibleNextNodes.Add(currentNodeIndex - 1);
                }
            }
            if (currentIndexXY.y < size.x - 1)
            {
                //checking the left node of current node
                if (!CurrentPath.Contains(GameManager.Instance.nodes[currentNodeIndex + 1]) && !CompletedNodes.Contains(GameManager.Instance.nodes[currentNodeIndex + 1]))
                {
                    possibleDirections.Add(3);
                    possibleNextNodes.Add(currentNodeIndex + 1);
                }
            }

            if (possibleDirections.Count > 0)
            {
                int chosenDirection = Random.Range(0, possibleDirections.Count);
                MazeNode chosenNode = GameManager.Instance.nodes[possibleNextNodes[chosenDirection]];

                switch (possibleDirections[chosenDirection])
                {
                    case 0:
                        chosenNode.RemoveWall(1);
                        CurrentPath[CurrentPath.Count - 1].RemoveWall(0);
                        break;

                    case 1:
                        chosenNode.RemoveWall(0);
                        CurrentPath[CurrentPath.Count - 1].RemoveWall(1);
                        break;

                    case 2:
                        chosenNode.RemoveWall(2);
                        CurrentPath[CurrentPath.Count - 1].RemoveWall(3);
                        break;

                    case 3:
                        chosenNode.RemoveWall(3);
                        CurrentPath[CurrentPath.Count - 1].RemoveWall(2);
                        break;
                }

                CurrentPath.Add(chosenNode);
                // chosenNode.SetState(NodeState.Current);
            }
            else
            {
                CompletedNodes.Add(CurrentPath[CurrentPath.Count - 1]);
                //CurrentPath[CurrentPath.Count - 1].SetState(NodeState.Completed);
                CurrentPath.RemoveAt(CurrentPath.Count - 1);
            }
        }
        GameManager.Instance.enemyStart = true;
    }
}
