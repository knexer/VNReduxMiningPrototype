using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ResourceTank : MonoBehaviour {

    //Editor-configurables
    public Resource Type;
    public float Capacity;
    public Text Display;

    public float Stored { get; private set; }
    public float RemainingCapacity { get { return Capacity - Stored; } }

    /// <summary>
    /// Attempt to store resources in the tank - as much as possible, up to amount.
    /// </summary>
    /// <param name="amount">The maximum quantity to store.</param>
    /// <returns>The quantity that was successfully stored.</returns>
    public float tryStore(float amount)
    {
        if (amount < 0) throw new ArgumentException("Cannot store a negative quantity of resources: " + amount);
        float remainingCapacity = Capacity - Stored;
        float taken = Mathf.Min(amount, remainingCapacity);
        Stored += taken;
        return taken;
    }

    /// <summary>
    /// Attempt to extract resources from the tank - as much as possible up to amount.
    /// </summary>
    /// <param name="amount">The maximum quantity to extract.</param>
    /// <returns>The quantity that was successfully extracted.</returns>
    public float tryExtract(float amount)
    {
        if (amount < 0) throw new ArgumentException("Cannot extract a negative quantity of resources: " + amount);
        float taken = Mathf.Min(amount, Stored);
        Stored -= taken;
        return taken;
    }

	// Use this for initialization
	void Start () {
        Stored = 0;
	}
	
	// Update is called once per frame
	void Update () {
        // TODO update this only when stored changes?
        if (Display != null)
        {
            Display.text = Type.ToString() + ": " + Stored;
        }
	}
}
