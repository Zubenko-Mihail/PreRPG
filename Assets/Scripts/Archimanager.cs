using System.IO;
using UnityEngine;

public class Archimanager : MonoBehaviour
{
    private void Awake()
    {
        ItemManager.InitializeItems();
        GameObject.Find("Fog").GetComponent<Projector>().enabled = true;
    }
    private void Start()
    {
        SaveManager.LoadGame();
    }
    private void OnApplicationQuit()
    {
        //File.WriteAllText(Directory.GetParent(Application.dataPath) + "/LOG.txt", Application.consoleLogPath);
    }

}
