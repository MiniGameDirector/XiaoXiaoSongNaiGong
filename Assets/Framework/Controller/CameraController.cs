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
        ////�������Ҽ�����
        //if (Input.GetMouseButtonDown(0) && UIManager.GetInstance().canChangeScene)
        //{
        //    float speed = 2.5f;//��ת�����ٶ�
        //    float OffsetX = Input.GetAxis("Mouse X");//��ȡ���x���ƫ����
        //    float OffsetY = Input.GetAxis("Mouse Y");//��ȡ���y���ƫ����
        //    transform.Rotate(new Vector3(OffsetY, -OffsetX, 0) * speed, Space.World);//��ת����
        //}
    }
}
