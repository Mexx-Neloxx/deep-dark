using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static DataManager;
using static UnityEngine.EventSystems.EventTrigger;

public class HUD : MonoBehaviour
{
    public PlayerInput Input;
    public UIDocument UI;
    public DataManager DataManager;


    private VisualElement _root;
    
    private MovementController _player;
  
    private ProgressBar _ProgBarHP;

    void Start()
    {

        if (GameData.NeedLoadGame)
        {
            HUD_LoadGame();
            Debug.Log("NeedLoadGame");
            GameData.NeedLoadGame = false;
        }

        Input.SwitchCurrentActionMap("Player");
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;


        _player = FindAnyObjectByType<MovementController>();

        _root = UI.rootVisualElement;
        Input.actions["PauseMenu"].performed += HUD_performed; ;
        Input.actions["ContinueGame"].performed += HUD_performed;

        _root.Q<Button>("Btn_Continue").clicked += HUD_Continue;
        _root.Q<Button>("Btn_SaveGame").clicked += HUD_SaveGame;
        _root.Q<Button>("Btn_LoadGame").clicked += HUD_LoadGame;
        _root.Q<Button>("Btn_MainMenu").clicked += HUD_MainMenu;

        _ProgBarHP = _root.Q<ProgressBar>("PB_HP");
    }

    // � ������� ����
    private void HUD_MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        OnDisable();
    }

    // ��������� ����
    private void HUD_LoadGame()
    {
        var player = FindAnyObjectByType<MovementController>();       
        var saveData = DataManager.Load();

        if (saveData == null) return;
        player.transform.position = saveData.PlayerPosition;
        player.transform.rotation = saveData.LookPosition;
        
    }

    // ��������� ����
    private void HUD_SaveGame()
    {
        var player = FindAnyObjectByType<MovementController>();
        var saveData = new GameData()
        {          
            PlayerPosition = player.transform.position,
            LookPosition = player.transform.rotation
          
        };

        DataManager.Save(saveData);
    }

    // ����������
    private void HUD_Continue()
    {
        var menu = _root.Q<VisualElement>("VisElem_Pause");
        var hud = _root.Q<VisualElement>("VisElem_InGame");

        Input.SwitchCurrentActionMap("Player");
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
        menu.style.display = DisplayStyle.None;
        hud.style.display = DisplayStyle.Flex;
    }

    // ������������ ����� ������ � �����
    private void HUD_performed(InputAction.CallbackContext obj)
    {
        var menu = _root.Q<VisualElement>("VisElem_Pause");
        var hud = _root.Q<VisualElement>("VisElem_InGame");

        if (menu.style.display == DisplayStyle.None)
        {
            Input.SwitchCurrentActionMap("UI");
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;
            menu.style.display = DisplayStyle.Flex;
            hud.style.display = DisplayStyle.None;
        }
        else
        {
            Input.SwitchCurrentActionMap("Player");
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Cursor.visible = false;
            menu.style.display = DisplayStyle.None;
            hud.style.display = DisplayStyle.Flex;
        }
    }

 
    void Update()
    {
        
    }

    private void OnDisable()
    {
        Input.actions["PauseMenu"].performed -= HUD_performed;
        Input.actions["ContinueGame"].performed -= HUD_performed;

        _root.Q<Button>("Btn_Continue").clicked -= HUD_Continue;
        _root.Q<Button>("Btn_SaveGame").clicked -= HUD_SaveGame;
        _root.Q<Button>("Btn_LoadGame").clicked -= HUD_LoadGame;
        _root.Q<Button>("Btn_MainMenu").clicked -= HUD_MainMenu;
    }
}
