using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ResourceDisplay : MonoBehaviour {
    public GameObject TextPrefab;

    private Dictionary<Resource, Text> _resourceDisplays;
    private Ship _selectedShip;

	// Use this for initialization
	void Start () {
        Ship.ShipSelected += generateDisplay;
        hide();
	}
	
	// Update is called once per frame
	void Update () {
        updateText();
	}

    private void generateDisplay(Ship ship)
    {
        _selectedShip = ship;
        ResourceTankAggregator tanks = ship.GetComponent<ResourceTankAggregator>();
        if (null == tanks)
        {
            hide();
            return;
        }

        // clear the display
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            Destroy(gameObject.transform.GetChild(i).gameObject);
        }

        show();

        _resourceDisplays = new Dictionary<Resource, Text>();
        foreach (Resource resource in tanks.StoreableResources())
        {
            GameObject textObject = Instantiate(TextPrefab);
            textObject.transform.SetParent(gameObject.transform, false);
            _resourceDisplays[resource] = textObject.GetComponent<Text>();
        }

        updateText();
    }

    private void updateText()
    {
        ResourceTankAggregator tanks = _selectedShip.GetComponent<ResourceTankAggregator>();
        foreach (KeyValuePair<Resource, Text> display in _resourceDisplays)
        {
            Resource resource = display.Key;
            display.Value.text = resource.ToString() + ": " + tanks.StoredOf(resource) + " / " + tanks.CapacityFor(resource);
        }
    }

    private void hide()
    {
        gameObject.SetActive(false);
    }

    private void show()
    {
        gameObject.SetActive(true);
    }
}
