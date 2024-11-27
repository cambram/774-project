using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkbenchManager : MonoBehaviour
{
    /*public GameObject _aquarius;
    public GameObject _aries;
    public GameObject _cancer;
    public GameObject _capricorn;
    public GameObject _centaurus;
    public GameObject _cygnus;
    public GameObject _gemini;
    public GameObject _hydra;
    public GameObject _leo;
    public GameObject _leoMinor;
    public GameObject _libra;
    public GameObject _lynx;
    public GameObject _monceros;
    public GameObject _orion;
    public GameObject _pisces;
    public GameObject _sagittarius;
    public GameObject _scorpius;
    public GameObject _southernCrux;
    public GameObject _taurus;
    public GameObject _ursaMajor;
    public GameObject _virgo;*/

    public List<GameObject> constellationPages; // Assign constellation GameObjects in the inspector
    private int _currentPageIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < constellationPages.Count; i++) {
            if(i == 0) {
                constellationPages[i].SetActive(true);
            } else {
                constellationPages[i].SetActive(false);
            }
        }
    }

    public void NextButton() {
        // Deactivate the current page
        constellationPages[_currentPageIndex].SetActive(false);

        // Update the index for the next page, looping back to the start if necessary
        _currentPageIndex = (_currentPageIndex + 1) % constellationPages.Count;

        // Activate the new current page
        constellationPages[_currentPageIndex].SetActive(true);
    }
    public void PreviousButton() {
        // Deactivate the current page
        constellationPages[_currentPageIndex].SetActive(false);

        // Update the index for the previous page, wrapping around if necessary
        _currentPageIndex = (_currentPageIndex - 1 + constellationPages.Count) % constellationPages.Count;

        // Activate the new current page
        constellationPages[_currentPageIndex].SetActive(true);
    }
}
