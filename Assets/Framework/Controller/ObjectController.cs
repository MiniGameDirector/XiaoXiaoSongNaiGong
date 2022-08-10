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

    public List<Transform> listHouses = new List<Transform>();
    public List<Transform> listCameraPos = new List<Transform>();
    public List<Transform> listBearPos = new List<Transform>();
    public List<Transform> listMilkPos = new List<Transform>();
    public Transform bearTrans;
    public Transform leftMilk, rightMilk;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            GameScene();
        }
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
            CameraToPoint(Random.Range(1, 12));
        }
        else
        {
            Debug.Log("退出游戏");
            //设置相机的位置
            CameraToPoint(0);       
        }
    }
    /// <summary>
    /// 根据相机的点位设置相机的位置和方向
    /// </summary>
    /// <param name="areaIndex">点位的序号</param>
    public void CameraToPoint(int areaIndex) {
        if (areaIndex != 0)
        {
            if (bearTrans.GetComponent<BearController>().SetBearMove(listBearPos[areaIndex].position))
            {
                Camera.main.transform.DOMove(listCameraPos[areaIndex].localPosition, 2f).SetEase(Ease.Linear);
                Camera.main.transform.DORotate(listCameraPos[areaIndex].localEulerAngles, 2f).SetEase(Ease.Linear).OnComplete(delegate() {
                    listHouses[areaIndex - 1].GetComponent<Animator>().enabled = true;
                    if (UIManager.GetInstance().leftBtnClick)
                    {
                        leftMilk.GetComponent<Milk>().targetPos = listMilkPos[areaIndex - 1].position;
                        leftMilk.gameObject.SetActive(true);
                    }
                    else
                    {
                        rightMilk.GetComponent<Milk>().targetPos = listMilkPos[areaIndex - 1].position;
                        rightMilk.gameObject.SetActive(true);
                    }
                    //listHouses[areaIndex - 1].DOLocalRotate(new Vector3(0,0,))
                });
            }
        }
        else
        {
            bearTrans.GetComponent<BearController>().BearStart();
        }
    }
    
}
