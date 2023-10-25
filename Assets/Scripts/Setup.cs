using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Setup : MonoBehaviour
{
    //ÉÁÆÁ ¸üÐÂ µÇÂ¼

    private void Awake()
    {


        GameManager.Instance.Init();
    }
}

