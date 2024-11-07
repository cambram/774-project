using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class UIManager : MonoBehaviour
{
    public GameObject _menu;
    private InputData _inputData;
    private bool _wasMenuButtonPressed = false;
    private bool _isMenuVisible = false;
    public Camera _head;
    private float _spawnDistance = 1f;

    //Getting starfield data to display individual constellations
    public GameObject _starFieldHolder;
    private StarField _starFieldHolderScript;

    //UI left panel buttons to change colour
    public GameObject _txtConstellationLocator;
    public GameObject _imgConstellationLocator;
    public GameObject _txtSettings;
    public GameObject _imgSettings;
    public GameObject _txtControls;
    public GameObject _imgControls;
    public GameObject _txtQuit;
    public GameObject _imgQuit;

    //Images of icons
    public Sprite _yellowArrow;
    public Sprite _greyArrow;
    public Sprite _yellowLogout;
    public Sprite _greyLogout;

    //UI menu items/pages
    public GameObject _constellationLocatorUI;
    public GameObject _settingsUI;
    public GameObject _controlsUI;
    public GameObject _quitUI;

    //Constellation UI pages
    public GameObject _page1;
    public GameObject _page2;
    public GameObject _page3;
    private int _pageNumber = 1;

    //Settings UI pages
    public GameObject _mainSettingsPage;
    public GameObject _volumePage;

    //Colours
    private Color _offYellow = new Color(0.9529411764705882f, 0.8588235294117647f, 0.4823529411764706f, 1.0f);
    private Color _offGrey = new Color(0.6196078431372549f, 0.6235294117647059f, 0.6431372549019608f, 1.0f);

    // Start is called before the first frame update
    void Start(){
        // This is to retrieve the InputData script in order to communicate with the quest controllers
        _starFieldHolderScript = _starFieldHolder.GetComponent<StarField>();
        _inputData = GetComponent<InputData>();
        _menu.SetActive(false);
        OpenConstellationLocator();
    }

    // Update is called once per frame
    void Update(){
        if (_inputData._leftController.TryGetFeatureValue(CommonUsages.menuButton, out bool pressed)) {
            if (pressed && !_wasMenuButtonPressed) {
                _isMenuVisible = !_isMenuVisible;
                _menu.transform.position = _head.transform.position + new Vector3(_head.transform.forward.x, 0, _head.transform.forward.z).normalized * _spawnDistance;
                OpenConstellationLocator();
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

    //Constellation page 
    public void NextButton() {
        switch (_pageNumber) {
            case 1:
                _page1.SetActive(false);
                _page2.SetActive(true);
                _page3.SetActive(false);
                _pageNumber = 2;
                break;
            case 2:
                _page1.SetActive(false);
                _page2.SetActive(false);
                _page3.SetActive(true);
                _pageNumber = 3;
                break;
            case 3:
                _page1.SetActive(true);
                _page2.SetActive(false);
                _page3.SetActive(false);
                _pageNumber = 1;
                break;
            default:
                break;
        }
    }

    public void PrevButton() {
        switch (_pageNumber) {
            case 1:
                _page1.SetActive(false);
                _page2.SetActive(false);
                _page3.SetActive(true);
                _pageNumber = 3;
                break;
            case 2:
                _page1.SetActive(true);
                _page2.SetActive(false);
                _page3.SetActive(false);
                _pageNumber = 1;
                break;
            case 3:
                _page1.SetActive(false);
                _page2.SetActive(true);
                _page3.SetActive(false);
                _pageNumber = 2;
                break;
            default:
                break;
        }
    }

    //Settings page 
    public void OpenVolume() {
        _mainSettingsPage.SetActive(false);
        _volumePage.SetActive(true);
    }

    public void ResetView() {

    }

    //Navigation Functions
    public void OpenConstellationLocator() {
        _constellationLocatorUI.SetActive(true);
        _settingsUI.SetActive(false);
        _controlsUI.SetActive(false);
        _quitUI.SetActive(false);

        _page1.SetActive(true);
        _page2.SetActive(false);
        _page3.SetActive(false);

        //Change left panel buttons
        _txtConstellationLocator.GetComponent<Text>().color = _offYellow;
        _imgConstellationLocator.GetComponent<Image>().sprite = _yellowArrow;

        _txtSettings.GetComponent<Text>().color = _offGrey;
        _imgSettings.GetComponent<Image>().sprite = _greyArrow;

        _txtControls.GetComponent<Text>().color = _offGrey;
        _imgControls.GetComponent<Image>().sprite = _greyArrow;

        _txtQuit.GetComponent<Text>().color = _offGrey;
        _imgQuit.GetComponent<Image>().sprite = _greyLogout;
    }

    public void OpenSettings() {
        _constellationLocatorUI.SetActive(false);
        _settingsUI.SetActive(true);
        _controlsUI.SetActive(false);
        _quitUI.SetActive(false);

        _mainSettingsPage.SetActive(true);
        _volumePage.SetActive(false);

        //Change left panel buttons
        _txtConstellationLocator.GetComponent<Text>().color = _offGrey;
        _imgConstellationLocator.GetComponent<Image>().sprite = _greyArrow;

        _txtSettings.GetComponent<Text>().color = _offYellow;
        _imgSettings.GetComponent<Image>().sprite = _yellowArrow;

        _txtControls.GetComponent<Text>().color = _offGrey;
        _imgControls.GetComponent<Image>().sprite = _greyArrow;

        _txtQuit.GetComponent<Text>().color = _offGrey;
        _imgQuit.GetComponent<Image>().sprite = _greyLogout;
    }

    public void OpenControls() {
        _constellationLocatorUI.SetActive(false);
        _settingsUI.SetActive(false);
        _controlsUI.SetActive(true);
        _quitUI.SetActive(false);

        //Change left panel buttons
        _txtConstellationLocator.GetComponent<Text>().color = _offGrey;
        _imgConstellationLocator.GetComponent<Image>().sprite = _greyArrow;

        _txtSettings.GetComponent<Text>().color = _offGrey;
        _imgSettings.GetComponent<Image>().sprite = _greyArrow;

        _txtControls.GetComponent<Text>().color = _offYellow;
        _imgControls.GetComponent<Image>().sprite = _yellowArrow;

        _txtQuit.GetComponent<Text>().color = _offGrey;
        _imgQuit.GetComponent<Image>().sprite = _greyLogout;
    }

    public void OpenQuitMenu() {
        _constellationLocatorUI.SetActive(false);
        _settingsUI.SetActive(false);
        _controlsUI.SetActive(false);
        _quitUI.SetActive(true);

        //Change left panel buttons
        _txtConstellationLocator.GetComponent<Text>().color = _offGrey;
        _imgConstellationLocator.GetComponent<Image>().sprite = _greyArrow;

        _txtSettings.GetComponent<Text>().color = _offGrey;
        _imgSettings.GetComponent<Image>().sprite = _greyArrow;

        _txtControls.GetComponent<Text>().color = _offGrey;
        _imgControls.GetComponent<Image>().sprite = _greyArrow;

        _txtQuit.GetComponent<Text>().color = _offYellow;
        _imgQuit.GetComponent<Image>().sprite = _yellowLogout;
    }

    //Quit Functions
    public void QuitExperience() {
        Application.Quit();
    }
}
