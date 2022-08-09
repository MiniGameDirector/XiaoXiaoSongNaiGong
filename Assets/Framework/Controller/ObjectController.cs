using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    #region 单例
    private static ObjectController instance;
    public static ObjectController GetInstance() {
        if (instance == null)
        {
            instance = new ObjectController();
        }
        return instance;
    }
    private void Awake()
    {
        instance = this;
    }
    #endregion


    public List<Transform> cameraPos = new List<Transform>();
    public List<Transform> areas = new List<Transform>();
    public Transform nowArea;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
           
    }
    /// <summary>
    /// 进出游戏事件
    /// </summary>
    /// <param name="startGame"></param>
    public void GameScene(bool startGame = true) {
        AudioController.GetInstance().DisableOther();
        if (startGame)
        {
            Debug.Log("进入游戏");
            //设置相机的位置
            //CameraToPoint(1);
        }
        else
        {
            Debug.Log("退出游戏");
            //设置相机的位置
            //CameraToPoint(0);       
        }
    }
    /// <summary>
    /// 根据相机的点位设置相机的位置和方向
    /// </summary>
    /// <param name="areaIndex">点位的序号</param>
    public void CameraToPoint(int areaIndex) {
        Camera.main.transform.DOMove(cameraPos[areaIndex].localPosition, 1f).SetEase(Ease.Linear);
        Camera.main.transform.DORotate(cameraPos[areaIndex].localEulerAngles, 1f).SetEase(Ease.Linear).OnComplete(delegate() {
        });
    }
    
}
