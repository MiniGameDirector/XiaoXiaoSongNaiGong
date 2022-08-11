using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseUp()
    {
        if (transform.name.Split('_')[1] == "1" || transform.name.Split('_')[1] == "4" || transform.name.Split('_')[1] == "8" || transform.name.Split('_')[1] == "11")
        {
            ObjectController.GetInstance().SongNai();
            ObjectController.GetInstance().DisableCollider();
        }
        else
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //���߼�⣺�õ����߸���ײ�������ײ��λ��
            //Physics.Raycast();
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                Debug.Log(raycastHit.point.y);//��ӡ������ײ����ά����������
                if (raycastHit.point.y < 2)
                {
                    AudioController.GetInstance().DisableOther();
                    StartCoroutine(AudioController.GetInstance().SetAudioClipByName("����_0001", false, AudioController.GetInstance().CreateAudio()));
                    StartCoroutine(AudioController.GetInstance().SetAudioClipByName("��ʾ��¥", false, AudioController.GetInstance().CreateAudio()));
                }
                else
                {
                    ObjectController.GetInstance().SongNai();
                    transform.GetComponent<MeshCollider>().enabled = false;
                }
            }
            
        }
    }
}
