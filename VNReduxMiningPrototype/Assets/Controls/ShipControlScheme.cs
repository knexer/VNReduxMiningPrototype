using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class ShipControlScheme : ControlScheme {

    public float TorquePower;
    public float AngularSpeedFloor;

    private Dictionary<KeyCode, Command> keyDownHandlers;
    private Dictionary<KeyCode, Command> keyUpHandlers;
    private Dictionary<KeyCode, Command> keyHandlers;

    void Start() {
        keyDownHandlers = new Dictionary<KeyCode, Command>();
        keyUpHandlers = new Dictionary<KeyCode, Command>();
        keyHandlers = new Dictionary<KeyCode, Command>();

        Rigidbody _target = GetComponent<Rigidbody>();

        // Linear controls
        keyHandlers.Add(KeyCode.I, () => { _target.AddRelativeForce(Vector3.forward); });
        keyHandlers.Add(KeyCode.K, () => { _target.AddRelativeForce(Vector3.back); });
        keyHandlers.Add(KeyCode.J, () => { _target.AddRelativeForce(Vector3.left); });
        keyHandlers.Add(KeyCode.L, () => { _target.AddRelativeForce(Vector3.right); });
        keyHandlers.Add(KeyCode.U, () => { _target.AddRelativeForce(Vector3.up); });
        keyHandlers.Add(KeyCode.O, () => { _target.AddRelativeForce(Vector3.down); });

        // Torque controls
        keyHandlers.Add(KeyCode.W, () => { _target.AddRelativeTorque(Vector3.right * TorquePower); });
        keyHandlers.Add(KeyCode.S, () => { _target.AddRelativeTorque(Vector3.left * TorquePower); });
        keyHandlers.Add(KeyCode.A, () => { _target.AddRelativeTorque(Vector3.down * TorquePower); });
        keyHandlers.Add(KeyCode.D, () => { _target.AddRelativeTorque(Vector3.up * TorquePower); });
        keyHandlers.Add(KeyCode.Q, () => { _target.AddRelativeTorque(Vector3.forward * TorquePower); });
        keyHandlers.Add(KeyCode.E, () => { _target.AddRelativeTorque(Vector3.back * TorquePower); });

        // Angular dampening
        keyHandlers.Add(KeyCode.F, () => {
            if (_target.angularVelocity.magnitude < AngularSpeedFloor) {
                _target.angularVelocity = Vector3.zero;
            } else {
                _target.AddTorque(-1 * TorquePower * _target.angularVelocity.normalized);
            }
        });

    }

    public override Dictionary<KeyCode, Command> getKeyDownHandlers() {
        return keyDownHandlers;
    }

    public override Dictionary<KeyCode, Command> getKeyUpHandlers() {
        return keyUpHandlers;
    }

    public override Dictionary<KeyCode, Command> getKeyHandlers() {
        return keyHandlers;
    }
}
