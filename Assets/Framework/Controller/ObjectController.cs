using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    #region ����
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
    private int houseIndex = 1;
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
    /// ������Ϸ�¼�
    /// </summary>
    /// <param name="startGame"></param>
    public void GameScene(bool startGame = true) {
        AudioController.GetInstance().DisableOther();
        if (startGame)
        {
            Debug.Log("������Ϸ");
            //���������λ��
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
            Debug.Log("�˳���Ϸ");
            //���������λ��
            CameraToPoint(0);       
        }
    }
    /// <summary>
    /// ��������ĵ�λ���������λ�úͷ���
    /// </summary>
    /// <param name="areaIndex">��λ�����</param>
    public void CameraToPoint(int areaIndex) {
        if (areaIndex != 0)
        {
            if (bearTrans.GetComponent<BearController>().SetBearMove(listBearPos[areaIndex].position))
            {
                Camera.main.transform.DOMove(listCameraPos[areaIndex].localPosition, 2f).SetEase(Ease.Linear);
                Camera.main.transform.DORotate(listCameraPos[areaIndex].localEulerAngles, 2f).SetEase(Ease.Linear).OnComplete(delegate() {
                    listHouses[areaIndex - 1].GetComponent<Animator>().enabled = true;
                    listHouses[areaIndex - 1].GetChild(0).GetComponent<MeshCollider>().enabled = true;
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
            Camera.main.transform.DOMove(listCameraPos[areaIndex].localPosition, 2f).SetEase(Ease.Linear);
            Camera.main.transform.DORotate(listCameraPos[areaIndex].localEulerAngles, 2f).SetEase(Ease.Linear).OnComplete(delegate ()
            {
                bearTrans.GetComponent<BearController>().BearStart();
            });
        }
    }
    public void SongNai() {

        AudioController.GetInstance().DisableOther();
        if (UIManager.GetInstance().leftBtnClick)
        {
            leftMilk.GetComponent<Milk>().SongNai();
        }
        else
        {
            rightMilk.GetComponent<Milk>().SongNai();
        }
        StartCoroutine(AudioController.GetInstance().SetAudioClipByName("����", false, AudioController.GetInstance().CreateAudio()));
        StartCoroutine(AudioController.GetInstance().SetAudioClipByName("�ú�" + (houseIndex % 6 + 1).ToString(), false, AudioController.GetInstance().CreateAudio(),delegate() {
            if (UIManager.GetInstance().isWin)
            {
                UIManager.GetInstance().GameOverEvent(true);
            }
        }));
    }
    
}
