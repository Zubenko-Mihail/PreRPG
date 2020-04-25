using Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class Controls : MonoBehaviour
{
    RaycastHit hit;
    Animator animator;
    public Camera camComponent;
    Vector3 targetMove, lookTo;
    NavMeshAgent nav;
    int layerMask;
    Ray MoveRay;
    bool wasBeingHeld = false, canMove = true, canBeginMove = true, qpressed;
    public bool canInteract = true, isRunning = false;
    const float OneSecond = 0.2f;
    float alreadyHolding = 0f;
    DialoguesManager dialoguesManager;
    public Coroutine waitUntilInteract, setTarget, rotateTowards;
    [Space]
    [Range(1, 20)]
    public float interactRange = 4;
    Attack attackScript;
    CamSwitcher camSwitcher;
    GameObject miniCam, cam, MoveParticles;
    PlayerStats playerStats;
    public GameObject InventoryPanel;
    public GameObject UI;

    Inventory PlayerInventory;
    public bool IsDragging;

    [Space]
    public KeyCode Esc = KeyCode.Escape;
    public string NPCName;

    Keyboard kb;
    Mouse mouse;
    private void Awake()
    {
        layerMask = 1 << LayerMask.NameToLayer("Default");
        layerMask |= 1 << LayerMask.NameToLayer("Ignore Raycast");
        layerMask = ~layerMask;

        PrefabsManager.SetPrefabs();
        PlayerInventory = GetComponent<Inventory>();
        animator = GetComponent<Animator>();
        camSwitcher = GetComponent<CamSwitcher>();
        attackScript = GetComponent<Attack>();
        playerStats = GetComponent<PlayerStats>();
        nav = GetComponent<NavMeshAgent>();
        UI = GameObject.Find("UI");
        miniCam = GameObject.Find("MiniCam");
        cam = GameObject.Find("Main Camera");
        camComponent = cam.GetComponent<Camera>();
        InventoryPanel = UI.transform.Find("InvUI/PlayerInventory").gameObject;
        MoveParticles = Resources.Load<GameObject>("Prefabs/Move Effect");
        dialoguesManager = UI.GetComponent<DialoguesManager>();
        kb = UsefulThings.kb;
        mouse = UsefulThings.mouse;
    }
    private void Start()
    {
        UsefulThings.inputManager.Gameplay.CreateItem.performed += _ => SpawnRandomItem();
    }
    void Update()
    {
        if (nav.velocity.magnitude <= 0.5f)
        {
            if (nav.isOnNavMesh && nav.remainingDistance < 0.01)
                nav.ResetPath();
            animator.SetBool("isRunning", false);
            isRunning = false;
        }
        else
        {
            animator.SetBool("isRunning", true);
            isRunning = true;
        }
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            canMove = true;
            canBeginMove = true;
        }
        else
        {
            if (!wasBeingHeld)
                canMove = false;
            canBeginMove = false;
        }

        Interact();
        CheckExitDia();
        Move();
        if (kb.escapeKey.wasPressedThisFrame)
        {
            GameObject SettingsMenu = UsefulThings.TransformSearch(UI.transform, "SettingsMenu").gameObject;
            SettingsMenu.SetActive(!SettingsMenu.activeSelf);
            if (SettingsMenu.activeSelf)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }

    private void LateUpdate()
    {
        MoveMiniCam();
    }

    void CheckExitDia()
    {
        if (kb.escapeKey.wasPressedThisFrame)
        {
            dialoguesManager.StopDia();
        }
    }

    void IsPressed()
    {
        if (mouse.leftButton.isPressed) alreadyHolding += Time.deltaTime;
        if (mouse.leftButton.wasReleasedThisFrame)
        {
            alreadyHolding = 0;
            wasBeingHeld = false;
        }
        if (alreadyHolding >= OneSecond) wasBeingHeld = true;
    }

    void Move()
    {
        IsPressed();
        if (mouse.leftButton.isPressed && wasBeingHeld && canInteract && canMove)
        {
            nav.ResetPath();
            isRunning = true;
            animator.SetBool("isRunning", true);
            MoveRay = camComponent.ScreenPointToRay(UsefulThings.mouse.position.ReadValue());
            if (Physics.Raycast(MoveRay, out hit, Mathf.Infinity, layerMask))
            {
                targetMove = hit.point;
            }

            lookTo.Set(targetMove.x, transform.position.y, targetMove.z);
            transform.LookAt(lookTo);
            nav.Move(transform.forward * 6 * Time.deltaTime);
        }
    }

    void MoveEffect(Vector3 targetMove)
    {
        Instantiate(MoveParticles, targetMove + Vector3.up * 1.5f, Quaternion.identity);
    }
    void MoveMiniCam()
    {
        miniCam.transform.position = transform.position + Vector3.up * 100;

    }

    void Interact()
    {
        if (mouse.leftButton.wasPressedThisFrame)
        {
            attackScript.RemoveTarget();
        }
        GameObject go;
        Ray ray = camComponent.ScreenPointToRay(UsefulThings.mouse.position.ReadValue());
        if (mouse.leftButton.wasReleasedThisFrame && canInteract)
        {
            if (waitUntilInteract != null)
                StopCoroutine(waitUntilInteract);
            if (rotateTowards != null)
                StopCoroutine(rotateTowards);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask) && !EventSystem.current.IsPointerOverGameObject())
            {
                go = hit.collider.gameObject;
                switch (go.tag)
                {
                    case "Item":
                        waitUntilInteract = StartCoroutine(WaitUntilInteract(go.transform.parent.gameObject));
                        break;
                    case "Enemy":
                    case "NPC":
                    case "CraftPlace":
                    case "Shop":
                        waitUntilInteract = StartCoroutine(WaitUntilInteract(go));
                        break;
                    default:
                        if (canBeginMove)
                        {
                            nav.SetDestination(hit.point);
                            MoveEffect(nav.destination);
                        }
                        break;
                }
            }
        }
    }

    public IEnumerator WaitUntilInteract(GameObject go)
    {
        setTarget = StartCoroutine(SetTarget(go));
        if (go.tag == "Item")
        {
            if (!((transform.position - go.transform.position).magnitude < interactRange))
                nav.SetDestination(go.transform.position);
            yield return new WaitUntil(() => (transform.position - go.transform.position).magnitude < interactRange);
            nav.ResetPath();
            if (PlayerInventory.AddItem(go.GetComponent<ItemConnector>().item, 1))
            {
                if (InventoryPanel.activeSelf)
                {
                    PlayerInventory.Show();
                }
                Destroy(go);
            }
            else
            {
                Debug.Log("Inventory is FULL!");
            }
        }
        else if (go.tag == "Enemy")
        {
            if (!((transform.position + Vector3.up * 0.5f - go.transform.position).magnitude < playerStats.attackRange))
                nav.SetDestination(go.transform.position);
            else nav.ResetPath();
            yield return new WaitUntil(() => (transform.position + Vector3.up * 0.5f - go.transform.position).magnitude < playerStats.attackRange);
            nav.ResetPath();
            if (PlayerInventory.Weapon_1.Item != null)
                Attack(go);
        }
        else if (go.tag == "NPC")
        {
            if (!((transform.position - go.transform.position).magnitude - go.GetComponent<CapsuleCollider>().radius * Mathf.Max(transform.localScale.x, transform.localScale.z) < interactRange))
                nav.SetDestination(go.transform.position);
            yield return new WaitUntil(() => (transform.position - go.transform.position).magnitude - go.GetComponent<CapsuleCollider>().radius * Mathf.Max(transform.localScale.x, transform.localScale.z) < interactRange);
            nav.ResetPath();
            canInteract = false;
            camSwitcher.SwitchCamToDia();
            NPCName = go.name;
        }
        else if (go.tag == "CraftPlace")
        {
            if (!((transform.position - go.transform.position).magnitude - go.GetComponent<CapsuleCollider>().radius * Mathf.Max(transform.localScale.x, transform.localScale.z) < interactRange))
                nav.SetDestination(go.transform.position);
            yield return new WaitUntil(() => (transform.position - go.transform.position).magnitude - go.GetComponent<CapsuleCollider>().radius * Mathf.Max(transform.localScale.x, transform.localScale.z) < interactRange);
            nav.ResetPath();
            switch (go.name)
            {
                case "Forge":
                    {
                        if (!go.GetComponent<Forge>().Craft)
                            go.GetComponent<Forge>().StartCraft();
                        break;
                    }
            }
        }
        else if (go.tag == "Shop")
        {
            Debug.Log("S");
            if (!((transform.position - go.transform.position).magnitude - go.GetComponent<CapsuleCollider>().radius * Mathf.Max(transform.localScale.x, transform.localScale.z) < interactRange))
                nav.SetDestination(go.transform.position);
            yield return new WaitUntil(() => (transform.position - go.transform.position).magnitude - go.GetComponent<CapsuleCollider>().radius * Mathf.Max(transform.localScale.x, transform.localScale.z) < interactRange);
            nav.ResetPath();
            
            transform.GetComponent<InventoryInteractions>().ShowShop();
            go.GetComponent<Inventory>().Show();
        }
    }

    void Attack(GameObject target)
    {
        rotateTowards = StartCoroutine(RotateTowards(target));
        attackScript.AttackTarget(target);
    }

    IEnumerator SetTarget(GameObject target)
    {
        while (!mouse.leftButton.isPressed)
        {
            if (target == null) break;
            if (nav.hasPath && canInteract)
                nav.SetDestination(target.transform.position);
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator RotateTowards(GameObject target)
    {
        Vector3 lookTargetVector3 = target.transform.position;
        lookTargetVector3.Set(lookTargetVector3.x, transform.position.y, lookTargetVector3.z);
        while (target!=null&&!(Mathf.Abs(Quaternion.LookRotation(target.transform.position - transform.position).eulerAngles.y - transform.rotation.eulerAngles.y) <= 1))
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lookTargetVector3 - transform.position), 10);
            yield return new WaitForFixedUpdate();
        }
    }

    void SpawnRandomItem()
    {
        Item item = null;
        int r;
        int lvl;
        int price = 1;
        ItemRarity rarity;
        Dictionary<ItemRarity, List<List<Item>>> Weapons = ItemManager.Weapons;
        Dictionary<ItemRarity, List<List<Item>>> Armor = ItemManager.Armor;
        while (item == null)
        {
            lvl = Random.Range(1, 3);
            price = Random.Range(1, 1000);
            r = Random.Range(0, 100);
            if (r < 10) rarity = ItemRarity.Mythical;
            else if (r < 20) rarity = ItemRarity.Legendary;
            else if (r < 30) rarity = ItemRarity.Rare;
            else rarity = ItemRarity.Common;
            if (r % 2 == 0)
                item = Weapons[rarity][lvl][Random.Range(0, Weapons[rarity][lvl].Count)];
            else
                item = Armor[rarity][lvl][Random.Range(0, Armor[rarity][lvl].Count)];
        }
        if (item.itemType == ItemType.Weapon)
        {
            item = ItemGenerator.CreateWeapon(item);
        }
        item.price = price;
        ItemGenerator.SpawnItem(item, transform.position);
    }
}