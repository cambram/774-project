using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundingBox : MonoBehaviour
{
    public Camera _head;

    private void OnTriggerStay(Collider collision) {
        if (collision.CompareTag("boundingBox")) {
            this.transform.position = new Vector3(2.07f, _head.transform.position.y, 3.617f);
        }
    }
}
