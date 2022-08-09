using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region ����
    private static UIManager instance;
    public static UIManager GetInstance() {
        if (instance == null)
        {
            instance = new UIManager();
        }
        return instance;
    }
    private void Awake()
    {
        instance = this;
    }
    #endregion

    public Transform uiCanvas;

    public Button btnGameOver, btnRealOver, btnReturn;
    //�����л���ť
    public Button btnStartScene, btnGameScene;

    public Button btnStartGame, btnTurn;
    public Transform imgStart;

    public Transform initObj;
    public Transform panelRecord;
    public Transform panelOver;
    public Text txtJifenBan1, txtJifenBan2;
    public bool canChangeScene = false;
    private bool isWin = false;
    /// <summary>
    /// ��ʼ���¼�
    /// </summary>
    private void InitButtonEvent() {
        //��ʼ��Ϸ
        btnStartGame.onClick.AddListener(StartScene);
        //����Ŀ¼
        btnTurn.onClick.AddListener(delegate () { Application.Quit(); });
        //��Ϸ����
        btnGameOver.onClick.AddListener(delegate() {
            if (canChangeScene)
            {
                GameOverEvent(false);
            }
        });
        //��Ϸ������ť
        btnStartScene.onClick.AddListener(delegate ()
        {
            StartScene();
        });
        //��Ϸ������ť
        btnGameScene.onClick.AddListener(delegate ()
        {
            if (canChangeScene)
            {
                GameScene();
            }
        });
        //�����˳���Ϸ��ť
        btnRealOver.onClick.AddListener(delegate () {
            StartCoroutine(AudioController.GetInstance().SetAudioClipByName("��������", false, null, GameOverRealEvent));
            AudioController.GetInstance().DisableOther();
        });
        //�ָ���Ϸ
        btnReturn.onClick.AddListener(delegate ()
        {
            AudioController.GetInstance().DisableOther();
            if (isWin)
            {
                WinGameReturn();
            }
            panelOver.gameObject.SetActive(false);
        });
    }
    /// <summary>
    /// ���ʤ���˻ָ���Ϸ�¼�
    /// </summary>
    private void WinGameReturn() {
        StartScene();
        txtJifenBan1.text = "0";
    }
    /// <summary>
    /// ����������ť�¼�
    /// </summary>
    private void StartScene() {
        imgStart.gameObject.SetActive(false);
        Text text1 = btnStartScene.transform.GetChild(0).GetComponent<Text>();
        text1.fontStyle = FontStyle.Bold;
        text1.color = Color.red;

        Text text2 = btnGameScene.transform.GetChild(0).GetComponent<Text>();
        text2.fontStyle = FontStyle.Normal;
        text2.color = Color.white;
        initObj.gameObject.SetActive(true);
        AudioController.GetInstance().StartGame();
        panelRecord.gameObject.SetActive(false);
        ObjectController.GetInstance().GameScene(false);
    }
    /// <summary>
    /// ������Ϸ������ť�¼�
    /// </summary>
    public void GameScene() {
        Text text1 = btnGameScene.transform.GetChild(0).GetComponent<Text>();
        text1.fontStyle = FontStyle.Bold;
        text1.color = Color.red;

        Text text2 = btnStartScene.transform.GetChild(0).GetComponent<Text>();
        text2.fontStyle = FontStyle.Normal;
        text2.color = Color.white;
        initObj.gameObject.SetActive(false);
        panelRecord.gameObject.SetActive(true);
        ObjectController.GetInstance().GameScene();
    }
    /// <summary>
    /// ��Ϸ�����¼�
    /// </summary>
    public void GameOverEvent(bool _iswin = false) {
        Debug.Log("��Ϸ����");
        isWin = _iswin;
        AudioController.GetInstance().DisableOther();
        StartCoroutine(AudioController.GetInstance().SetAudioClipByName("ʤ��3", false, AudioController.GetInstance().CreateAudio()));
        panelOver.gameObject.SetActive(true);
    }
    /// <summary>
    /// �˳���Ϸ
    /// </summary>
    private void GameOverRealEvent() {
        Application.Quit();
    }
    /// <summary>
    /// ����Ŀ¼�¼�
    /// </summary>
    private void TurnMenuEvent() { 
    
    }
    // Start is called before the first frame update
    void Start()
    {
        InitButtonEvent();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// tank�Ʒְ��1
    /// </summary>
    public void AddDishuNum() {
        txtJifenBan1.transform.parent.GetComponent<RectTransform>().DOSizeDelta(new Vector2(100f, 133.25f), 0.2f).SetEase(Ease.Linear).OnComplete(delegate ()
        {
            txtJifenBan1.transform.parent.GetComponent<RectTransform>().DOSizeDelta(new Vector2(80f, 106.5f), 0.2f).SetEase(Ease.Linear);
        });
        int dishuNum = int.Parse(txtJifenBan1.text);
        dishuNum++;
        txtJifenBan1.GetComponent<CountingNumber>().ChangeTo(dishuNum, 0.2f);
        txtJifenBan2.text = dishuNum.ToString();
    }
}
