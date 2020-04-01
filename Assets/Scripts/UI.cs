using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    Camera camComponent, miniCamComponent;
    static Image HPImage;
    static Image MPImage;
    GameObject player, miniCam, miniMap;
    private void Awake()
    {
        HPImage = PlayerStats.TransformSearch(transform, "HP").GetComponent<Image>();
        MPImage = PlayerStats.TransformSearch(transform, "MP").GetComponent<Image>();
        player = GameObject.FindGameObjectWithTag("Player");
        camComponent = player.GetComponent<Controls>().camComponent;
        miniCam = GameObject.Find("MiniCam");
        miniCamComponent = miniCam.GetComponent<Camera>();
        miniMap = GameObject.Find("MiniMap");
    }
    void Start()
    {

    }
    void OnGUI()
    {
        WatchStats();
    }
    void Update()
    {
        MiniMap();
    }
    public GameObject attackTarget;
    void WatchStats()
    {
        attackTarget = player.GetComponent<Attack>().target;
        RaycastHit hit;
        if (Physics.Raycast(camComponent.ScreenPointToRay(Input.mousePosition), out hit))
        {
            if (hit.collider.gameObject.tag == "Item" && attackTarget == null)
            {
                GameObject go = hit.collider.gameObject.transform.parent.gameObject;
                string name = go.GetComponent<ItemConnector>().item.name;
                GUI.contentColor = Color.white;
                GUI.skin.box.fontSize = 35;
                GUI.Box(new Rect((Screen.width - GUI.skin.box.fontSize * name.Length * 0.7f) / 2, 10, GUI.skin.box.fontSize * (name.Length * 0.7f), GUI.skin.box.fontSize + 10), name);
            }
            if (hit.collider.gameObject.tag == "Enemy" && attackTarget == null && hit.collider.gameObject.GetComponent<Renderer>().enabled == true)
            {
                GameObject go = hit.collider.gameObject;
                float HP = go.GetComponent<EnemyConnector>().enemy.HP;
                float maxHP = go.GetComponent<EnemyConnector>().enemy.maxHP;
                string s = go.name + " HP: " + HP.ToString();
                GUI.color = Color.red;
                GUI.DrawTexture(new Rect((Screen.width - GUI.skin.box.fontSize * s.Length) / 2, 10, GUI.skin.box.fontSize * s.Length * (HP / maxHP), GUI.skin.box.fontSize + 10), new Texture2D(10, 10));
                GUI.color = Color.white;
                GUI.skin.box.fontSize = 35;
                GUI.Box(new Rect((Screen.width - GUI.skin.box.fontSize * s.Length) / 2, 10, GUI.skin.box.fontSize * s.Length, GUI.skin.box.fontSize + 10), s);
            }
            if (attackTarget != null)
            {
                float HP = attackTarget.GetComponent<EnemyConnector>().enemy.HP;
                if (HP > 0)
                {
                    float maxHP = attackTarget.GetComponent<EnemyConnector>().enemy.maxHP;
                    GUI.skin.box.fontSize = 35;
                    string s = attackTarget.name + " HP: " + HP.ToString();
                    GUI.color = Color.red;
                    GUI.DrawTexture(new Rect((Screen.width - GUI.skin.box.fontSize * s.Length) / 2, 10, GUI.skin.box.fontSize * s.Length * (HP / maxHP), GUI.skin.box.fontSize + 10), new Texture2D(10, 10));
                    GUI.color = Color.white;
                    GUI.Box(new Rect((Screen.width - GUI.skin.box.fontSize * s.Length) / 2, 10, GUI.skin.box.fontSize * s.Length, GUI.skin.box.fontSize + 10), s);
                }
            }
        }
    }
    public static void SetHPNormalized(float f)
    {
        HPImage.fillAmount = f;
    }
    void MiniMap()
    {
        if (Input.GetButtonDown("MiniMap+") && miniCamComponent.orthographicSize > 30)
        {
            miniCamComponent.orthographicSize -= 10;
        }
        if (Input.GetButtonDown("MiniMap-") && miniCamComponent.orthographicSize < 60)
        {
            miniCamComponent.orthographicSize += 10;
        }
        if (Input.GetButtonDown("HideMiniMap"))
        {
            miniMap.SetActive(!miniMap.activeSelf);
        }
    }
}
