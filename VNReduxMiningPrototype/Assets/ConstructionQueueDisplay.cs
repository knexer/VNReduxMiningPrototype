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
    private Ship _selectedShip
    {
        get
        {
            return __selectedShip;
        }
        set
        {
            // deregister from old selection's events
            if (__selectedShip != null)
            {
                Shipyard oldConstructionManager = __selectedShip.GetComponent<Shipyard>();
                if (oldConstructionManager != null)
                {
                    oldConstructionManager.orderEnqueued -= enqueueOrder;
                    oldConstructionManager.orderDequeued -= dequeueOrder;
                }
            }

            // register to new selection's events
            Shipyard constructionManager = value.GetComponent<Shipyard>();
            if (constructionManager != null)
            {
                constructionManager.orderEnqueued += enqueueOrder;
                constructionManager.orderDequeued += dequeueOrder;
            }

            __selectedShip = value;
        }
    }
    private Ship __selectedShip;

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

        // Populate UI to represent initial state of queue
        foreach(Shipyard.ConstructionOrder construction in constructionManager.ConstructionQueue) {
            enqueueOrder(construction);
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

    private void dequeueOrder(Shipyard.ConstructionOrder construction) {
        // TODO stop assuming the first item in the queue was finished
        Destroy(_queuedItems.Dequeue());
        if(_queuedItems.Count == 0) {
            hide();
        }
    }

    private void enqueueOrder(Shipyard.ConstructionOrder construction)
    {
        GameObject queueEntry = Instantiate<GameObject>(QueueEntryPrefab);
        queueEntry.transform.SetParent(gameObject.transform, false);
        queueEntry.GetComponent<Text>().text = construction.Ship.Name;

        _queuedItems.Enqueue(queueEntry);

        if (_queuedItems.Count == 1)
        {
            show();
        }
    }
}
