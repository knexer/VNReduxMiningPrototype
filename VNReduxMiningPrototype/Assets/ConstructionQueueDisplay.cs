using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// Displays the ships currently under construction and their progress / ETA.
/// </summary>
public class ConstructionQueueDisplay : MonoBehaviour {

    public GameObject QueueEntryPrefab;

    private Queue<GameObject> _queuedItems;
    private Ship _selectedShip;

    public ConstructionQueueDisplay() {
        _queuedItems = new Queue<GameObject>();
    }

	// Use this for initialization
	void Start () {
        Ship.ShipSelected += generateDisplay;
        hide();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void generateDisplay(Ship ship)
    {
        _selectedShip = ship;
        foreach (GameObject queuedItem in _queuedItems) {
            Destroy(queuedItem);
        }
        _queuedItems.Clear();

        Shipyard constructionManager = ship.GetComponent<Shipyard>();
        if (null == constructionManager)
        {
            hide();
            return;
        }

        show();
        foreach(Shipyard.ConstructionOrder construction in constructionManager.ConstructionQueue) {
            GameObject queueEntry = Instantiate<GameObject>(QueueEntryPrefab);
            queueEntry.transform.SetParent(gameObject.transform, false);
            queueEntry.GetComponent<Text>().text = construction.Ship.Name;

            _queuedItems.Enqueue(queueEntry);
            // TODO handle ships added to queue for currently selected ship
            // TODO handle this in the shipyard
            construction.OnConstructionCompleted += () => CompleteConstructionFor(ship);
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

    private void CompleteConstructionFor(Ship ship) {
        if(ship == _selectedShip) {
            Destroy(_queuedItems.Dequeue());
            if(_queuedItems.Count == 0) {
                hide();
            }
        }
    }
}
