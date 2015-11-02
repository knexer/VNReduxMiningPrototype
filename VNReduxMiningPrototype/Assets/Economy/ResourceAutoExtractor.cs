using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class ResourceAutoExtractor : MonoBehaviour {
    public Resource Type;
    public float TransferRange;
    public float TransferRate;

    private ResourceTankAggregator _tanks;
    private float _progress = 0;

	// Use this for initialization
	void Start () {
        _tanks = GetComponent<ResourceTankAggregator>();
	}
	
	// Update is called once per frame
	void Update () {
        IList<ResourceTankAggregator> allTanks = new List<ResourceTankAggregator>(FindObjectsOfType<ResourceTankAggregator>());

        float minDistance = float.MaxValue;
        ResourceTankAggregator closest = null;
        foreach (
            ResourceTankAggregator tanks in allTanks
            .Where<ResourceTankAggregator>((tanks) => tanks.StoreableResources().Contains(Type))
            .Where<ResourceTankAggregator>((tanks) => tanks.transform.root != transform.root)
            .Where<ResourceTankAggregator>((tanks) => tanks.StoredOf(Type) > 0)
            )
        {
            float sqDistance = (tanks.transform.position - transform.position).sqrMagnitude;
            if (sqDistance < TransferRange * TransferRange && sqDistance < minDistance)
            {
                minDistance = sqDistance;
                closest = tanks;
            }
        }

        if (closest != null)
        {
            _progress += TransferRate * Time.deltaTime;
            if (_progress >= 1)
            {
                int amountToTransfer = (int)_progress;
                _progress -= amountToTransfer;
                amountToTransfer = Math.Min(
                     amountToTransfer,
                     Math.Min(closest.StoredOf(Type), _tanks.RemainingCapacityFor(Type)));
                closest.TryExtract(Type, amountToTransfer);
                _tanks.TryStore(Type, amountToTransfer);
            }
        }
	}
}
