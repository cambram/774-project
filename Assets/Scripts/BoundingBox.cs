using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundingBox : MonoBehaviour
{
    public Camera _head;

    private void OnTriggerStay(Collider collision) {
        if (collision.CompareTag("boundingBox")) {
            this.transform.position = new Vector3(2.185f, _head.transform.position.y, 3.646f);
        }
    }
}
