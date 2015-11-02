using UnityEngine;
using System.Collections.Generic;

public class ControlManager : MonoBehaviour {

    private HashSet<ControlScheme> enabledControls;
    
	void Start () {
        enabledControls = new HashSet<ControlScheme>();

        Ship.ShipSelected += new Ship.ShipSelectionEventHandler((ship) => {
            clearControlSchemes();
            enableControlScheme(ship.ControlScheme);
        });
	}
	
	void Update () {
	    foreach (ControlScheme controlScheme in enabledControls) {
            foreach (KeyCode keyCode in controlScheme.getKeyHandlers().Keys) {
                if (Input.GetKey(keyCode)) {
                    controlScheme.getKeyHandlers()[keyCode]();
                }
            }

            foreach (KeyCode keyCode in controlScheme.getKeyDownHandlers().Keys) {
                if (Input.GetKeyDown(keyCode)) {
                    controlScheme.getKeyDownHandlers()[keyCode]();
                }
            }

            foreach (KeyCode keyCode in controlScheme.getKeyUpHandlers().Keys) {
                if (Input.GetKeyUp(keyCode)) {
                    controlScheme.getKeyUpHandlers()[keyCode]();
                }
            }
        }
	}

    private void enableControlScheme(ControlScheme controlScheme) {
        if (controlScheme != null) {
            enabledControls.Add(controlScheme);
        }
    }

    private void disableControlScheme(ControlScheme controlScheme) {
        enabledControls.Remove(controlScheme);
    }

    private void clearControlSchemes() {
        enabledControls.Clear();
    }
}
