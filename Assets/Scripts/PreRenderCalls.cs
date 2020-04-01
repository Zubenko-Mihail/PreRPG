using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreRenderCalls : MonoBehaviour
{
    public Fog _Fog;
    private void Start()
    {
        
    }
    void OnPreRender()
    {
        _Fog = GameObject.Find("Fog").GetComponent<Fog>();
        // FOG CALL
        _Fog.SetCookie();
    }
}
