using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeState
{
    Available,
    Current,
    Completed,
    CorrectPath,
    WrongPath,
}

public class MazeNode : MonoBehaviour
{
    // Start is called before the first frame update
    // RIGHT, LEFT, TOP, DOWN
    public GameObject[] walls;
    public SpriteRenderer floor;
    public List<GameObject> DestroyedWalls = new List<GameObject>();
    private Collider2D myCollider;
    private SpriteRenderer myRenderer;

    public void RemoveWall(int wallToRemove)
    {
        DestroyedWalls.Add(walls[wallToRemove]);
        walls[wallToRemove].SetActive(false);
    }

    public void EntryExit(int wallIndex)
    {
        SpriteRenderer spriteRenderer = walls[wallIndex].GetComponent<SpriteRenderer>();
        Collider2D collider2d = walls[wallIndex].GetComponent<Collider2D>();

        spriteRenderer.enabled = false;
        collider2d.enabled = false;
    }

    public void SetState(NodeState state)
    {
        switch (state)
        {
            case NodeState.Available:
                floor.color = Color.white;
                break;

            case NodeState.Current:
                floor.color = Color.yellow;
                break;

            case NodeState.Completed:
                floor.color = Color.blue;
                break;

            case NodeState.CorrectPath:
                floor.color = Color.green;
                break;

            case NodeState.WrongPath:
                floor.color = Color.red;
                break;

        }
    }
}

