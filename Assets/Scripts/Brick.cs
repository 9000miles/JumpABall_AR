using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField]
    private float width;

    public float Width { get; set; }

    [SerializeField]
    private BrickType brickType;

    private float maxWidth = 3f;
    private float minWidth = 1f;

    public BrickType BrickType
    {
        get { return brickType; }
        set { brickType = value; }
    }

    //private void Awake()
    //{
    //    InitBrick();
    //}

    public void InitBrick()
    {
        GetComponent<Collider>().isTrigger = true;
        SetBrickWidth();
        switch (brickType)
        {
            case BrickType.Entiy:
                GetComponent<Renderer>().material.color = Color.black;
                transform.tag = BrickType.Entiy.ToString();
                break;

            case BrickType.Hollow:
                GetComponent<Renderer>().enabled = false;
                //GetComponent<Renderer>().material.color = Color.white;
                transform.tag = BrickType.Hollow.ToString();
                break;

            case BrickType.Bad:
                GetComponent<Renderer>().material.color = Color.red;
                transform.tag = BrickType.Bad.ToString();
                break;

            case BrickType.Undefined:
                GetComponent<Renderer>().material.color = Color.yellow;
                transform.tag = BrickType.Undefined.ToString();
                break;

            default:
                break;
        }
    }

    private void SetBrickWidth()
    {
        Width = UnityEngine.Random.Range(minWidth, maxWidth);
        transform.localScale = new Vector3(transform.localScale.x * Width, transform.localScale.y, transform.localScale.z);
    }
}