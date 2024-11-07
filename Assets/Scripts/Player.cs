using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject _XROrigin;
    // Start is called before the first frame update
    void Start() {
        //ResetView();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetView() {
        _XROrigin.transform.position = new Vector3(2.61f, 0.4f, 4.45f);
        _XROrigin.transform.eulerAngles = new Vector3(0, 200, 0);
    }
}
