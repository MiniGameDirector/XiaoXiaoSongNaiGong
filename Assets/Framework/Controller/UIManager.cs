using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region 单例
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
    //场景切换按钮
    public Button btnStartScene, btnGameScene;

    public Button btnStartGame, btnTurn;
    public Transform imgStart;

    public Transform initObj;
    public Transform panelRecord;
    public Transform panelOver;
    public Text txtJifenBan1, txtJifenBan2;
    public Button btnMilk1, btnMilk2;
    public bool canChangeScene = false;
    public bool isWin = false;

    private int leftMilkCount = 10, rightMilkCount = 10;
    public bool leftBtnClick = false;
    /// <summary>
    /// 初始化事件
    /// </summary>
    private void InitButtonEvent() {
        //开始游戏
        btnStartGame.onClick.AddListener(StartScene);
        //返回目录
        btnTurn.onClick.AddListener(delegate () { Application.Quit(); });
        //游戏结束
        btnGameOver.onClick.AddListener(delegate() {
            if (canChangeScene)
            {
                GameOverEvent(false);
            }
        });
        //游戏开场按钮
        btnStartScene.onClick.AddListener(delegate ()
        {
            StartScene();
        });
        //游戏场景按钮
        btnGameScene.onClick.AddListener(delegate ()
        {
            if (canChangeScene)
            {
                GameScene();
            }
        });
        //最终退出游戏按钮
        btnRealOver.onClick.AddListener(delegate () {
            StartCoroutine(AudioController.GetInstance().SetAudioClipByName("结束语音", false, null, GameOverRealEvent));
            AudioController.GetInstance().DisableOther();
        });
        //恢复游戏
        btnReturn.onClick.AddListener(delegate ()
        {
            WinGameReturn();
        });

        //下边的奶瓶按钮
        btnMilk1.onClick.AddListener(delegate() {
            MilkClickEvent(0);
        });
        btnMilk2.onClick.AddListener(delegate () {
            MilkClickEvent(1);
        });
    }
    /// <summary>
    /// 如果胜利了恢复游戏事件
    /// </summary>
    public void WinGameReturn() {
        AudioController.GetInstance().DisableOther();
        if (isWin)
        {
            StartScene();
            txtJifenBan1.text = "0";
            isWin = false;
        }
        panelOver.gameObject.SetActive(false);
        
    }
    /// <summary>
    /// 开场场景按钮事件
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
        //initObj.GetComponent<NavMeshAgent>().Stop();
        AudioController.GetInstance().StartGame();
        panelRecord.gameObject.SetActive(false);
        ObjectController.GetInstance().GameScene(false);
    }
    /// <summary>
    /// 进入游戏场景按钮事件
    /// </summary>
    public void GameScene() {
        Text text1 = btnGameScene.transform.GetChild(0).GetComponent<Text>();
        text1.fontStyle = FontStyle.Bold;
        text1.color = Color.red;

        Text text2 = btnStartScene.transform.GetChild(0).GetComponent<Text>();
        text2.fontStyle = FontStyle.Normal;
        text2.color = Color.white;
        //initObj.gameObject.SetActive(false);
        panelRecord.gameObject.SetActive(true);
        //ObjectController.GetInstance().GameScene();
    }
    /// <summary>
    /// 游戏结束事件
    /// </summary>
    public void GameOverEvent(bool _iswin = false) {
        Debug.Log("游戏结束");
        isWin = _iswin;
        AudioController.GetInstance().DisableOther();
        StartCoroutine(AudioController.GetInstance().SetAudioClipByName("胜利4", false, AudioController.GetInstance().CreateAudio()));
        panelOver.gameObject.SetActive(true);
    }
    /// <summary>
    /// 退出游戏
    /// </summary>
    private void GameOverRealEvent() {
        Application.Quit();
    }
    /// <summary>
    /// 奶瓶按钮的点击事件
    /// </summary>
    /// <param name="milkBtnIndex"></param>
    private void MilkClickEvent(int milkBtnIndex) {
        if (milkBtnIndex == 0 && leftMilkCount > 0)//左侧的按钮
        {
            leftBtnClick = true;
            leftMilkCount--;
            Text tMilkLeft = btnMilk1.transform.Find("Text").GetComponent<Text>();
            tMilkLeft.text = leftMilkCount.ToString();
            tMilkLeft.color = new Color((10 - leftMilkCount) * 25.5f / 255f, leftMilkCount * 25.5f / 255f, 0);
            ObjectController.GetInstance().GameScene();
            
        }
        else if(milkBtnIndex == 1 && rightMilkCount > 0)
        {
            leftBtnClick = false;
            rightMilkCount--;
            Text tMilkRight = btnMilk2.transform.Find("Text").GetComponent<Text>();
            tMilkRight.text = rightMilkCount.ToString();
            tMilkRight.color = new Color((10 - rightMilkCount) * 25.5f / 255f, rightMilkCount * 25.5f / 255f, 0);
            ObjectController.GetInstance().GameScene();
        }
        if (rightMilkCount == 0 && leftMilkCount == 0)
        {
            isWin = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.LogError("开始游戏测试");
        StartScene();
        InitButtonEvent();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// tank计分板加1
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
