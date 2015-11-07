using UnityEngine;
using System.Collections;

/// <summary>
/// Displays the ships currently under construction and their progress / ETA.
/// </summary>
public class ConstructionQueueDisplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Ship.ShipSelected += generateDisplay;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void generateDisplay(Ship ship)
    {
        Shipyard constructionManager = ship.GetComponent<Shipyard>();
        if (null == constructionManager)
        {
            hide();
        }
    }

    private void hide()
    {
    }

    private void show()
    {
    }
}
