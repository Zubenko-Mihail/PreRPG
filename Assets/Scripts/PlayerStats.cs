using Items;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    Inventory inventory;
    Animator animator;
    [HideInInspector]
    public int HP = 100,
    maxHP = 100,
    MP = 100,
    maxMP = 100,
    physRes,
    weight,
    maxWeight,
    level,
    needXP,
    curXP,
    averageDamage,
    money;
    [HideInInspector]
    public float damageMultiplier = 0,
    rawAttackSpeed,
    attackSpeedMultiplier,
    attackSpeed,
    attackRange = 1;

    Slot Weapon_1, Weapon_2, Helmet, Chestplate, Leggings, Boots, Bracers;

    private GameObject RightHand, Head, Chest, LeftLeg, RightLeg;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        ResetStats();
        inventory = GetComponent<Inventory>();
        Weapon_1 = inventory.Weapon_1;
        Weapon_2 = inventory.Weapon_2;
        Helmet = inventory.Helmet;
        Chestplate = inventory.Chestplate;
        Leggings = inventory.Leggings;
        Boots = inventory.Boots;
        Bracers = inventory.Bracers;
        maxHP = 100;
        HP = maxHP;

        RightHand = UsefulThings.TransformSearch(transform, "RightHand").gameObject;
        Head = UsefulThings.TransformSearch(transform, "Head").gameObject;
        Chest = UsefulThings.TransformSearch(transform, "Chest").gameObject;
        LeftLeg = UsefulThings.TransformSearch(transform, "LeftLeg").gameObject;
        RightLeg = UsefulThings.TransformSearch(transform, "RightLeg").gameObject;

        CreateMesh(RightHand);
        CreateMesh(Head);
        CreateMesh(Chest);
        CreateMesh(LeftLeg);
        CreateMesh(RightLeg);
    }
    private void CreateMesh(GameObject go)
    {
        Transform mesh;
        mesh = new GameObject("Mesh").transform;
        mesh.parent = go.transform;
        mesh.localPosition = Vector3.zero;
        mesh.localRotation = Quaternion.identity;
        mesh.gameObject.AddComponent<MeshFilter>();
        mesh.gameObject.AddComponent<MeshRenderer>();
    }
    public void UpdateEquipment()
    {
        ResetStats();
        UpdateItem(Weapon_1);
        UpdateItem(Helmet);
        UpdateItem(Chestplate);
        UpdateItem(Leggings);
        UpdateItem(Boots);
        UpdateItem(Bracers);
        UpdateAttackAnimBehaviours();
        UpdateLook();
    }
    private void UpdateItem(Slot slot)
    {
        if (slot != null && slot.Item != null)
        {
            Item item = slot.Item;
            switch (item.itemType)
            {
                case ItemType.Weapon:
                    rawAttackSpeed = (item as Weapon).attackSpeed;
                    attackRange = (item as Weapon).range;
                    break;
                case ItemType.Armor:
                    physRes += (item as Armor).physRes;
                    break;
            }
        }
    }
    private void ResetStats()
    {
        physRes = 0;
        weight = 0;
        maxWeight = 0;
        averageDamage = 0;
        attackRange = 1;
    }
    public void GetDamage(Damage damage)
    {
        HP -= (int)damage.amount;
        UpdateUI();
        if (HP <= 0)
        {
            Death();
        }
    }
    public void UpdateUI()
    {
        UI.SetHPNormalized(HP / (float)maxHP);
    }
    private void UpdateAttackAnimBehaviours()
    {
        if (animator != null && inventory.Weapon_1.Item != null)
            foreach (AttackAnimBehaviour aAB in animator.GetBehaviours<AttackAnimBehaviour>())
            {
                aAB.attackSpeed = (inventory.Weapon_1.Item as Weapon).attackSpeed;
            }
    }
    private void UpdateLook()
    {
        SetLook(RightHand,Weapon_1);
        SetLook(Head, Helmet);
        SetLook(Chest, Chestplate);
        SetLook(LeftLeg, Leggings);
        SetLook(RightLeg, Leggings);
    }
    private void SetLook(GameObject go, Slot slot)
    {
        if (slot != null)
        {
            go = go.transform.GetChild(0).gameObject;
            if (slot.Item != null)
            {
                go.SetActive(true);
                GameObject pref = slot.Item.prefab;
                Mesh mesh = pref.GetComponent<MeshFilter>().sharedMesh;
                go.GetComponent<MeshFilter>().sharedMesh = mesh;
                go.GetComponent<MeshRenderer>().sharedMaterials = slot.Item.prefab.GetComponent<MeshRenderer>().sharedMaterials;
                go.transform.localRotation = slot.Item.prefab.transform.localRotation;
            }
            else
            {
                go.SetActive(false);
            }
        }
    }
    private void Death()
    {
        print("KILLED");
        HP = maxHP;
        UI.SetHPNormalized(HP / (float)maxHP);
        SceneLoader.LoadScene("Game");
    }
    
    
}
