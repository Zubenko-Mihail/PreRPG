using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwitcher : MonoBehaviour
{
    Camera mainCam,
    diaCam;
    public bool isInDia;
    void Start()
    { 
        mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        diaCam = GameObject.Find("DialogueCam").GetComponent<Camera>();
    }
    
    public void SwitchCamToDia()
    {
        //Time.timeScale = 0;
        isInDia = true;
        mainCam.enabled = !true;
        diaCam.enabled = !false;
    }
    public void SwitchCamToMain()
    {
        //Time.timeScale = 1;
        isInDia = false;
        mainCam.enabled = true;
        diaCam.enabled = false;
    }
}
