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

    public GameObject ForgeLine;
    public GameObject Jacque;
    Transform jacquePos;
    float progress;
    float step;
    Vector2 start = new Vector2(-250f, -30f);
    Vector2 end = new Vector2(250f, -30f);
    Vector2 c;

    public int QTEGen;
    public int WaitingForKey;
    public int CorrectKey;
    public int CountingDown;

    public int fail;
    public int pass;
    public int spawn;

    public bool Craft = false;
    public bool LineCraft = false;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryInteractions>();
        forge = transform;
        UI = GameObject.Find("UI");

        ForgeQTE = UI.transform.Find("CraftInterface/ForgeQTE").gameObject;
        BTPBox = UI.transform.Find("CraftInterface/ForgeQTE/ButtonToPress").gameObject;
        LogBox = UI.transform.Find("CraftInterface/ForgeQTE/Log").gameObject;

        ForgeLine = UI.transform.Find("CraftInterface/ForgeLine").gameObject;
        Jacque = UI.transform.Find("CraftInterface/ForgeLine/Jacque").gameObject;
        jacquePos = Jacque.transform;
        step = 0.01f;
        StartLineCraft();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //   StartCraft();
        //}

        if (UsefulThings.kb.escapeKey.wasPressedThisFrame)
        {
            if(Craft || LineCraft)
                StopCraft();
        }

        if (LineCraft)
        {
            jacquePos.localPosition = Vector2.Lerp(start, end, progress);
            progress += step;

            if (UsefulThings.kb.anyKey.wasPressedThisFrame)
            {
                if (0.6499f < progress && progress < 0.7501f)
                {
                    pass++;
                    Debug.Log("PASS!!!");
                    Debug.Log(progress);
                }
                else
                {
                    fail++;
                    Debug.Log("Fail");
                    Debug.Log(progress);
                }
                spawn++;

                if (spawn >= 10)
                    Spawning();

                progress = 0;
                step = Mathf.Abs(step);
            }

            if (progress >= 1.01f)
                step = -step;
            if (progress <= -0.01f)
                step = -step;
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
            if (UsefulThings.kb.anyKey.wasPressedThisFrame)
            {
                Debug.Log(QTEGen);
                switch (QTEGen)
                {
                    case 1:
                        if (UsefulThings.kb.bKey.wasPressedThisFrame)
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
                        if (UsefulThings.kb.nKey.wasPressedThisFrame)
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
                        if (UsefulThings.kb.mKey.wasPressedThisFrame)
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

    public void StartLineCraft()
    {
        Time.timeScale = 0;
        LineCraft = true;
        CorrectKey = 0;
        WaitingForKey = 0;
        CountingDown = 1;
        QTEGen = 4;

        ForgeLine.SetActive(true);
        jacquePos.localPosition = new Vector2(-250, -30);
        start = new Vector2(-250f, -30f);
        end = new Vector2(250f, -30f);
        step = Mathf.Abs(step);

        fail = 0;
        pass = 0;
        spawn = 0;
    }

    public void StopCraft()
    {
        Time.timeScale = 1;
        Craft = false;
        LineCraft = false;
        ForgeQTE.SetActive(false);
        ForgeLine.SetActive(false);
        spawn = 0;
    }

    void Spawning()
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
            price = Random.Range(1, (int)(Mathf.Max(100, 1000 - 100 * fail + 70 * pass)));
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
        item.price = price;
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
