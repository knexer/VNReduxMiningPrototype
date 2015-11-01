using System;
using System.Collections.Generic;
using UnityEngine;

class CameraController : MonoBehaviour {

    public Vector3 followOffset;

    public void follow(GameObject obj) {
        transform.rotation = obj.transform.rotation;
        transform.position = obj.transform.position + followOffset;
        transform.parent = obj.transform;
    }

    public void unfollow() {
        transform.parent = null;
    }

}
