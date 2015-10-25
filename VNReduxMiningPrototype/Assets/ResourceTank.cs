using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResourceTank : MonoBehaviour {

    //Editor-configurables
    public Resource Type;
    public float Capacity;
    public Text Display;

    public float Stored { get; private set; }

    public float BestEffortStore(float amount)
    {
        amount = Mathf.Max(amount, 0.0f);
        float remainingCapacity = Capacity - Stored;
        float taken = Mathf.Min(amount, remainingCapacity);
        Stored += taken;
        return amount - taken;
    }

    public float BestEffortExtract(float amount)
    {
        amount = Mathf.Max(amount, 0.0f);
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
        if (Display != null)
        {
            Display.text = Type.ToString() + ": " + Stored;
        }
	}
}
