using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    GameObject controlsSettings;
    InputActionRebindingExtensions.RebindingOperation rebindOperation;
    Dictionary<string, InputAction> Bindings = new Dictionary<string, InputAction>();
    GameObject currButton;

    private void Awake()
    {
        controlsSettings = gameObject.transform.Find("ControlsSettings").gameObject;
    }
    private void Start()
    {
        UsefulThings.inputManager.Gameplay.Esc.performed += _ => gameObject.SetActive(false);
        Bindings.Add("OpenInventory", UsefulThings.inputManager.Gameplay.OpenInventory);
        Bindings.Add("SetDefaultCamPosition", UsefulThings.inputManager.Gameplay.SetDefaultCamPosition);
        Bindings.Add("CastFirstSpell", UsefulThings.inputManager.Spells.CastFirstSpell);
        Bindings.Add("CastSecondSpell", UsefulThings.inputManager.Spells.CastSecondSpell);
        Bindings.Add("CastThirdSpell", UsefulThings.inputManager.Spells.CastThirdSpell);
    }
    private void Update()
    {
        if(rebindOperation!=null)
            print(rebindOperation.completed);
    }
    public void ChangeControls(string action)
    {
        InputAction act = Bindings[action];
        UsefulThings.inputManager.Disable();
        currButton = UsefulThings.TransformSearch(transform, action).gameObject;
        RemapButtonClicked(act);

    }
    void RemapButtonClicked(InputAction actionToRebind)
    {
        rebindOperation = actionToRebind.PerformInteractiveRebinding()
                    // To avoid accidental input from mouse motion
                    .WithControlsExcluding("Mouse")
                    .OnMatchWaitForAnother(0.1f)
                    .Start();
        rebindOperation.OnComplete(OnComplete);
    }
    void OnComplete(InputActionRebindingExtensions.RebindingOperation rebindingOperation)
    {
        Text text = currButton.transform.GetComponentInChildren<Text>();
        string s = rebindingOperation.action.bindings[0].effectivePath;
        text.text = text.gameObject.transform.parent.name + " " + s.Substring(11, s.Length - 11);
        UsefulThings.inputManager.Enable();
        print("comleted");
    }
}
