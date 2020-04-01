using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class SceneLoader : MonoBehaviour
{
    static string num;
    static AsyncOperation currentLoading;
    static SceneLoader instance;
    static GameObject player;
    static NavMeshAgent nav;
    private void Awake()
    {
        instance = this;
        
        player = GameObject.FindGameObjectWithTag("Player");
        if(SceneManager.sceneCount<2)
        {
            LoadScene("Beggining");
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            LoadScene("lev2");
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            LoadScene("lev3");
        }

    }

    public static void LoadScene(string sceneName)
    {
        if (SceneManager.GetSceneByName(sceneName).buildIndex==-1)
        {
            nav = GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>();
            nav.enabled = false;
            num = Convert.ToInt32(UnityEngine.Random.Range(1, 3)).ToString();
            Time.timeScale = 0;
            if (SceneManager.sceneCount > 1)
                SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(SceneManager.sceneCount - 1));
            currentLoading = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }
    }
    private void OnGUI()
    {
        if (currentLoading != null && !currentLoading.isDone)
        {
            //works tak sebe
            //a cho ne tak? prosto slishkom bistraya zagruzka
            GUI.skin.box.stretchHeight = true;
            GUI.skin.box.stretchWidth = true;
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), Resources.Load<Texture2D>("LoadScreens/" + num));
            GUI.skin.box.alignment = TextAnchor.MiddleCenter;
            GUI.backgroundColor = Color.cyan;
            GUI.Box(new Rect((Screen.width - Screen.width * 0.78f) / 2, Screen.height * 0.6f,
                Screen.width * 0.78f * currentLoading.progress, Screen.height * 0.06f), "");
            GUI.backgroundColor = Color.black;
            GUI.Box(new Rect((Screen.width - Screen.width * 0.78f) / 2, Screen.height * 0.6f,
                Screen.width * 0.78f, Screen.height * 0.06f), "LOADING " + (currentLoading.progress * 100).ToString() + "%");
        }
        if (currentLoading != null && currentLoading.isDone)
        {
            player.SetActive(true);
            Time.timeScale = 1;
            player.transform.position = GameObject.FindGameObjectWithTag("Entrance").transform.position;
            currentLoading = null;
            nav.enabled = true;
        }
    }
}
