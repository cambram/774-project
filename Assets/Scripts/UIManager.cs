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

    //Navigation Functions
    public void OpenConstellationLocator() {
        _constellationLocatorUI.SetActive(true);
        _settingsUI.SetActive(false);
        _controlsUI.SetActive(false);
        _quitUI.SetActive(false);

        //Change left panel buttons
        _txtConstellationLocator.GetComponent<Text>().color = _offYellow;
        _imgConstellationLocator.GetComponent<Image>().sprite = _yellowArrow;

        _txtSettings.GetComponent<Text>().color = _offGrey;
        _imgSettings.GetComponent<Image>().sprite = _greyArrow;

        _txtControls.GetComponent<Text>().color = _offGrey;
        _imgControls.GetComponent<Image>().sprite = _greyArrow;

        _txtQuit.GetComponent<Text>().color = _offGrey;
        _imgQuit.GetComponent<Image>().sprite = _greyArrow;
    }

    public void OpenSettings() {
        _constellationLocatorUI.SetActive(false);
        _settingsUI.SetActive(true);
        _controlsUI.SetActive(false);
        _quitUI.SetActive(false);

        //Change left panel buttons
        _txtConstellationLocator.GetComponent<Text>().color = _offGrey;
        _imgConstellationLocator.GetComponent<Image>().sprite = _greyArrow;

        _txtSettings.GetComponent<Text>().color = _offYellow;
        _imgSettings.GetComponent<Image>().sprite = _yellowArrow;

        _txtControls.GetComponent<Text>().color = _offGrey;
        _imgControls.GetComponent<Image>().sprite = _greyArrow;

        _txtQuit.GetComponent<Text>().color = _offGrey;
        _imgQuit.GetComponent<Image>().sprite = _greyArrow;
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
        _imgQuit.GetComponent<Image>().sprite = _greyArrow;
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
        _imgQuit.GetComponent<Image>().sprite = _yellowArrow;
    }
}
