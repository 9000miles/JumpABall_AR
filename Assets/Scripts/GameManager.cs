using MyCommonTools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public GameObject brickObject;

    public GameObject wall;
    public int Score { get; set; }

    private void Start()
    {
        wall = new GameObject();
        wall.name = "Wall";
        if (brickObject != null)
        {
            RowBrick.Instance.Init();
            RowBrick.Instance.CreateWall(brickObject);
        }
    }

    private void Update()
    {
    }

    private void OnGUI()
    {
        if (GUILayout.Button("xiaohuo"))
        {
            RowBrick.Instance.DestroyRowBrick(RowBrick.Instance.bricktWallList[0]);
            RowBrick.Instance.bricktWallList.RemoveAt(0);
        }
        if (GUILayout.Button("shengcheng"))
        {
            GameObject go = new GameObject("NewRow");
            go.transform.parent = wall.transform;
            RowBrick.Instance.CreateARowBrick(brickObject, go.transform);
        }
    }

    private void Puse()
    {
    }

    private void GameOver()
    {
    }

    private void QuitGame()
    {
    }
}