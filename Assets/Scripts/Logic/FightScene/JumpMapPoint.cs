using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//��ͼ��ת��
public class JumpMapPoint : MonoBehaviour
{
    public int JumpToMapID = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != GameSetting.MainRoleLayer)
        {
            return;
        }

        other.gameObject.GetComponent<MainRole>().OnJumpTo(JumpToMapID);
    }
}
