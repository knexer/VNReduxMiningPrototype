using System;
using System.Collections.Generic;
using UnityEngine;

class CameraController : MonoBehaviour {

    public Vector3 followOffset;

    void Start() {
        Ship.ShipSelected += new Ship.ShipSelectionEventHandler((ship) => {
            follow(ship.gameObject);
        });
    }

    public void follow(GameObject obj) {
        transform.parent = obj.transform;
        transform.localPosition = followOffset;
        transform.localRotation = Quaternion.identity;
    }

    public void unfollow() {
        transform.parent = null;
    }

}
