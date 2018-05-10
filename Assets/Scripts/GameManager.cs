using MyCommonTools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public GameObject brickObject;
    public int Score { get; set; }

    private void Start()
    {
        if (brickObject != null)
            RowBrick.Instance.CreateWall(brickObject);
    }

    private void Update()
    {
    }

    private void Puse()
    {
    }

    private void GameOver()
    {
    }
}