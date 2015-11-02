using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {

    public delegate void ShipSelectionEventHandler(Ship selectedShip);
    public static event ShipSelectionEventHandler ShipSelected;

    public string shipName;

    public ControlScheme ControlScheme { get; private set; }

	// Use this for initialization
	void Start () {
        ControlScheme = gameObject.GetComponent<ShipControlScheme>();
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        if (null != rigidbody) rigidbody.sleepThreshold = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void select() {

        ShipSelected(this);
    }
}
