using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class ShipSelectorMenu : MonoBehaviour {

    public ShipSelectorButton buttonPrefab;

    private GameObject content;

    void Start() {
        content = transform.Find("Viewport/Content").gameObject;
        hide();
    }

    void Update() {
        if (Input.GetKeyUp(KeyCode.Escape)) {
            hide();
        }
    }

    public void show() {
        gameObject.SetActive(true);
        findShips();
    }

    public void hide() {
        gameObject.SetActive(false);
    }

    private void findShips() {
        while (content.transform.childCount > 0) {
            Destroy(content.transform.GetChild(0));
        }

        Ship[] ships = FindObjectsOfType<Ship>();
        foreach (Ship ship in ships) {
            ShipSelectorButton button = Instantiate(buttonPrefab);
            button.setShip(ship);
            button.transform.SetParent(content.transform);
            button.GetComponent<Button>().onClick.AddListener(() => { hide(); });
        }
    }
}
