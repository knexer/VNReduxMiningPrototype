using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceTankAggregator : MonoBehaviour {
    private IDictionary<Resource, HashSet<ResourceTank>> tanks;

    public ResourceTankAggregator() {
        tanks = new Dictionary<Resource, HashSet<ResourceTank>>();
    }

    public void RegisterTank(ResourceTank tank) {
        if (null == tank) return;
        if (!tanks.ContainsKey(tank.Type)) {
            tanks[tank.Type] = new HashSet<ResourceTank>();
        }
        tanks[tank.Type].Add(tank);
    }

    public void DeregisterTank(ResourceTank tank) {
        if (null == tank) return;
        if (!tanks.ContainsKey(tank.Type)) return;
        tanks[tank.Type].Remove(tank);
    }

    public int TryStore(Resource type, int amount) {
        int storedAmount = 0;
        if (null == tanks[type]) return storedAmount;
        foreach (ResourceTank tank in tanks[type]) {
            storedAmount += tank.tryStore(amount - storedAmount);
        }
        return storedAmount;
    }

    public int TryExtract(Resource type, int amount) {
        int takenAmount = 0;
        if (null == tanks[type]) return takenAmount;
        foreach(ResourceTank tank in tanks[type]) {
            takenAmount += tank.tryExtract(amount - takenAmount);
        }
        return takenAmount;
    }

    public int CapacityFor(Resource type) {
        int capacity = 0;
        if (null == tanks[type]) return capacity;
        foreach(ResourceTank tank in tanks[type]) {
            capacity += tank.Capacity;
        }
        return capacity;
    }

    public int StoredOf(Resource type) {
        int stored = 0;
        if (null == tanks[type]) return stored;
        foreach(ResourceTank tank in tanks[type]) {
            stored += tank.Stored;
        }
        return stored;
    }

    public int RemainingCapacityFor(Resource type) {
        int remainingCapacity = 0;
        if (null == tanks[type]) return remainingCapacity;
        foreach(ResourceTank tank in tanks[type]) {
            remainingCapacity += tank.RemainingCapacity;
        }
        return remainingCapacity;
    }

    public IEnumerable<Resource> StoreableResources()
    {
        return tanks.Keys;
    }
}
