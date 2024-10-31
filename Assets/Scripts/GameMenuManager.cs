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

    //Constellation locator stuff
    [SerializeField]
    private GameObject _page1;
    [SerializeField]
    private GameObject _page2;
    [SerializeField]
    private GameObject _page3;
    [SerializeField]
    private GameObject _prev;
    int _constellationPage = 1;

    [SerializeField]
    private AudioSource _audioSource;


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

    private void DisplayIndividualConstellation(int index) {
        if (_starFieldHolderScript.GetIsAllVisible() == false) {
            // check if there is another constellation other than this one visible?
            if (_starFieldHolderScript.GetIsIndividualVisible()) { // yes, there is an individual constellation visible
                int _visIndex = _starFieldHolderScript.WhichIndexIsVisible();
                if (_visIndex == index) { // is it this one?
                    // yes, then untoggle it
                    _starFieldHolderScript.ToggleConstellation(_visIndex);
                    _starFieldHolderScript.SetIsIndividualVisible(false);
                    _starFieldHolderScript.SetIsVisibleArray(false, _visIndex);
                } else {
                    // no, then untoggle the other constellation... 
                    _starFieldHolderScript.ToggleConstellation(_visIndex);
                    _starFieldHolderScript.SetIsVisibleArray(false, _visIndex);
                    // ... and toggle this one
                    _starFieldHolderScript.ToggleConstellation(index);
                    _starFieldHolderScript.SetIsVisibleArray(true, index);
                }
            } else { // no there is no constellation individually visible
                _starFieldHolderScript.ToggleConstellation(index);
                _starFieldHolderScript.SetIsIndividualVisible(true);
                _starFieldHolderScript.SetIsVisibleArray(true, index);
            }
        }
    }

    public void PlayUIAudio() {
        _audioSource.Play();
    }

    //Constellation Functions
    // 1 Aquarius
    public void displayAquarius() {
        DisplayIndividualConstellation(0);
    }
    // 2 Aries
    public void displayAries() {
        DisplayIndividualConstellation(1);
    }
    // 3 Cancer
    public void displayCancer() {
        DisplayIndividualConstellation(2);
    }
    // 4 Capricorn
    public void displayCapricorn() {
        DisplayIndividualConstellation(3);
    }
    // 5 Centaurus
    public void displayCentaurus() {
        DisplayIndividualConstellation(4);
    }
    // 6 Cygnus
    public void displayCygnus() {
        DisplayIndividualConstellation(5);
    }
    // 7 Gemini
    public void displayGemini() {
        DisplayIndividualConstellation(6);
    }
    // 8 Hydra
    public void displayHydra() {
        DisplayIndividualConstellation(7);
    }
    // 9 Leo
    public void displayLeo() {
        DisplayIndividualConstellation(8);
    }
    // 10 Leo Minor
    public void displayLeoMinor() {
        DisplayIndividualConstellation(10);
    }
    // 11 Libra
    public void displayLibra() {
        DisplayIndividualConstellation(11);
    }
    // 12 Lynx
    public void displayLynx() {
        DisplayIndividualConstellation(12);
    }
    // 13 Monceros
    public void displayMonceros() {
        DisplayIndividualConstellation(13);
    }
    // 14 Orion
    public void displayOrion() {
        DisplayIndividualConstellation(14);
    }
    // 15 Pisces
    public void displayPisces() {
        DisplayIndividualConstellation(15);
    }
    // 16 Sagittarius
    public void displaySagittarius() {
        DisplayIndividualConstellation(16);
    }
    // 17 Scorpius
    public void displayScorpius() {
        DisplayIndividualConstellation(17);
    }
    // 18 Southern Crux
    public void displaySouthernCrux() {
        DisplayIndividualConstellation(18);
    }
    // 19 Taurus
    public void displayTaurus() {
        DisplayIndividualConstellation(19);
    }
    // 20 Ursa Major
    public void displayUrsaMajor() {
        DisplayIndividualConstellation(20);
    }
    // 21 Virgo
    public void displayVirgo() {
        DisplayIndividualConstellation(21);
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

    // Constellation menu stuff
    public void OpenConstellationMenu() {
        InitialiseConstellationMenu();
        _mainMenuUI.SetActive(false);
        _quitConfirmUI.SetActive(false);
        _constellationLocatorUI.SetActive(true);
        _spaceEventsUI.SetActive(false);
        _settingsUI.SetActive(false);
    }

    private void InitialiseConstellationMenu() {
        _page1.SetActive(true);
        _page2.SetActive(false);
        _page3.SetActive(false);
        _prev.SetActive(false);
    }

    public void NextConstellationMenu() {
        if(_constellationPage < 3) {
            _constellationPage++;
        }
        switch (_constellationPage) {
            case 1:
                _page1.SetActive(true);
                _page2.SetActive(false);
                _page3.SetActive(false);
                _prev.SetActive(false);
                break;
            case 2:
                _page1.SetActive(false);
                _page2.SetActive(true);
                _page3.SetActive(false);
                _prev.SetActive(true);
                break;
            case 3:
                _page1.SetActive(false);
                _page2.SetActive(false);
                _page3.SetActive(true);
                _prev.SetActive(true);
                break;
            default: 
                break;
        }
    }

    public void PrevConstellationMenu() {
        if (_constellationPage > 1) {
            _constellationPage--;
        }
        switch (_constellationPage) {
            case 1:
                _page1.SetActive(true);
                _page2.SetActive(false);
                _page3.SetActive(false);
                _prev.SetActive(false);
                break;
            case 2:
                _page1.SetActive(false);
                _page2.SetActive(true);
                _page3.SetActive(false);
                _prev.SetActive(true);
                break;
            case 3:
                _page1.SetActive(false);
                _page2.SetActive(false);
                _page3.SetActive(true);
                _prev.SetActive(true);
                break;
            default:
                break;
        }
    }

    // End of constellation menu stuff

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
