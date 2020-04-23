// GENERATED AUTOMATICALLY FROM 'Assets/InputManager.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputManager : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputManager()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputManager"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""667ce29a-5b41-440e-bfc4-40968f91361e"",
            ""actions"": [
                {
                    ""name"": ""CreateItem"",
                    ""type"": ""Button"",
                    ""id"": ""54b18208-452f-46a8-a33a-d87f54d759d2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""OpenInventory"",
                    ""type"": ""Button"",
                    ""id"": ""c2d2bf11-141f-4461-939a-3d4564d78221"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SetDefaultCamPosition"",
                    ""type"": ""Button"",
                    ""id"": ""7b3e48cb-a4bd-4d84-a460-ee5c566418b1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ChangeMinimapSize"",
                    ""type"": ""Value"",
                    ""id"": ""f32e2e7c-f1eb-4b6a-b664-2133fd7110dc"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""HideMiniMap"",
                    ""type"": ""Button"",
                    ""id"": ""1dbae946-7a80-4241-8834-38e9dd7cfd3c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Mouse"",
                    ""type"": ""Value"",
                    ""id"": ""dd375973-4b4b-4378-8d55-2f92c26a4b87"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Esc"",
                    ""type"": ""Button"",
                    ""id"": ""cd5a2da2-ab7c-4242-807a-d8236f249f81"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""09c951ae-7f02-46c7-a5c9-60fc3ee05c2b"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenInventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8a6ec33f-0c41-4eeb-98fa-f8338335132c"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SetDefaultCamPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""d7db199e-6f03-43cb-8862-6490a3fb4218"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeMinimapSize"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""5dbb3c83-aad3-4de5-bee1-b7b07a7ef6c6"",
                    ""path"": ""<Keyboard>/minus"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeMinimapSize"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""25fd406a-59bf-4e58-bea3-fa92053948ca"",
                    ""path"": ""<Keyboard>/equals"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeMinimapSize"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""109f28b5-8d79-40c1-9e3e-d98c1fab2d14"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeMinimapSize"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""796e658e-03ab-4522-ac9e-2a4d3e8280b2"",
                    ""path"": ""<Keyboard>/numpadMinus"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeMinimapSize"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""d9989e48-bf18-45ea-9217-9bf09bf21cba"",
                    ""path"": ""<Keyboard>/numpadPlus"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeMinimapSize"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""4315e9c8-9990-4dff-bff3-ac53a77b2ffb"",
                    ""path"": ""<Keyboard>/m"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HideMiniMap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6cf9614d-b9f3-47e2-8123-716e2c80fc6f"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""07e077a1-8221-4734-8f76-b97f1d76c25e"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CreateItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a2f5f70a-8ae5-4939-b15f-51141b947f6d"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Esc"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Spells"",
            ""id"": ""3d2ee0b4-5438-425b-991d-ea7693b3a8ea"",
            ""actions"": [
                {
                    ""name"": ""CastFirstSpell"",
                    ""type"": ""Button"",
                    ""id"": ""9c0f6d23-2d38-447e-9af4-41b7735e7b50"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CastSecondSpell"",
                    ""type"": ""Button"",
                    ""id"": ""5efa4e2b-0722-4621-a12c-904373b92502"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CastThirdSpell"",
                    ""type"": ""Button"",
                    ""id"": ""21241ef7-9e16-4d5a-b8ed-17658f2c9acd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""3c6bcd23-bc50-4de7-bdf4-6d2eb005bd24"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CastFirstSpell"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7d1ec542-0c84-4a4c-a2c3-af4e86abbc5f"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CastSecondSpell"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""24a9059f-c0b7-4a2a-b13e-dc3aed65da14"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CastThirdSpell"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_CreateItem = m_Gameplay.FindAction("CreateItem", throwIfNotFound: true);
        m_Gameplay_OpenInventory = m_Gameplay.FindAction("OpenInventory", throwIfNotFound: true);
        m_Gameplay_SetDefaultCamPosition = m_Gameplay.FindAction("SetDefaultCamPosition", throwIfNotFound: true);
        m_Gameplay_ChangeMinimapSize = m_Gameplay.FindAction("ChangeMinimapSize", throwIfNotFound: true);
        m_Gameplay_HideMiniMap = m_Gameplay.FindAction("HideMiniMap", throwIfNotFound: true);
        m_Gameplay_Mouse = m_Gameplay.FindAction("Mouse", throwIfNotFound: true);
        m_Gameplay_Esc = m_Gameplay.FindAction("Esc", throwIfNotFound: true);
        // Spells
        m_Spells = asset.FindActionMap("Spells", throwIfNotFound: true);
        m_Spells_CastFirstSpell = m_Spells.FindAction("CastFirstSpell", throwIfNotFound: true);
        m_Spells_CastSecondSpell = m_Spells.FindAction("CastSecondSpell", throwIfNotFound: true);
        m_Spells_CastThirdSpell = m_Spells.FindAction("CastThirdSpell", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_CreateItem;
    private readonly InputAction m_Gameplay_OpenInventory;
    private readonly InputAction m_Gameplay_SetDefaultCamPosition;
    private readonly InputAction m_Gameplay_ChangeMinimapSize;
    private readonly InputAction m_Gameplay_HideMiniMap;
    private readonly InputAction m_Gameplay_Mouse;
    private readonly InputAction m_Gameplay_Esc;
    public struct GameplayActions
    {
        private @InputManager m_Wrapper;
        public GameplayActions(@InputManager wrapper) { m_Wrapper = wrapper; }
        public InputAction @CreateItem => m_Wrapper.m_Gameplay_CreateItem;
        public InputAction @OpenInventory => m_Wrapper.m_Gameplay_OpenInventory;
        public InputAction @SetDefaultCamPosition => m_Wrapper.m_Gameplay_SetDefaultCamPosition;
        public InputAction @ChangeMinimapSize => m_Wrapper.m_Gameplay_ChangeMinimapSize;
        public InputAction @HideMiniMap => m_Wrapper.m_Gameplay_HideMiniMap;
        public InputAction @Mouse => m_Wrapper.m_Gameplay_Mouse;
        public InputAction @Esc => m_Wrapper.m_Gameplay_Esc;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @CreateItem.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCreateItem;
                @CreateItem.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCreateItem;
                @CreateItem.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCreateItem;
                @OpenInventory.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnOpenInventory;
                @OpenInventory.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnOpenInventory;
                @OpenInventory.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnOpenInventory;
                @SetDefaultCamPosition.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSetDefaultCamPosition;
                @SetDefaultCamPosition.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSetDefaultCamPosition;
                @SetDefaultCamPosition.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSetDefaultCamPosition;
                @ChangeMinimapSize.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnChangeMinimapSize;
                @ChangeMinimapSize.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnChangeMinimapSize;
                @ChangeMinimapSize.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnChangeMinimapSize;
                @HideMiniMap.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnHideMiniMap;
                @HideMiniMap.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnHideMiniMap;
                @HideMiniMap.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnHideMiniMap;
                @Mouse.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouse;
                @Mouse.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouse;
                @Mouse.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouse;
                @Esc.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnEsc;
                @Esc.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnEsc;
                @Esc.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnEsc;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @CreateItem.started += instance.OnCreateItem;
                @CreateItem.performed += instance.OnCreateItem;
                @CreateItem.canceled += instance.OnCreateItem;
                @OpenInventory.started += instance.OnOpenInventory;
                @OpenInventory.performed += instance.OnOpenInventory;
                @OpenInventory.canceled += instance.OnOpenInventory;
                @SetDefaultCamPosition.started += instance.OnSetDefaultCamPosition;
                @SetDefaultCamPosition.performed += instance.OnSetDefaultCamPosition;
                @SetDefaultCamPosition.canceled += instance.OnSetDefaultCamPosition;
                @ChangeMinimapSize.started += instance.OnChangeMinimapSize;
                @ChangeMinimapSize.performed += instance.OnChangeMinimapSize;
                @ChangeMinimapSize.canceled += instance.OnChangeMinimapSize;
                @HideMiniMap.started += instance.OnHideMiniMap;
                @HideMiniMap.performed += instance.OnHideMiniMap;
                @HideMiniMap.canceled += instance.OnHideMiniMap;
                @Mouse.started += instance.OnMouse;
                @Mouse.performed += instance.OnMouse;
                @Mouse.canceled += instance.OnMouse;
                @Esc.started += instance.OnEsc;
                @Esc.performed += instance.OnEsc;
                @Esc.canceled += instance.OnEsc;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);

    // Spells
    private readonly InputActionMap m_Spells;
    private ISpellsActions m_SpellsActionsCallbackInterface;
    private readonly InputAction m_Spells_CastFirstSpell;
    private readonly InputAction m_Spells_CastSecondSpell;
    private readonly InputAction m_Spells_CastThirdSpell;
    public struct SpellsActions
    {
        private @InputManager m_Wrapper;
        public SpellsActions(@InputManager wrapper) { m_Wrapper = wrapper; }
        public InputAction @CastFirstSpell => m_Wrapper.m_Spells_CastFirstSpell;
        public InputAction @CastSecondSpell => m_Wrapper.m_Spells_CastSecondSpell;
        public InputAction @CastThirdSpell => m_Wrapper.m_Spells_CastThirdSpell;
        public InputActionMap Get() { return m_Wrapper.m_Spells; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(SpellsActions set) { return set.Get(); }
        public void SetCallbacks(ISpellsActions instance)
        {
            if (m_Wrapper.m_SpellsActionsCallbackInterface != null)
            {
                @CastFirstSpell.started -= m_Wrapper.m_SpellsActionsCallbackInterface.OnCastFirstSpell;
                @CastFirstSpell.performed -= m_Wrapper.m_SpellsActionsCallbackInterface.OnCastFirstSpell;
                @CastFirstSpell.canceled -= m_Wrapper.m_SpellsActionsCallbackInterface.OnCastFirstSpell;
                @CastSecondSpell.started -= m_Wrapper.m_SpellsActionsCallbackInterface.OnCastSecondSpell;
                @CastSecondSpell.performed -= m_Wrapper.m_SpellsActionsCallbackInterface.OnCastSecondSpell;
                @CastSecondSpell.canceled -= m_Wrapper.m_SpellsActionsCallbackInterface.OnCastSecondSpell;
                @CastThirdSpell.started -= m_Wrapper.m_SpellsActionsCallbackInterface.OnCastThirdSpell;
                @CastThirdSpell.performed -= m_Wrapper.m_SpellsActionsCallbackInterface.OnCastThirdSpell;
                @CastThirdSpell.canceled -= m_Wrapper.m_SpellsActionsCallbackInterface.OnCastThirdSpell;
            }
            m_Wrapper.m_SpellsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @CastFirstSpell.started += instance.OnCastFirstSpell;
                @CastFirstSpell.performed += instance.OnCastFirstSpell;
                @CastFirstSpell.canceled += instance.OnCastFirstSpell;
                @CastSecondSpell.started += instance.OnCastSecondSpell;
                @CastSecondSpell.performed += instance.OnCastSecondSpell;
                @CastSecondSpell.canceled += instance.OnCastSecondSpell;
                @CastThirdSpell.started += instance.OnCastThirdSpell;
                @CastThirdSpell.performed += instance.OnCastThirdSpell;
                @CastThirdSpell.canceled += instance.OnCastThirdSpell;
            }
        }
    }
    public SpellsActions @Spells => new SpellsActions(this);
    public interface IGameplayActions
    {
        void OnCreateItem(InputAction.CallbackContext context);
        void OnOpenInventory(InputAction.CallbackContext context);
        void OnSetDefaultCamPosition(InputAction.CallbackContext context);
        void OnChangeMinimapSize(InputAction.CallbackContext context);
        void OnHideMiniMap(InputAction.CallbackContext context);
        void OnMouse(InputAction.CallbackContext context);
        void OnEsc(InputAction.CallbackContext context);
    }
    public interface ISpellsActions
    {
        void OnCastFirstSpell(InputAction.CallbackContext context);
        void OnCastSecondSpell(InputAction.CallbackContext context);
        void OnCastThirdSpell(InputAction.CallbackContext context);
    }
}
