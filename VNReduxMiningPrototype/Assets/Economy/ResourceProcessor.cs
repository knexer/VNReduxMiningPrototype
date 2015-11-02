using UnityEngine;
using System.Collections;

public class ResourceProcessor : MonoBehaviour {
    public Recipe[] Recipes;
    public float ProductionSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        foreach(Recipe recipe in Recipes) {

        }
	}

    [System.Serializable]
    public class Recipe {
        public Resource InputType;
        public Resource OutputType;
        public int ConsumedAmount;
        public int ProducedAmount;
    }
}
