using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialoguesManager : MonoBehaviour
{
    public XMLparser DialoguesParser = new XMLparser();
    string lastGeneredButtonText;
    public int dialogueN = 1, ansN = 1;
    int count = 1, mCount=0;
    float offset=0;
    CamSwitcher camSwitcher;
    Controls controls;
    void Awake()
    {
        DialoguesParser.SetDoc("Dialogues/XMLdialogues");
        camSwitcher = GameObject.FindGameObjectWithTag("Player").GetComponent<CamSwitcher>();
        controls = GameObject.FindGameObjectWithTag("Player").GetComponent<Controls>();
    }
    public void StopDia()
    {
        dialogueN = 1;
        ansN = 1;
        camSwitcher.SwitchCamToMain();
        offset = 0;
        mCount = 0;
        StartCoroutine(canInteractNextFrame());
    }
    private void OnGUI()//TODO: MOVE TO UI.cs !!!!
    {
        if (camSwitcher.isInDia)
        {
            controls.canInteract = false;
            #region style
            GUI.skin.button.clipping = TextClipping.Clip;
            GUI.skin.box.fontSize = 21;
            GUI.skin.box.alignment = TextAnchor.UpperLeft;
            GUI.Box(new Rect((Screen.width - Screen.width * 0.8f) / 2, Screen.height * 0.55f,
                Screen.width * 0.8f, Screen.height * 0.44f), controls.NPCName + ":\n" + DialoguesParser.GetNPCReplic(dialogueN).Value);
            GUI.skin.button.alignment = TextAnchor.MiddleLeft;
            GUI.skin.button.fontSize = 14;
            GUI.skin.button.fontStyle = FontStyle.BoldAndItalic;
            GUI.backgroundColor = Color.clear;
            #endregion
            ansN = Convert.ToInt32(DialoguesParser.GetNPCReplic(dialogueN).Attribute("n").Value);
            count = 0;
            lastGeneredButtonText = null;
            for (int i = 1; i <= 10; i++)
            {
                if (Input.mousePosition.x >= (Screen.width - Screen.width * 0.78f) / 2 &&
                    Input.mousePosition.x <= Screen.width * 0.78f &&
                    Screen.height - Input.mousePosition.y >= Screen.height * (0.6f + (count + 1) * 0.065f) + offset &&
                    Screen.height - Input.mousePosition.y <= Screen.height * (0.6f + (count + 1) * 0.065f) + offset + Screen.height * 0.06f)
                {
                    GUI.contentColor = Color.green;
                }
                if (DialoguesParser.GetAnswers(ansN, i).Value != "empty" &&
                    DialoguesParser.GetAnswers(ansN, i).Value != lastGeneredButtonText)
                {
                    count++;
                    if (Screen.height * (0.6f + count * 0.065f) + offset + Screen.height * 0.06f >= Screen.height * 0.7f &&
                    Screen.height * (0.6f + count * 0.065f) + offset + Screen.height * 0.06f <= Screen.height * 0.99f)
                    {
                        lastGeneredButtonText = DialoguesParser.GetAnswers(ansN, i).Value;
                        if (GUI.Button(new Rect((Screen.width - Screen.width * 0.78f) / 2, Screen.height * (0.6f + count * 0.065f) + offset, //position
                        Screen.width * 0.78f, Screen.height * 0.06f), DialoguesParser.GetAnswers(ansN, i).Value))//size and content
                        {
                            dialogueN = Convert.ToInt32(DialoguesParser.GetAnswers(ansN, i).Attribute("to").Value);
                            if (dialogueN == 0)
                            {
                                StopDia();
                            }
                            count = 0;
                            offset = 0;
                            mCount = 0;
                            break;
                        }
                        GUI.contentColor = Color.white;
                    }
                }
                else if (Screen.height * (0.6f + (count + 1) * 0.065f) + offset + Screen.height * 0.06f >= Screen.height * 0.7f &&
                    Screen.height * (0.6f + count * 0.065f) + Screen.height * 0.06f + offset <= Screen.height * 0.99f)
                {
                    //count++;
                    if (GUI.Button(new Rect((Screen.width - Screen.width * 0.78f) / 2, Screen.height * (0.6f + (count + 1) * 0.065f) + offset, //position
                    Screen.width * 0.78f, Screen.height * 0.06f), "Exit"))
                    {
                        StopDia();
                    }
                    GUI.contentColor = Color.white;
                    break;
                }

            }
            if (count > mCount) mCount = count;
            GUI.backgroundColor = Color.black;
            GUI.VerticalScrollbar(new Rect(Screen.width * 0.9f, Screen.height * 0.55f, Screen.width * 0.95f, Screen.height * 0.44f),
                Screen.height * 0.55f - (offset * Screen.height * 0.4f / Mathf.Clamp(mCount - 4, 1, 10) / ((Screen.height * 0.06f) + 0.065f)),
                Screen.height * 0.4f / Mathf.Clamp(mCount - 4, 1, 10), Screen.height * 0.55f, Screen.height * 0.95f);
            offset += Input.mouseScrollDelta.y * 2;
            offset = Mathf.Clamp(offset, -((Screen.height * 0.06f) + 0.065f) * (mCount >= 4 ? mCount - 4 : 0), 0);
            count = 0;
        }
    }
    IEnumerator canInteractNextFrame()
    {
        yield return new WaitForEndOfFrame();
        controls.canInteract = true;
    }
}

