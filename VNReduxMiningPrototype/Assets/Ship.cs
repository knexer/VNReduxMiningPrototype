using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {

    public string shipName;

    private ControlScheme controlScheme;

	// Use this for initialization
	void Start () {
        controlScheme = gameObject.AddComponent<ShipControlScheme>(); // TODO: allow editing control scheme on ship
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public ControlScheme getControlScheme() {
        return controlScheme;
    }
}
