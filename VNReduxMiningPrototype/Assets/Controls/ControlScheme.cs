using UnityEngine;
using System.Collections.Generic;

public abstract class ControlScheme : MonoBehaviour {

    public delegate void Command();
    
    public abstract Dictionary<KeyCode, Command> getKeyDownHandlers();
    public abstract Dictionary<KeyCode, Command> getKeyUpHandlers();
    public abstract Dictionary<KeyCode, Command> getKeyHandlers();

    public HashSet<KeyCode> getKeysHandled() {
        HashSet<KeyCode> keyCodes = new HashSet<KeyCode>();
        keyCodes.UnionWith(getKeyDownHandlers().Keys);
        keyCodes.UnionWith(getKeyUpHandlers().Keys);
        keyCodes.UnionWith(getKeyHandlers().Keys);
        return keyCodes;
    }
}
