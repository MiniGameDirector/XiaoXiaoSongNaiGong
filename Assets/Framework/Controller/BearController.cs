using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;
using System;

public class BearController : MonoBehaviour
{

    public Transform bear;
    public string startAnimName, latterAnimName;
    public Vector3 bearStandePos, bearTargetPos;
    public Animator bearAnimator;
    private Vector3 bearStartPos;
    private NavMeshAgent meshAgent;
    private bool isMove = false;
    private Action bearMoveComplete = null;

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
        if (Vector3.Distance(transform.position, bearTargetPos) < 0.5f && isMove)
        {
            Debug.Log("停止");
            if (!PanduanForward())
            {
                transform.eulerAngles += new Vector3(0, 180, 0);
            }
            SetBearIdle();
            isMove = false;
        }
    }
    /// <summary>
    /// 设置小熊移动
    /// </summary>
    /// <param name="targetPos"></param>
    /// <param name="_complete"></param>
    /// <returns></returns>
    public bool SetBearMove(Vector3 targetPos) {
        if (!isMove)
        {
            bearTargetPos = targetPos;
            bearAnimator.Play("Run_0");
            meshAgent.SetDestination(targetPos);
            isMove = true;
            return true;
        }
        return false;
    }
    private void SetBearIdle() {
        bearAnimator.Play("Idle & waving_0");
        StartCoroutine(AudioController.GetInstance().SetAudioClipByName("冒泡提示", false, AudioController.GetInstance().CreateAudio()));
    }
    /// <summary>
    /// 判断相机在小熊的前边还是后边
    /// </summary>
    /// <returns></returns>
    private bool PanduanForward() {
        Vector3 dir = Camera.main.transform.position - transform.position;
        return Vector3.Dot(transform.forward, dir) > 0;
    }
    /// <summary>
    /// 初始化 animation、transform
    /// </summary>
    public void BearStart() {
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
