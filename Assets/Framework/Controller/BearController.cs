using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class BearController : MonoBehaviour
{

    public Transform bear;
    public string startAnimName, latterAnimName;
    public Vector3 bearStandePos;
    private Animator bearAnimator;
    private Vector3 bearStartPos;
    private NavMeshAgent meshAgent;
    private bool isMove = false;

    // Start is called before the first frame update
    void Awake()
    {
        //bear = this.transform;
        bearStartPos = bear.position;
        bearAnimator = bear.GetChild(0).GetComponent<Animator>();
        meshAgent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x == bearStandePos.x && isMove)
        {
            Debug.Log("停止");
            isMove = false;
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            meshAgent.SetDestination(bearStandePos);
            isMove = true;
        }
    }
    /// <summary>
    /// 初始化 animation、transform
    /// </summary>
    public void OnEnable() {
        bear.position = bearStartPos;
        AnimatorController.SetAnimatorByName(bearAnimator, startAnimName);
        bear.DOMove(bearStandePos, 3f).SetEase(Ease.Linear).OnComplete(delegate() {
            AnimatorController.SetAnimatorByName(bearAnimator, latterAnimName);
            StartCoroutine(AudioController.GetInstance().SetAudioClipByName("开场语音", false, null, delegate () {
                UIManager.GetInstance().canChangeScene = true;
                UIManager.GetInstance().GameScene();
            }));
        });
    }
}
