using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class Archimanager : MonoBehaviour
{
    
    private void Awake()
    {
        UsefulThings.inputManager = new InputManager();
        UsefulThings.inputManager.Enable();
        UsefulThings.kb = Keyboard.current;
        UsefulThings.mouse = Mouse.current;
        ItemManager.InitializeItems();
        EntityManager.InitializeEntities();
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
