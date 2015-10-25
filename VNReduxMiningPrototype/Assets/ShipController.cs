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
        handleTimeControls();
	}

    private void handleLinearControls()
    {
        if (Input.GetKey(KeyCode.I))
        {
            _target.AddRelativeForce(Vector3.forward);
        }
        if (Input.GetKey(KeyCode.K))
        {
            _target.AddRelativeForce(Vector3.back);
        }
        if (Input.GetKey(KeyCode.J))
        {
            _target.AddRelativeForce(Vector3.left);
        }
        if (Input.GetKey(KeyCode.L))
        {
            _target.AddRelativeForce(Vector3.right);
        }
        if (Input.GetKey(KeyCode.U))
        {
            _target.AddRelativeForce(Vector3.up);
        }
        if (Input.GetKey(KeyCode.O))
        {
            _target.AddRelativeForce(Vector3.down);
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

    private void handleTimeControls()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            Time.timeScale *= 2;
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            Time.timeScale /= 2;
        }
    }
}
