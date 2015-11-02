using UnityEngine;
using System.Collections;

public class AsteroidMiner : MonoBehaviour {

    public float MiningDistance;
    public float MiningRate;

    private ResourceTank _tank;
    private Mineable _target;
    private float _sqMiningDistance;

	// Use this for initialization
	void Start () {
        ResourceTank[] tanks = GetComponents<ResourceTank>();
        foreach(ResourceTank tank in tanks)
        {
            if (tank.Type == Resource.ORE) _tank = tank;
        }
        _sqMiningDistance = MiningDistance * MiningDistance;
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            tryAcquireTarget();
        }

        checkRange();

        if(_target != null)
        {
            if (_tank != null && _tank.Stored < _tank.Capacity)
            {
                _tank.tryStore((int)(MiningRate * Time.deltaTime));
            }
        }
    }

    private void tryAcquireTarget()
    {
        // find closest asteroid
        Mineable[] asteroids = FindObjectsOfType<Mineable>();
        float minDistance = float.MaxValue;
        _target = null;
        foreach (Mineable target in asteroids)
        {
            float distance = (target.transform.position - transform.position).sqrMagnitude;
            if (distance < minDistance)
            {
                _target = target;
                minDistance = distance;
            }
        }

        // if it's too far away, don't start mining
        if (minDistance > _sqMiningDistance) _target = null;
    }

    private void checkRange()
    {
        if (_target == null) return;
        float sqDistance = (_target.transform.position - transform.position).sqrMagnitude;
        if (sqDistance > _sqMiningDistance)
        {
            tryAcquireTarget();
        }
    }
}
