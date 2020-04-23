using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationTransition : MonoBehaviour
{
    public string ToScene;
    [SerializeField]
    public GameObject dialogueWindow;
    GameObject player;
    GameObject currDialogueWindow;
    Button Yes, No;
    Text text;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            ShowDialogueWindow();
        }
    }
    void ShowDialogueWindow()
    {
        Time.timeScale = 0;
        currDialogueWindow = Instantiate(dialogueWindow, GameObject.Find("UI").transform, false);
        Yes = UsefulThings.TransformSearch(currDialogueWindow.transform, "ButtonYes").GetComponent<Button>();
        No = UsefulThings.TransformSearch(currDialogueWindow.transform, "ButtonNo").GetComponent<Button>();
        text = UsefulThings.TransformSearch(currDialogueWindow.transform, "Text").GetComponent<Text>();
        Yes.onClick.AddListener(ChangeLocation);
        No.onClick.AddListener(CloseDialogueWindow);
        text.text = "Перейти в " + ToScene;
    }
    void ChangeLocation()
    {
        CloseDialogueWindow();
        SceneLoader.LoadScene(ToScene);
    }
    void CloseDialogueWindow()
    {
        player.transform.position = transform.position + transform.forward * 3;
        player.GetComponent<UnityEngine.AI.NavMeshAgent>().ResetPath();
        Destroy(currDialogueWindow);
        Time.timeScale = 1;
    }
}
