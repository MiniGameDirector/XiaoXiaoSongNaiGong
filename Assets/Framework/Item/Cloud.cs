using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOMove(transform.position + new Vector3(Random.Range(-1f,1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)), 2f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        transform.DORotate(transform.localEulerAngles + new Vector3(Random.Range(-10f,10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f)), 2).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
