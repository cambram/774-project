using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

public class GameMenuManager : MonoBehaviour
{
    public GameObject _menu;
    private InputData _inputData;
    private bool _wasMenuButtonPressed = false;
    private bool _isMenuVisible = false;
    [SerializeField]
    private Camera _head;
    public float _spawnDistance = 1.5f;

    [SerializeField]
    private GameObject _starFieldHolder;
    private StarField _starFieldHolderScript;

    //UI game objects
    [SerializeField]
    private GameObject _mainMenuUI;
    [SerializeField]
    private GameObject _constellationLocatorUI;
    [SerializeField]
    private GameObject _quitConfirmUI;
    [SerializeField]
    private GameObject _spaceEventsUI;
    [SerializeField]
    private GameObject _settingsUI;

    void Start() {
        // This is to retrieve the InputData script in order to communicate with the quest controllers
        _starFieldHolderScript = _starFieldHolder.GetComponent<StarField>();
        _inputData = GetComponent<InputData>();
        _menu.SetActive(false);
        InitialiseMenu();
    }

    // Update is called once per frame
    void Update(){
        if (_inputData._leftController.TryGetFeatureValue(CommonUsages.menuButton, out bool pressed)) {
            if (pressed && !_wasMenuButtonPressed) {
                _isMenuVisible = !_isMenuVisible;
                _menu.transform.position = _head.transform.position + new Vector3(_head.transform.forward.x, 0, _head.transform.forward.z).normalized * _spawnDistance;
                InitialiseMenu();
                _menu.SetActive(_isMenuVisible);
            }
            // Update the previous state to the current state
            _wasMenuButtonPressed = pressed;
        }

        _menu.transform.LookAt(new Vector3(_head.transform.position.x, _menu.transform.position.y, _head.transform.position.z));
        _menu.transform.forward *= -1;
    }

    //Constellation Functions

    //Quit Functions
    public void QuitExperience() {
        Application.Quit();
    }

    //Navigation Functions
    private void InitialiseMenu() {
        _mainMenuUI.SetActive(true);
        _quitConfirmUI.SetActive(false);
        _constellationLocatorUI.SetActive(false);
        _spaceEventsUI.SetActive(false);
        _settingsUI.SetActive(false);
    }

    public void OpenQuitMenu() {
        _mainMenuUI.SetActive(false);
        _quitConfirmUI.SetActive(true);
        _constellationLocatorUI.SetActive(false);
        _spaceEventsUI.SetActive(false);
        _settingsUI.SetActive(false);
    }
    public void OpenConstellationMenu() {
        _mainMenuUI.SetActive(false);
        _quitConfirmUI.SetActive(false);
        _constellationLocatorUI.SetActive(true);
        _spaceEventsUI.SetActive(false);
        _settingsUI.SetActive(false);
    }
    public void OpenSpaceEventsMenu() {
        _mainMenuUI.SetActive(false);
        _quitConfirmUI.SetActive(false);
        _constellationLocatorUI.SetActive(false);
        _spaceEventsUI.SetActive(true);
        _settingsUI.SetActive(false);
    }

    public void OpenSettingsMenu() {
        _mainMenuUI.SetActive(false);
        _quitConfirmUI.SetActive(false);
        _constellationLocatorUI.SetActive(false);
        _spaceEventsUI.SetActive(false);
        _settingsUI.SetActive(true);
    }

    public void BackToMainMenu() {
        _mainMenuUI.SetActive(true);
        _quitConfirmUI.SetActive(false);
        _constellationLocatorUI.SetActive(false);
        _spaceEventsUI.SetActive(false);
        _settingsUI.SetActive(false);
    }
}
