using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ResourceAutoExtractor : MonoBehaviour {
    public Resource Type;
    public float TransferRange;
    public float TransferRate;

    private ResourceTank _tank;

	// Use this for initialization
	void Start () {
        ResourceTank[] tanks = GetComponents<ResourceTank>();
        foreach (ResourceTank tank in tanks)
        {
            if (tank.Type == Type)
            {
                _tank = tank;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        IList<ResourceTank> allTanks = new List<ResourceTank>(FindObjectsOfType<ResourceTank>());

        float minDistance = float.MaxValue;
        ResourceTank closest = null;
        foreach (ResourceTank tank in allTanks
            .Where<ResourceTank>((tank) => tank.Type == Type)
            .Where<ResourceTank>((tank) => tank.transform.root != transform.root)
            .Where<ResourceTank>((tank) => tank.Stored > 0))
        {
            float sqDistance = (tank.transform.position - transform.position).sqrMagnitude;
            if (sqDistance < TransferRange * TransferRange && sqDistance < minDistance)
            {
                minDistance = sqDistance;
                closest = tank;
            }
        }

        if (closest != null)
        {
            float amountToTransfer = Mathf.Min(TransferRate * Time.deltaTime, closest.Stored, _tank.RemainingCapacity);
            closest.tryExtract(amountToTransfer);
            _tank.tryStore(amountToTransfer);
        }
	}
}
