using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjItem : MonoBehaviour
{
    public ObjType objType;
    public int objIndex;
    public Vector3 birthPos, endPos;
    public void BirthObj(Action complete = null) {
        transform.position = birthPos;
        transform.DOMove(endPos, 5f).SetEase(Ease.Linear).OnComplete(delegate ()
        {
            complete?.Invoke();
        });
    }
}
public enum ObjType { 
    air,
    tank
}
