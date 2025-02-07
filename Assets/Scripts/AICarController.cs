using System.Collections.Generic;
using UnityEngine;

public class AICarController : MonoBehaviour
{
    private Rigidbody rb;
    public List<Transform> waypoints;
    public int currentWaypointIndex;
    public float currentSteerAngle;
    public float motorForce = 650f;
    public float brakeForce = 4000f;
    public float maxSteerAngle = 45f;
    public float throttleInput = 1f;

    public Transform frontLeftWheelTransform;
    public Transform frontRightWheelTransform;
    public Transform rearLeftWheelTransform;
    public Transform rearRightWheelTransform;

    [SerializeField] private WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider, rearRightWheelCollider;

    public float waypointRange = 35;
    public float steeringThreshold = 25f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        waypoints = WaypointManager.Instance.GetWaypoints();
        currentWaypointIndex = 0;
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.levelStarted) return;
        FollowWaypoints();
        UpdateThrottleInput();
        ApplyMotor();
        ApplySteering();
        UpdateWheelVisuals();
        Debug.DrawRay(transform.position, waypoints[currentWaypointIndex].position - transform.position, Color.yellow);
    }

    private void FollowWaypoints()
    {
        if (currentWaypointIndex >= waypoints.Count)
        {
            return; 
        }

        Vector3 targetDir = waypoints[currentWaypointIndex].position - transform.position;
        float angle = Vector3.SignedAngle( transform.forward, targetDir, Vector3.up);
        currentSteerAngle = Mathf.Clamp(angle, -maxSteerAngle, maxSteerAngle);

        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < waypointRange)
        {
            currentWaypointIndex++;
        }
    }


    private void UpdateThrottleInput()
    {
        float carSpeed = rb.velocity.magnitude;

        if (Mathf.Abs(currentSteerAngle) > steeringThreshold)
        {
            throttleInput = Mathf.Lerp(throttleInput, 0f, Time.deltaTime * 2f);
        }
        else
        {
            float currentAngle = Mathf.Abs(currentSteerAngle);
            float maximumAngle = maxSteerAngle;
            float calculatedThrottleInput = Mathf.Clamp01((1f - Mathf.Abs(carSpeed * 0.01f * currentAngle) / maximumAngle));

            throttleInput = calculatedThrottleInput;
        }
    }




    private void ApplyMotor()
    {
        foreach (WheelCollider wheel in GetComponentsInChildren<WheelCollider>())
        {
            wheel.motorTorque = throttleInput * motorForce;
        }
    }

    private void ApplySteering()
    {
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;

        frontLeftWheelTransform.localEulerAngles = new Vector3(0, currentSteerAngle, 0);
        frontRightWheelTransform.localEulerAngles = new Vector3(0, currentSteerAngle, 0);
    }

    private void UpdateWheelVisuals()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Brakes"))
        {
            ApplyBrakes();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Brakes"))
        {
            StopBrakes();
        }
    }

    private void ApplyBrakes()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotationZ;

        frontRightWheelCollider.brakeTorque = brakeForce;
        frontLeftWheelCollider.brakeTorque = brakeForce;
        rearLeftWheelCollider.brakeTorque = brakeForce;
        rearRightWheelCollider.brakeTorque = brakeForce;
    }

    private void StopBrakes()
    {
        rb.constraints = RigidbodyConstraints.None;
        frontRightWheelCollider.brakeTorque = 0f;
        frontLeftWheelCollider.brakeTorque = 0f;
        rearLeftWheelCollider.brakeTorque = 0f;
        rearRightWheelCollider.brakeTorque = 0f;
    }
}
