using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;
using Items;

public class Forge : MonoBehaviour
{
    public CraftType forge_type;

    public InventoryInteractions Player;
    public Transform forge;

    public GameObject UI;

    #region QTE
    public bool QTECraft = false;

    public GameObject ForgeQTE;
    public GameObject BTPBox;
    public GameObject LogBox;

    public int QTEGen;
    public int WaitingForKey;
    public int CorrectKey;
    public int CountingDown;
    #endregion

    #region Line
    public bool LineCraft = false;

    public GameObject ForgeLine;
    public GameObject RedLine;
    public RectTransform YellowLine;
    public RectTransform GreenLine;
    public GameObject Jacque;
    public Text LineLog;

    public const int RedLine_size = 500;
    List<int> YellowLine_size;
    List<int> GreenLine_size;
    Transform Jacque_pos;

    float progress;
    float step;
    Vector2 start = new Vector2(-250f, -30f);
    Vector2 end = new Vector2(250f, -30f);
    #endregion

    public int fail;
    public int medium;
    public int pass;
    public int spawn;
    
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryInteractions>();
        forge = transform;
        UI = GameObject.Find("UI");

        ForgeQTE = UI.transform.Find("CraftInterface/ForgeQTE").gameObject;
        BTPBox = UI.transform.Find("CraftInterface/ForgeQTE/ButtonToPress").gameObject;
        LogBox = UI.transform.Find("CraftInterface/ForgeQTE/Log").gameObject;

        ForgeLine = UI.transform.Find("CraftInterface/ForgeLine").gameObject;
        RedLine = UI.transform.Find("CraftInterface/ForgeLine/Red").gameObject;
        YellowLine = UI.transform.Find("CraftInterface/ForgeLine/Red/Yellow").gameObject.GetComponent<RectTransform>();
        GreenLine = UI.transform.Find("CraftInterface/ForgeLine/Red/Yellow/Green").gameObject.GetComponent<RectTransform>();
        Jacque = UI.transform.Find("CraftInterface/ForgeLine/Red/Jacque").gameObject;
        LineLog = UI.transform.Find("CraftInterface/ForgeLine/Log").gameObject.GetComponent<Text>();

        #region sizes
        GreenLine_size = new List<int>();
        GreenLine_size.Add(22);
        GreenLine_size.Add(18);
        GreenLine_size.Add(15);
        GreenLine_size.Add(12);
        GreenLine_size.Add(9);
        GreenLine_size.Add(7);
        GreenLine_size.Add(5);
        GreenLine_size.Add(3);
        GreenLine_size.Add(2);
        GreenLine_size.Add(1);
        YellowLine_size = new List<int>(GreenLine_size);
        for (int i = 0; i < 10; i++)
            YellowLine_size[i] *= 3;
        #endregion
        Jacque_pos = Jacque.transform;
        step = 0.01f;

        StartLineCraft();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.O))
        //   StartCraft();

        if (UsefulThings.kb.escapeKey.wasPressedThisFrame)
            if(QTECraft || LineCraft)
                StopCraft();

        if (LineCraft)
            LineCraftProcess();

        if (QTECraft)
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
        switch (forge_type)
        {
            case CraftType.QTE:
                {
                    StartQTECraft();
                    break;
                }
            case CraftType.Line:
                {
                    StartLineCraft();
                    break;
                }
        }
    }

    void StartQTECraft()
    {
        Time.timeScale = 0;
        QTECraft = true;
        CorrectKey = 0;
        WaitingForKey = 0;
        CountingDown = 1;
        QTEGen = 4;
        ForgeQTE.SetActive(true);
        fail = 0;
        pass = 0;
        spawn = 0;
    }

    void StartLineCraft()
    {
        Time.timeScale = 0;
        LineCraft = true;

        ForgeLine.SetActive(true);
        LineLog.text = "Press \"B\"\nLevel of Difficulty -- 1";

        ChangeLineSize(0);

        Jacque_pos.localPosition = new Vector2(-250, -30);

        start = new Vector2(-250f, -30f);
        end = new Vector2(250f, -30f);
        step = Mathf.Abs(step);

        fail = 0;
        pass = 0;
        spawn = 0;
    }

    void LineCraftProcess()
    {
        Jacque_pos.localPosition = Vector2.Lerp(start, end, progress);
        progress += step;

        if (UsefulThings.kb.anyKey.wasPressedThisFrame)
        {
            Debug.Log("Green zone: " + ((YellowLine.anchoredPosition.x + GreenLine.anchoredPosition.x - GreenLine.sizeDelta.x / 2) / 5 + 50).ToString() + " -- " + ((YellowLine.anchoredPosition.x + GreenLine.anchoredPosition.x + GreenLine.sizeDelta.x / 2) / 5 + 50).ToString());
            Debug.Log("Yellow zone: " + ((YellowLine.anchoredPosition.x - YellowLine.sizeDelta.x / 2) / 5 + 50).ToString() + " -- " + ((YellowLine.anchoredPosition.x + YellowLine.sizeDelta.x / 2) / 5 + 50).ToString());
            Debug.Log("Progress -- " + (progress * 100).ToString());
            if ((YellowLine.anchoredPosition.x + GreenLine.anchoredPosition.x - GreenLine.sizeDelta.x / 2) / 5 + 50 < progress * 100 && progress * 100 < (YellowLine.anchoredPosition.x + GreenLine.anchoredPosition.x + GreenLine.sizeDelta.x / 2) / 5 + 50)
            {
                pass++;
                LineLog.text = "Press \"B\"\nLevel of Difficulty -- " + (pass + 1).ToString();
                if (pass < 10)
                    ChangeLineSize(pass);
                ChangeLinePosition();
                Debug.Log("PASS!!!");
            }
            else if ((YellowLine.anchoredPosition.x - YellowLine.sizeDelta.x / 2) / 5 + 50 < progress * 100 && progress * 100 < (YellowLine.anchoredPosition.x + YellowLine.sizeDelta.x / 2) / 5 + 50)
            {
                medium++;
                ChangeLinePosition();
                Debug.Log("Medium");
            }
            else {
                fail++;
                Debug.Log("Fail");
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

    void ChangeLinePosition()
    {
        YellowLine.anchoredPosition = new Vector2(Random.Range(-RedLine_size / 2 + YellowLine.sizeDelta.x / 2, RedLine_size / 2 - YellowLine.sizeDelta.x / 2), 0);
        GreenLine.anchoredPosition = new Vector2(Random.Range(-YellowLine.sizeDelta.x / 2 + GreenLine.sizeDelta.x / 2, YellowLine.sizeDelta.x / 2 - GreenLine.sizeDelta.x / 2), 0);
    }

    void ChangeLineSize(int k)
    {
        YellowLine.sizeDelta = new Vector2(5 * YellowLine_size[k], 50.0f);
        GreenLine.sizeDelta = new Vector2(5 * GreenLine_size[k], 50.0f);
    }

    public void StopCraft()
    {
        Time.timeScale = 1;
        QTECraft = false;
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

public enum CraftType
{
    QTE,
    Line
}
