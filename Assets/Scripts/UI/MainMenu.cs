using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static DataManager;

public class MainMenu : MonoBehaviour
{
    public PlayerInput Input;
    public UIDocument UI;
    public DataManager DataManager;

    private VisualElement _root;

    void Start()
    {
        _root = UI.rootVisualElement;
        Input.SwitchCurrentActionMap("UI");

        //элементы главного меню
        _root.Q<Button>("Btn_StartGame").clicked += MainMenu_StartGame;
        _root.Q<Button>("Btn_LoadGame").clicked += MainMenu_LoadGame;
        _root.Q<Button>("Btn_Settings").clicked += MainMenu_Settings;
        _root.Q<Button>("Btn_Quit").clicked += MainMenu_Quit;

        //элементы окна настроек
        _root.Q<Button>("Btn_ExitSettings").clicked += MainMenu_ExitSettings;

    }

    // выход из меню настроек
    private void MainMenu_ExitSettings()
    {
        var mainMenu = _root.Q<VisualElement>("VisElem_MainMenu");
        var settings = _root.Q<VisualElement>("VisElem_Settings");

        settings.style.display = DisplayStyle.None;
        mainMenu.style.display = DisplayStyle.Flex;

    }

    // выход из игры
    private void MainMenu_Quit()
    {
        Application.Quit();
    }

    // меню настроек
    private void MainMenu_Settings()
    {
        var mainMenu = _root.Q<VisualElement>("VisElem_MainMenu");
        var settings = _root.Q<VisualElement>("VisElem_Settings");

        mainMenu.style.display = DisplayStyle.None;
        settings.style.display = DisplayStyle.Flex;
    }

    // загрузить игру
    private void MainMenu_LoadGame()
    {
        GameData.NeedLoadGame = true;
        Input.SwitchCurrentActionMap("Player");
        OnDisable();
        SceneManager.LoadScene("scene");
    }

    // начать игру
    private void MainMenu_StartGame()
    {
        Input.SwitchCurrentActionMap("Player");
        OnDisable();
        SceneManager.LoadScene("scene");
    }

    void Update()
    {
        
    }

    private void OnDisable()
    {
        //элементы главного меню
        _root.Q<Button>("Btn_StartGame").clicked -= MainMenu_StartGame;
        _root.Q<Button>("Btn_LoadGame").clicked -= MainMenu_LoadGame;
        _root.Q<Button>("Btn_Settings").clicked -= MainMenu_Settings;
        _root.Q<Button>("Btn_Quit").clicked -= MainMenu_Quit;

        //элементы окна настроек
        _root.Q<Button>("Btn_ExitSettings").clicked -= MainMenu_ExitSettings;
    }
}
