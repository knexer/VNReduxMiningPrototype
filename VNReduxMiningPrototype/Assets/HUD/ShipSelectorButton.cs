using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof(Button))]
public class ShipSelectorButton : MonoBehaviour {

    private Ship ship;

	void Start () {
        GetComponent<Button>().onClick.AddListener(() => { switchToShip(); });
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setShip(Ship ship) {
        this.ship = ship;
        GetComponentInChildren<Text>().text = ship.shipName;
    }

    private void switchToShip() {
        ship.select();
    }
}
