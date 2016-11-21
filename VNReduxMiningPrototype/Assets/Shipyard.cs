using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

// Constructs ships
public class Shipyard : MonoBehaviour {

    public ShipCost[] BuildableShips;
    public Transform SpawnLocation;

    public IEnumerable ConstructionQueue {
        get { return _constructionQueue; }
    }

    public event Action<ConstructionOrder> orderEnqueued;
    public event Action<ConstructionOrder> orderDequeued;

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
                _constructionQueue.Dequeue().CompleteConstruction();
            }
        }
	}

    void CompleteShip(ShipCost completed) {
        Instantiate(completed.Prefab, SpawnLocation.position, SpawnLocation.rotation);
    }

    public void tryEnqueueShip(ShipCost ship)
    {
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
        toEnqueue.OnConstructionCompleted += () => CompleteShip(ship);
        toEnqueue.OnConstructionCompleted += () => orderDequeued(toEnqueue);

        // notify views
        if (orderEnqueued != null)
        {
            orderEnqueued(toEnqueue);
        }
    }

    public class ConstructionOrder
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

        public event Action OnConstructionCompleted;

        public void CompleteConstruction() {
            OnConstructionCompleted();
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
                return Name + ": " + Cost + " " + Type + ", " + Time + " seconds.";
            }
        }
    }
}
