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
    // 1 Aquarius
    public void displayAquarius() {
        Debug.Log("Aquarius displayed");
        _starFieldHolderScript.ToggleConstellation(0);
    }
    // 2 Aries
    public void displayAries() {
        _starFieldHolderScript.ToggleConstellation(1);
    }
    // 3 Cancer
    public void displayCancer() {
        _starFieldHolderScript.ToggleConstellation(2);
    }
    // 4 Capricorn
    public void displayCapricorn() {
        _starFieldHolderScript.ToggleConstellation(3);
    }
    // 5 Centaurus
    public void displayCentaurus() {
        _starFieldHolderScript.ToggleConstellation(4);
    }
    // 6 Cygnus
    public void displayCygnus() {
        _starFieldHolderScript.ToggleConstellation(5);
    }
    // 7 Gemini
    public void displayGemini() {
        _starFieldHolderScript.ToggleConstellation(6);
    }
    // 8 Hydra
    public void displayHydra() {
        _starFieldHolderScript.ToggleConstellation(7);
    }
    // 9 Leo
    public void displayLeo() {
        _starFieldHolderScript.ToggleConstellation(8);
    }
    // 10 Leo Minor
    public void displayLeoMinor() {
        _starFieldHolderScript.ToggleConstellation(10);
    }
    // 11 Libra
    public void displayLibra() {
        _starFieldHolderScript.ToggleConstellation(11);
    }
    // 12 Lynx
    public void displayLynx() {
        _starFieldHolderScript.ToggleConstellation(12);
    }
    // 13 Monceros
    public void displayMonceros() {
        _starFieldHolderScript.ToggleConstellation(13);
    }
    // 14 Orion
    public void displayOrion() {
        _starFieldHolderScript.ToggleConstellation(14);
    }
    // 15 Pisces
    public void displayPisces() {
        _starFieldHolderScript.ToggleConstellation(15);
    }
    // 16 Sagittarius
    public void displaySagittarius() {
        _starFieldHolderScript.ToggleConstellation(16);
    }
    // 17 Scorpius
    public void displayScorpius() {
        _starFieldHolderScript.ToggleConstellation(17);
    }
    // 18 Southern Crux
    public void displaySouthernCrux() {
        _starFieldHolderScript.ToggleConstellation(18);
    }
    // 19 Taurus
    public void displayTaurus() {
        _starFieldHolderScript.ToggleConstellation(19);
    }
    // 20 Ursa Major
    public void displayUrsaMajor() {
        _starFieldHolderScript.ToggleConstellation(20);
    }
    // 21 Virgo
    public void displayVirgo() {
        _starFieldHolderScript.ToggleConstellation(21);
    }


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
