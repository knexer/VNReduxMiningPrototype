using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour {

    public float TorquePower;
    public float AngularSpeedFloor;

    private Rigidbody _target;

	// Use this for initialization
	void Start () {
        _target = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
    void Update()
    {
        handleLinearControls();
        handleTorqueControls();
        handleAngularDampening();
	}

    private void handleLinearControls()
    {
        if (Input.GetKey(KeyCode.I))
        {
            _target.AddRelativeForce(Vector3.forward);
        }
    }

    private void handleTorqueControls()
    {
        Vector3 torqueVector = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            torqueVector += Vector3.right;
        }
        if (Input.GetKey(KeyCode.S))
        {
            torqueVector += Vector3.left;
        }
        if (Input.GetKey(KeyCode.A))
        {
            torqueVector += Vector3.down;
        }
        if (Input.GetKey(KeyCode.D))
        {
            torqueVector += Vector3.up;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            torqueVector += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.E))
        {
            torqueVector += Vector3.back;
        }

        _target.AddRelativeTorque(torqueVector * TorquePower);
    }

    private void handleAngularDampening()
    {
        if (Input.GetKey(KeyCode.F))
        {
            if (_target.angularVelocity.magnitude < AngularSpeedFloor)
            {
                _target.angularVelocity = Vector3.zero;
            }
            else
            {
                _target.AddTorque(-1 * TorquePower * _target.angularVelocity.normalized);
            }
        }
    }
}
