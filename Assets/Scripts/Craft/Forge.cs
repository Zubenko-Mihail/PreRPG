using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Items;

public class Forge : MonoBehaviour
{
    public InventoryInteractions Player;
    public Transform forge;

    public GameObject UI;
    public GameObject ForgeQTE;
    public GameObject BTPBox;
    public GameObject LogBox;

    public int QTEGen;
    public int WaitingForKey;
    public int CorrectKey;
    public int CountingDown;

    public int fail;
    public int pass;
    public int spawn;

    public bool Craft = false;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryInteractions>();
        forge = transform;
        UI = GameObject.Find("UI");
        ForgeQTE = UI.transform.Find("CraftInterface/ForgeQTE").gameObject;
        BTPBox = UI.transform.Find("CraftInterface/ForgeQTE/ButtonToPress").gameObject;
        LogBox = UI.transform.Find("CraftInterface/ForgeQTE/Log").gameObject;
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //   StartCraft();
        //}
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StopCraft();
        }

        if (Craft)
        {
            if (WaitingForKey == 0)
            {
                QTEGen = Random.Range(1, 4);
                CountingDown = 1;

                Debug.Log(QTEGen);

                StartCoroutine(CountDown());

                WaitingForKey = 1;

                switch (QTEGen)
                {
                    case 1:
                        BTPBox.GetComponentInChildren<Text>().text = "[B]"; break;
                    case 2:
                        BTPBox.GetComponentInChildren<Text>().text = "[N]"; break;
                    case 3:
                        BTPBox.GetComponentInChildren<Text>().text = "[M]"; break;
                }
            }
            if (Input.anyKeyDown)
            {
                Debug.Log(QTEGen);
                switch (QTEGen)
                {
                    case 1:
                        if (Input.GetKeyDown(KeyCode.B))
                        {
                            CorrectKey = 1;
                            StartCoroutine(KeyPressing());
                        }
                        else
                        {
                            CorrectKey = 2;
                            StartCoroutine(KeyPressing());
                        }
                        break;
                    case 2:
                        if (Input.GetKeyDown(KeyCode.N))
                        {
                            CorrectKey = 1;
                            StartCoroutine(KeyPressing());
                        }
                        else
                        {
                            CorrectKey = 2;
                            StartCoroutine(KeyPressing());
                        }
                        break;
                    case 3:
                        if (Input.GetKeyDown(KeyCode.M))
                        {
                            CorrectKey = 1;
                            StartCoroutine(KeyPressing());
                        }
                        else
                        {
                            CorrectKey = 2;
                            StartCoroutine(KeyPressing());
                        }
                        break;
                }
            }
        }
    }

    public void StartCraft()
    {
        Time.timeScale = 0;
        Craft = true;
        CorrectKey = 0;
        WaitingForKey = 0;
        CountingDown = 1;
        QTEGen = 4;
        ForgeQTE.SetActive(true);
        fail = 0;
        pass = 0;
        spawn = 0;
    }

    public void StopCraft()
    {
        Time.timeScale = 1;
        Craft = false;
        ForgeQTE.SetActive(false);
        spawn = 0;
    }

    void Spawning()
    {
        Item item = null;
        int r;
        int lvl;
        ItemRarity rarity;
        Dictionary<ItemRarity, List<List<Item>>> Weapons = ItemManager.Weapons;
        Dictionary<ItemRarity, List<List<Item>>> Armor = ItemManager.Armor;
        while (item == null)
        {
            lvl = Random.Range(1, 3);
            r = Random.Range(0, 100);
            r = r - 2 * pass;
            if (r < 0)
                r = 0;
            r += 3 * fail;
            if (r < 10) rarity = ItemRarity.Mythical;
            else if (r < 20) rarity = ItemRarity.Legendary;
            else if (r < 40) rarity = ItemRarity.Rare;
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
        ItemGenerator.SpawnItem(item, forge.position + new Vector3(0, 0, 1.5f));
        StopCraft();
    }

    IEnumerator KeyPressing()
    {
        QTEGen = 4;
        CountingDown = 2;
        if (CorrectKey == 1)
        {
            LogBox.GetComponentInChildren<Text>().color = Color.green;
            LogBox.GetComponentInChildren<Text>().text = "PASS!";
            pass++;
        }
        if (CorrectKey == 2)
        {
            LogBox.GetComponentInChildren<Text>().color = Color.red;
            LogBox.GetComponentInChildren<Text>().text = "FAIL!";
            fail++;
        }
        yield return new WaitForSecondsRealtime(1.5f);
        CorrectKey = 0;
        LogBox.GetComponentInChildren<Text>().text = "";
        BTPBox.GetComponentInChildren<Text>().text = "";
        yield return new WaitForSecondsRealtime(1.5f);
        WaitingForKey = 0;
        CountingDown = 1;
        spawn++;
        if (spawn >= 10)
            Spawning();
    }

    IEnumerator CountDown()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        if (CountingDown == 1)
        {
            QTEGen = 4;
            CountingDown = 2;
            LogBox.GetComponentInChildren<Text>().color = Color.red;
            LogBox.GetComponentInChildren<Text>().text = "FAIL!";
            fail++;
            yield return new WaitForSecondsRealtime(0.5f);
            CorrectKey = 0;
            LogBox.GetComponentInChildren<Text>().text = "";
            BTPBox.GetComponentInChildren<Text>().text = "";
            yield return new WaitForSecondsRealtime(1.5f);
            WaitingForKey = 0;
            CountingDown = 1;
            spawn++;
            if (spawn >= 10)
                Spawning();
        }
    }
}
