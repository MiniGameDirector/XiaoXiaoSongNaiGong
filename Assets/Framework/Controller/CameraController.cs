using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform milks;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        milks.position = transform.position;
        ////如果鼠标右键按下
        //if (Input.GetMouseButtonDown(0) && UIManager.GetInstance().canChangeScene)
        //{
        //    float speed = 2.5f;//旋转跟随速度
        //    float OffsetX = Input.GetAxis("Mouse X");//获取鼠标x轴的偏移量
        //    float OffsetY = Input.GetAxis("Mouse Y");//获取鼠标y轴的偏移量
        //    transform.Rotate(new Vector3(OffsetY, -OffsetX, 0) * speed, Space.World);//旋转物体
        //}
    }
}
