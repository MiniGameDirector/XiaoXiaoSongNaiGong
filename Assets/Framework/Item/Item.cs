using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour
{
    public int gsIndex = 0;//地上的是0 树上的是1
    public bool isKaihang = false;
    private Vector3 gsPos, gsScale;
    private Tweener tweener1 = null, tweener2 = null, tweener3 = null, tweener4 = null;
    private float waitTime = 0f;

    private void OnMouseUp()
    {
       
    }
    Vector3 ScreenToWorld() {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 mousePosOnScreen = UIManager.GetInstance().panelRecord.GetChild(0).position;
        mousePosOnScreen.z = screenPos.z;
        return Camera.main.ScreenToWorldPoint(mousePosOnScreen);
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        
    }
    private void Awake()
    {
        gsPos = transform.position;
        gsScale = transform.localScale;
    }
    private void OnDisable()
    {
        transform.position = gsPos;
        transform.localScale = gsScale;
        transform.GetComponent<Animator>().enabled = true;
    }
}
