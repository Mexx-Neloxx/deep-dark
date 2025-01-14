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


    private VisualElement _root, _ProgBarHP;
    
    private MovementController _player;

    private EnemyGivenDamageScript _HP;
  
    

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
        _HP = FindAnyObjectByType<EnemyGivenDamageScript>();

        _root = UI.rootVisualElement;
        Input.actions["PauseMenu"].performed += HUD_performed; ;
        Input.actions["ContinueGame"].performed += HUD_performed;

        _root.Q<Button>("Btn_Continue").clicked += HUD_Continue;
        _root.Q<Button>("Btn_SaveGame").clicked += HUD_SaveGame;
        _root.Q<Button>("Btn_LoadGame").clicked += HUD_LoadGame;
        _root.Q<Button>("Btn_MainMenu").clicked += HUD_MainMenu;

        _ProgBarHP = _root.Q<VisualElement>("VisElem_HPBar");
        
    }

    // в главное меню
    private void HUD_MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        OnDisable();
    }

    // загрузить игру
    private void HUD_LoadGame()
    {
        var player = FindAnyObjectByType<MovementController>();       
        var saveData = DataManager.Load();

        if (saveData == null) return;
        player.transform.position = saveData.PlayerPosition;
        player.transform.rotation = saveData.LookPosition;
        
    }

    // сохранить игру
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

    // продолжить
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

    // переключение между паузой и игрой
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
        _root.Q<Label>("Label_HP").text = "HP: " + _HP.HP.ToString();
        _ProgBarHP.style.width = new StyleLength(Convert.ToInt32(_HP.HP));

        if (_HP.HP <= (100 * 0.3))
        {
            _ProgBarHP.style.backgroundColor = new StyleColor(Color.red);

        }
        else if (_HP.HP <= (100 * 0.6))
        {
            _ProgBarHP.style.backgroundColor = new StyleColor(Color.yellow);
        }
        else if (_HP.HP > (100 * 0.6))
        {
            _ProgBarHP.style.backgroundColor = new StyleColor(Color.green);
        }

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
