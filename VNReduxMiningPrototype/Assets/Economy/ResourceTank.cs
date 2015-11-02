using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ResourceTank : MonoBehaviour {

    //Editor-configurables
    public Resource Type;
    public int Capacity;
    public Text Display;

    public int Stored { get; private set; }
    public int RemainingCapacity { get { return Capacity - Stored; } }

    public ResourceTank()
    {
        Stored = 0;
    }

    void Awake()
    {
        ResourceTankAggregator aggregator = GetComponent<ResourceTankAggregator>();
        if (null == aggregator) aggregator = gameObject.AddComponent<ResourceTankAggregator>();
        aggregator.RegisterTank(this);
    }

    void OnDestroy()
    {
        ResourceTankAggregator aggregator = GetComponent<ResourceTankAggregator>();
        if (null != aggregator)
        {
            aggregator.DeregisterTank(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // TODO update this only when stored changes?
        if (Display != null)
        {
            Display.text = Type.ToString() + ": " + Stored;
        }
    }

    /// <summary>
    /// Attempt to store resources in the tank - as much as possible, up to amount.
    /// </summary>
    /// <param name="amount">The maximum quantity to store.</param>
    /// <returns>The quantity that was successfully stored.</returns>
    public int tryStore(int amount)
    {
        if (amount < 0) throw new ArgumentException("Cannot store a negative quantity of resources: " + amount);
        int remainingCapacity = Capacity - Stored;
        int taken = Mathf.Min(amount, remainingCapacity);
        Stored += taken;
        return taken;
    }

    /// <summary>
    /// Attempt to extract resources from the tank - as much as possible up to amount.
    /// </summary>
    /// <param name="amount">The maximum quantity to extract.</param>
    /// <returns>The quantity that was successfully extracted.</returns>
    public int tryExtract(int amount)
    {
        if (amount < 0) throw new ArgumentException("Cannot extract a negative quantity of resources: " + amount);
        int taken = Mathf.Min(amount, Stored);
        Stored -= taken;
        return taken;
    }
}
