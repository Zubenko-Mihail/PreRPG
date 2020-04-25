using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class UI : MonoBehaviour
{
    Camera camComponent, miniCamComponent;
    static Image HPImage;
    static Image MPImage;
    GameObject player, miniCam, miniMap, help;
    private void Awake()
    {
        help = UsefulThings.TransformSearch(transform, "HelpMenuPanel").gameObject;
        HPImage = UsefulThings.TransformSearch(transform, "HP").GetComponent<Image>();
        MPImage = UsefulThings.TransformSearch(transform, "MP").GetComponent<Image>();
        player = GameObject.FindGameObjectWithTag("Player");
        camComponent = player.GetComponent<Controls>().camComponent;
        miniCam = GameObject.Find("MiniCam");
        miniCamComponent = miniCam.GetComponent<Camera>();
        miniMap = GameObject.Find("MiniMap");
    }
    void Start()
    {
        UsefulThings.inputManager.Gameplay.ChangeMinimapSize.performed += ctx => ChangeMinimapSize(ctx.ReadValue<float>());
        UsefulThings.inputManager.Gameplay.HideMiniMap.performed += _ => HideMiniMap();

    }
    void OnGUI()
    {
        WatchStats();
    }
    void Update()
    {
        Help();
    }
    public GameObject attackTarget;
    void WatchStats()
    {
        attackTarget = player.GetComponent<Attack>().target;
        RaycastHit hit;
        if (Physics.Raycast(camComponent.ScreenPointToRay(UsefulThings.mouse.position.ReadValue()), out hit))
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
    void HideMiniMap()
    {
        miniMap.SetActive(!miniMap.activeSelf);
    }

    void Help()
    {
        if (UsefulThings.kb.f1Key.wasPressedThisFrame)
        {
            help.SetActive(!help.activeSelf);
        }
    }
    void ChangeMinimapSize(float f)
    {
        if (miniCamComponent.orthographicSize > 30 && f < -0.5f)
        {
            miniCamComponent.orthographicSize -= 10;
        }
        if (miniCamComponent.orthographicSize < 60 && f > 0.5f)
        {
            miniCamComponent.orthographicSize += 10;
        }
    }

}
