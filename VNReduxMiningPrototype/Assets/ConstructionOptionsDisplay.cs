using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// Display the set of construction options available on the currently selected ship and afford enqueueing them.
/// </summary>
public class ConstructionOptionsDisplay : MonoBehaviour
{
    public GameObject ButtonPrefab;
    
    private Dictionary<Shipyard.ShipCost, GameObject> _buttons;

    // Use this for initialization
    void Start()
    {
        Ship.ShipSelected += generateDisplay;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void generateDisplay(Ship ship)
    {
        Debug.Log("Generating construction options display for " + ship.shipName);
        Shipyard constructionManager = ship.GetComponent<Shipyard>();
        if (null == constructionManager)
        {
            hide();
            return;
        }
        Debug.Log("Found " + constructionManager.BuildableShips.Length + " options.");

        // clear the display
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            Destroy(gameObject.transform.GetChild(i).gameObject);
        }

        show();

        _buttons = new Dictionary<Shipyard.ShipCost, GameObject>();
        foreach (Shipyard.ShipCost option in constructionManager.BuildableShips)
        {
            GameObject button = Instantiate<GameObject>(ButtonPrefab);
            button.GetComponentInChildren<Text>().text = option.Message;
            button.transform.SetParent(gameObject.transform, false);

            Debug.Log("Creating button with message: " + option.Message);

            // gotta declare a new ShipCost inside each loop iteration since C# closures are lexical
            // fuckin gets me every time
            Shipyard.ShipCost lexicallyEnclosedShip = option;
            button.GetComponent<Button>().onClick.AddListener(() => { constructionManager.tryEnqueueShip(lexicallyEnclosedShip); });
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
