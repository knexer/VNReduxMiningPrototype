using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceProcessor : MonoBehaviour {
    public Recipe[] Recipes;
    public float ProductionSpeed;

    private Dictionary<Recipe, float> _progress;
    private ResourceTankAggregator _tanks;

	// Use this for initialization
	void Start () {
        foreach (Recipe recipe in Recipes)
        {
            _progress[recipe] = 0.0f;
        }
        _tanks = GetComponent<ResourceTankAggregator>();
	}
	
	// Update is called once per frame
	void Update () {
        foreach(Recipe recipe in Recipes) {
            _progress[recipe] += ProductionSpeed * Time.deltaTime;
            int numCompleteCycles = (int)_progress[recipe];
            _progress[recipe] -= numCompleteCycles;
            attemptProduction(recipe, numCompleteCycles);
        }
	}

    private void attemptProduction(Recipe recipe, int requestedNumCycles)
    {
        int availableInput = _tanks.StoredOf(recipe.InputType);
        int inputConstraint = availableInput / recipe.ConsumedAmount;

        int outputCapacity = _tanks.RemainingCapacityFor(recipe.OutputType);
        int outputConstraint = outputCapacity / recipe.ProducedAmount;

        int actualNumCycles = Mathf.Min(requestedNumCycles, inputConstraint, outputConstraint);

        if (actualNumCycles > 0)
        {
            // No need to verify that resources were successfully updated
            _tanks.TryExtract(recipe.InputType, actualNumCycles * recipe.ConsumedAmount);
            _tanks.TryStore(recipe.OutputType, actualNumCycles * recipe.ProducedAmount);
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
