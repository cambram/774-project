using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject _XROrigin;
    [SerializeField]
    private GameObject _mainCamera;
    [SerializeField]
    private Transform _cameraOffset;
    private Vector3 _origin = new Vector3(2.326f, 0.4f, 4.395f);

    // Start is called before the first frame update
    void Start() {
        
    }

    public void ResetView() {
        _XROrigin.transform.position = _origin;
        _XROrigin.transform.eulerAngles = new Vector3(0, 200, 0);
    }
}
