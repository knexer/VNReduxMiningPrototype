using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

// Constructs ships
public class Shipyard : MonoBehaviour {

    public ShipCost[] BuildableShips;
    public Transform SpawnLocation;

    private Queue<ConstructionOrder> _constructionQueue;
    private float _queueCompletionTime;

    public Shipyard()
    {
        _constructionQueue = new Queue<ConstructionOrder>();
    }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (_constructionQueue.Count > 0)
        {
            if (Time.time >= _constructionQueue.Peek().EndTime)
            {
                ShipCost completed = _constructionQueue.Dequeue().Ship;
                Instantiate(completed.Prefab, SpawnLocation.position, SpawnLocation.rotation);
                Debug.Log("Construction finished on " + completed.Name);
            }
        }
	}

    public void tryEnqueueShip(ShipCost ship)
    {
        Debug.Log("Attempting to enqueue a " + ship.Name);
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
        int remainingCost = ship.Cost;
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
        public string Name;

        public int Cost;
        public Resource Type;
        public float Time;

        public string Message
        {
            get
            {
                return "Construct a " + Name + ": " + Cost + " " + Type + ", " + Time + " seconds.";
            }
        }
    }
}
