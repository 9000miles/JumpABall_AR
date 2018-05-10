using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyCommonTools;

public class Ball : MonoSingleton<Ball>
{
    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 pos;
    private bool isArrived;
    private float upSpeed = 0.18f;
    private float downSpeed = 0.28f;
    private float speed;

    private void Start()
    {
        startPos = transform.position;
        endPos = startPos + new Vector3(0, 4, 0);
        pos = endPos;
        speed = upSpeed;
    }

    // Update is called once per frame
    private void Update()
    {
        JumpUP();
    }

    private void JumpUP()
    {
        if (Vector3.Distance(transform.position, pos) > 0.05f)
        {
            transform.position = Vector3.Lerp(transform.position, pos, speed);
        }
        else
        {
            isArrived = !isArrived;
            pos = isArrived ? startPos : endPos;
            speed = isArrived ? downSpeed : upSpeed;
        }
    }
}