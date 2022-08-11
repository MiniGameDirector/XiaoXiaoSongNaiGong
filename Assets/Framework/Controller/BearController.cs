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
    public bool isStart = false;

    // Start is called before the first frame update
    void Awake()
    {
        //bear = this.transform;
        bearStartPos = bear.position;
        bearAnimator = bear.GetChild(0).GetComponent<Animator>();
        meshAgent = gameObject.GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        BearStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, bearTargetPos) < 0.5f && isMove)
        {
            Debug.Log("ֹͣ");
            if (!isStart)
            {
                if (!PanduanForward())
                {
                    transform.eulerAngles += new Vector3(0, 180, 0);
                }
                SetBearIdle();
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 10, 0);
                StartCoroutine(BearRestart());
                isStart = false;
            }
            isMove = false;
            
        }
    }
    /// <summary>
    /// ����С���ƶ�
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
        StartCoroutine(AudioController.GetInstance().SetAudioClipByName("ð����ʾ", false, AudioController.GetInstance().CreateAudio()));
    }
    /// <summary>
    /// �ж������С�ܵ�ǰ�߻��Ǻ��
    /// </summary>
    /// <returns></returns>
    private bool PanduanForward() {
        Vector3 dir = Camera.main.transform.position - transform.position;
        return Vector3.Dot(transform.forward, dir) > 0;
    }
    /// <summary>
    /// ��ʼ�� animation��transform
    /// </summary>
    public void BearStart() {
        bear.position = bearStartPos;
        bearAnimator.Play("Run");
        bear.DOMove(bearStandePos, 3f).SetEase(Ease.Linear).OnComplete(delegate() {
            bearAnimator.SetBool("Skidding", true);
            StartCoroutine(AudioController.GetInstance().SetAudioClipByName("��������", false, null, delegate () {
                UIManager.GetInstance().canChangeScene = true;
                UIManager.GetInstance().GameScene();
            }));
        });
    }
    /// <summary>
    /// �ָ���Ϸ��ťС�ܵĶ���
    /// </summary>
    /// <returns></returns>
    private IEnumerator BearRestart() {
        bearTargetPos = bearStandePos;
        bearAnimator.Play("Run");
        meshAgent.SetDestination(bearTargetPos);
        yield return new WaitForSeconds(3f);
        bearAnimator.SetBool("Skidding", true);
        StartCoroutine(AudioController.GetInstance().SetAudioClipByName("��������", false, null, delegate () {
            UIManager.GetInstance().canChangeScene = true;
            UIManager.GetInstance().GameScene();
        }));
    }
}
