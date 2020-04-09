using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationTransition : MonoBehaviour
{
    
    public Object ToScene;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            ShowDialogueWindow();
            ChangeLocation();
        }
    }
    void ShowDialogueWindow()
    {

    }
    void ChangeLocation()
    {
        SceneLoader.LoadScene(ToScene.name);
    }
}
