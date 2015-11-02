using UnityEngine;
using System.Collections;

public class AsteroidMiner : MonoBehaviour {

    public float MiningDistance;
    public float MiningRate;

    private ResourceTankAggregator _tanks;
    private Mineable _target;
    private float _sqMiningDistance;
    private float _progress = 0;

	// Use this for initialization
	void Start () {
        _tanks = GetComponent<ResourceTankAggregator>();
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
            if (_tanks != null && _tanks.RemainingCapacityFor(Resource.ORE) > 0)
            {
                _progress += MiningRate * Time.deltaTime;
                int storeableAmount = (int)_progress;
                if (storeableAmount > 0)
                {
                    _progress -= storeableAmount;
                    _tanks.TryStore(Resource.ORE, storeableAmount);
                }
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
