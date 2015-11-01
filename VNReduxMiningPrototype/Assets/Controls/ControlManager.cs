using UnityEngine;
using System.Collections.Generic;

public class ControlManager : MonoBehaviour {

    private HashSet<ControlScheme> enabledControls;
    
	void Start () {
        enabledControls = new HashSet<ControlScheme>();
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

    public void enableControlScheme(ControlScheme controlScheme) {
        enabledControls.Add(controlScheme);
    }

    public void disableControlScheme(ControlScheme controlScheme) {
        enabledControls.Remove(controlScheme);
    }

    public void clearControlSchemes() {
        enabledControls.Clear();
    }
}
