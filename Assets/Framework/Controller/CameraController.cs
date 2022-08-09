using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 相机位置和方向移动
    /// </summary>
    /// <param name="_target"></param>
    public void MoveToTarget(Transform _target, Transform objPa) {
        transform.DOMove(_target.localPosition, 1f).SetEase(Ease.Linear);
        transform.DORotate(_target.localEulerAngles, 1f).SetEase(Ease.Linear).OnComplete(delegate() {
            if (objPa != null)
            {
                for (int i = 0; i < objPa.childCount; i++)
                {
                    objPa.GetChild(i).gameObject.SetActive(true);
                }
            }
        });
    }
}
