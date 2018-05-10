using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickObjectPool
{
    private static BrickObjectPool instance;
    private Vector3 origin = new Vector3(0, 0, 5);
    private GameObject brickObject;
    public Dictionary<BrickType, List<GameObject>> brickPool = new Dictionary<BrickType, List<GameObject>>();

    public static BrickObjectPool Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new BrickObjectPool();
            }
            return instance;
        }
    }

    private void AddBrickToPool(BrickType type, GameObject go)
    {
        if (!brickPool.ContainsKey(type))
        {
            brickPool.Add(type, new List<GameObject>());
        }
        brickPool[type].Add(go);
    }

    /// <summary>
    /// 寻找可使用的Brick
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private GameObject FindUsableBrick(BrickType type)
    {
        if (brickPool.ContainsKey(type))
        {
            for (int i = 0; i < brickPool[type].Count; i++)
            {
                if (!brickPool[type][i].activeSelf)
                    return brickPool[type][i];
            }
        }
        return null;
    }

    public GameObject CreateBrick(BrickType type, GameObject brickGO)
    {
        GameObject go = FindUsableBrick(type);
        if (go != null)
        {
            go.GetComponent<Brick>().BrickType = type;
            //go.transform.position = pos;
            //go.transform.rotation = dir;
        }
        else
        {
            go = GameObject.Instantiate(brickGO/*, pos, dir*/);
            Brick brick = go.AddComponent<Brick>();
            brick.BrickType = type;
            brick.InitBrick();
            AddBrickToPool(type, go);
        }
        return go;
    }

    public void RecycleBrick(GameObject go)
    {
        go.transform.position = origin;
        go.SetActive(false);
    }
}