using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowBrick
{
    private int rowNum = 5;
    private int columnNum = 10;
    private int nextStep = 3;
    public List<List<GameObject>> bricktWallList = new List<List<GameObject>>(10);
    private Vector3 originBrickPos = new Vector3(-10, 0, 0);
    private static RowBrick instance;
    private GameObject wall;

    public void Init()
    {
        wall = GameManager.Instance.wall;
    }

    public static RowBrick Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new RowBrick();
            }
            return instance;
        }
    }

    /// <summary>
    /// 生成墙面
    /// </summary>
    public void CreateWall(GameObject brickObject)
    {
        for (int i = 0; i < rowNum; i++)
        {
            GameObject rowGO = new GameObject();
            rowGO.name = "Row " + i.ToString();
            rowGO.transform.parent = wall.transform;
            CreateARowBrick(brickObject, rowGO.transform);
            MoveUpWall();
        }
    }

    private void MoveUpWall()
    {
        wall.transform.position = new Vector3(wall.transform.position.x, wall.transform.position.y + nextStep, wall.transform.position.z);
    }

    /// <summary>
    /// 生成一排Brick
    /// </summary>
    public void CreateARowBrick(GameObject brickObject, Transform parent)
    {
        List<GameObject> brickRowList = new List<GameObject>(5);
        for (int i = 0; i < columnNum; i++)
        {
            BrickType type;
            type = i == 0 ? BrickType.Undefined : brickRowList[i - 1].GetComponent<Brick>().BrickType;
            type = SetBrickType(type);

            Vector3 rowBrickPos = Vector3.zero;
            GameObject go = BrickObjectPool.Instance.CreateBrick(type, brickObject);
            go.transform.parent = parent;
            brickRowList.Add(go);
            rowBrickPos = SetBrickPosition(brickRowList, i);
            go.transform.position = rowBrickPos;
        }
        bricktWallList.Add(brickRowList);
        MoveUpWall();
    }

    /// <summary>
    /// 设置每个Brick的Type类型，防止出现全为Bad Brick
    /// </summary>
    /// <param name="badNum"></param>
    /// <param name="rowBrickNum"></param>
    private BrickType SetBrickType(BrickType lastType)
    {
        BrickType type = BrickType.Undefined;
        if (lastType == BrickType.Bad)
        {
            do
            {
                type = (BrickType)Random.Range(0, BrickType.GetValues(typeof(BrickType)).Length - 1);
            } while (type == BrickType.Bad);
        }
        else
            type = (BrickType)Random.Range(0, BrickType.GetValues(typeof(BrickType)).Length - 1);
        return type;
    }

    /// <summary>
    /// 设置Brick的位置
    /// </summary>
    private Vector3 SetBrickPosition(List<GameObject> list,/* out Vector3 rowBrickPos,*/ int index)
    {
        Vector3 rowBrickPos;
        if (bricktWallList.Count == 0)
        {
            rowBrickPos = originBrickPos;
        }
        else
        {
            rowBrickPos = bricktWallList[bricktWallList.Count - 1][0].transform.position;
            rowBrickPos = new Vector3(rowBrickPos.x, rowBrickPos.y - nextStep, rowBrickPos.z);//向下偏移距离
        }
        if (list.Count > 1)
        {
            rowBrickPos = list[index - 1].transform.position;
            float lastBrickWidth = list[index - 1].GetComponent<Brick>().Width / 2f;
            float currentBrickWidth = list[index].GetComponent<Brick>().Width / 2f;
            float posX = rowBrickPos.x + lastBrickWidth + currentBrickWidth;
            rowBrickPos = new Vector3(posX, rowBrickPos.y, rowBrickPos.z);
        }
        return rowBrickPos;
    }

    public void DestroyRowBrick(List<GameObject> list)
    {
        foreach (var item in list)
        {
            BrickObjectPool.Instance.RecycleBrick(item);
        }
    }
}