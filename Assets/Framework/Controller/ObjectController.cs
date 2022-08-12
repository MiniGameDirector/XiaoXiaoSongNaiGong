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
    public List<Transform> listMilks = new List<Transform>();
    public Transform bearTrans;
    public Transform leftTarget, rightTarget;
    public GameObject milkGo;
    private int houseIndex = 1;

    private Tweener tweenerTemp = null;
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
        if (Input.GetKeyUp(KeyCode.B))
        {
            SongNai();
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
            CameraToPoint(houseIndex);
            if (houseIndex > 11)
            {
                houseIndex = 1;
            }
            else
            {
                houseIndex++;
            }
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
        Resources.UnloadUnusedAssets();
        if (areaIndex != 0)
        {
            bearTrans.GetComponent<BearController>().SetBearMove(listBearPos[areaIndex].position);
            if (tweenerTemp != null)
            {
                Camera.main.transform.DOKill();
                TweenKillAction(areaIndex == 1 ? 12 : areaIndex - 1);
            }
            tweenerTemp = Camera.main.transform.DOMove(listCameraPos[areaIndex].localPosition, 2f).SetEase(Ease.Linear);
            Camera.main.transform.DORotate(listCameraPos[areaIndex].localEulerAngles, 2f).SetEase(Ease.Linear).OnComplete(delegate ()
            {
                listHouses[areaIndex - 1].GetComponent<Animator>().enabled = true;
                listHouses[areaIndex - 1].GetChild(0).GetComponent<MeshCollider>().enabled = true;
                CreateMilk();
                tweenerTemp = null;
            });
        }
        else
        {
            bearTrans.GetComponent<BearController>().isStart = true;
            bearTrans.GetComponent<BearController>().SetBearMove(listBearPos[areaIndex].position);
            Camera.main.transform.DOMove(listCameraPos[areaIndex].localPosition, 2f).SetEase(Ease.Linear);
            Camera.main.transform.DORotate(listCameraPos[areaIndex].localEulerAngles, 2f).SetEase(Ease.Linear);
        }
    }
    private void TweenKillAction(int _index) {
        Camera.main.transform.position = listCameraPos[_index].localPosition;
        Camera.main.transform.eulerAngles = listCameraPos[_index].localEulerAngles;
        listHouses[_index - 1].GetComponent<Animator>().enabled = true;
        listHouses[_index - 1].GetChild(0).GetComponent<MeshCollider>().enabled = true;
        CreateMilk();
    }
    public void SongNai() {

        AudioController.GetInstance().DisableOther();
        for (int i = 0; i < listMilks.Count; i++)
        {
            listMilks[i].GetComponent<Milk>().SongNai();
        }
        listMilks.Clear();
        StartCoroutine(AudioController.GetInstance().SetAudioClipByName("门铃", false, AudioController.GetInstance().CreateAudio()));
        StartCoroutine(AudioController.GetInstance().SetAudioClipByName("好喝" + (houseIndex % 6 + 1).ToString(), false, AudioController.GetInstance().CreateAudio(),delegate() {
            if (UIManager.GetInstance().isWin)
            {
                UIManager.GetInstance().GameOverEvent(true);
            }
        }));
    }
    public void DisableCollider() {
        for (int i = 0; i < listHouses.Count; i++)
        {
            listHouses[i].GetChild(0).GetComponent<MeshCollider>().enabled = false;
        }
    }
    /// <summary>
    /// 小熊跑过去之后创建milk
    /// </summary>
    public void CreateMilk() {
        if (UIManager.GetInstance().leftBtnClick)
        {
            GameObject go = Instantiate(milkGo);
            go.transform.position = leftTarget.position;
            go.transform.localScale = Vector3.zero;
            go.GetComponent<Milk>().targetPos = listMilkPos[houseIndex == 1 ? 11 : houseIndex - 2].position;
            listMilks.Add(go.transform);
        }
        else
        {
            GameObject go = Instantiate(milkGo);
            go.transform.position = rightTarget.position;
            go.transform.localScale = Vector3.zero;
            go.GetComponent<Milk>().targetPos = listMilkPos[houseIndex == 1 ? 11 : houseIndex - 2].position;
            listMilks.Add(go.transform);
        }
    }
    
}
