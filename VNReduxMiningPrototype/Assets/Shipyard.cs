using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

// Constructs ships
public class Shipyard : MonoBehaviour {

    public ShipCost[] BuildableShips;
    public GameObject ButtonPrefab;
    public GameObject ButtonParent;

    private Queue<ConstructionOrder> _constructionQueue;
    private float _queueCompletionTime;

    public Shipyard()
    {
        _constructionQueue = new Queue<ConstructionOrder>();
    }

	// Use this for initialization
	void Start () {
        foreach (ShipCost ship in BuildableShips)
        {
            GameObject button = Instantiate<GameObject>(ButtonPrefab);
            button.transform.parent = ButtonParent.transform;
            button.GetComponentInChildren<Text>().text = ship.message;

            // gotta declare a new ShipCost inside each loop iteration since C# closures are lexical
            // fuckin gets me every time
            ShipCost lexicallyEnclosedShip = ship;
            button.GetComponent<Button>().onClick.AddListener(() => { tryEnqueueShip(lexicallyEnclosedShip); });
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (_constructionQueue.Count > 0)
        {
            if (Time.time >= _constructionQueue.Peek().EndTime)
            {
                // TODO finish construction
                Debug.Log("Construction finished on " + _constructionQueue.Peek().Ship.name);
                _constructionQueue.Dequeue();
            }
        }
	}

    void tryEnqueueShip(ShipCost ship)
    {
        Debug.Log("Attempting to enqueue a " + ship.name);
        // check resource costs
        ResourceTank[] tanks = GetComponents<ResourceTank>();
        float availableResource = 0;
        foreach (ResourceTank tank in tanks)
        {
            if (tank.Type == ship.Type)
            {
                availableResource += tank.Stored;
            }
        }

        Debug.Log("Needed " + ship.Cost + " " + ship.Type + " and found " + availableResource);
        if (availableResource < ship.Cost)
        {
            return;
        }

        // consume resources
        float remainingCost = ship.Cost;
        foreach (ResourceTank tank in tanks)
        {
            if (tank.Type == ship.Type)
            {
                remainingCost -= tank.tryExtract(remainingCost);
            }
        }

        // set up start time based on what's in queue
        float startTime = Time.time;
        if (_constructionQueue.Count > 0)
        {
            startTime = _queueCompletionTime;
        }

        // actually enqueue
        ConstructionOrder toEnqueue = new ConstructionOrder() { Ship = ship, StartTime = startTime };
        _constructionQueue.Enqueue(toEnqueue);
        _queueCompletionTime = toEnqueue.EndTime;
    }

    private class ConstructionOrder
    {
        public ShipCost Ship;
        public float StartTime;
        public float EndTime
        {
            get
            {
                return StartTime + Ship.Time;
            }
        }
    }

    [System.Serializable]
    public class ShipCost
    {
        public GameObject Prefab;
        public string name;

        public float Cost;
        public Resource Type;
        public float Time;

        public string message
        {
            get
            {
                return "Construct a " + name + ": " + Cost + " " + Type + ", " + Time + " seconds.";
            }
        }
    }
}
