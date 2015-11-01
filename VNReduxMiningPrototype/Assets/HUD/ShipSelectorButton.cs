using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof(Button))]
public class ShipSelectorButton : MonoBehaviour {

    private Ship ship;

    private CameraController cameraController;
    private ControlManager controlManager;

	void Start () {
        GetComponent<Button>().onClick.AddListener(() => { switchToShip(); });
        controlManager = GameObject.FindObjectOfType<ControlManager>();
        cameraController = GameObject.FindObjectOfType<CameraController>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setShip(Ship ship) {
        this.ship = ship;
        GetComponentInChildren<Text>().text = ship.shipName;
    }

    private void switchToShip() {
        GameObject.FindObjectOfType<CameraController>().follow(ship.gameObject);
        controlManager.clearControlSchemes();
        controlManager.enableControlScheme(ship.GetComponent<ShipControlScheme>());
        cameraController.follow(ship.gameObject);
    }
}
