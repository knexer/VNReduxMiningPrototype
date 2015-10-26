using System;
using System.Collections.Generic;
using UnityEngine;

class CameraController : MonoBehaviour {

    public void follow(GameObject obj, Vector3 offset) {
        transform.rotation = obj.transform.rotation;
        transform.position = obj.transform.position + offset;
        transform.parent = obj.transform;
    }

    public void unfollow() {
        transform.parent = null;
    }

}
