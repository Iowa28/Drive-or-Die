using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float maxSteerAngle;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private float dragForce;
    [SerializeField] private Transform centerMass;

    [Header("Flying")] 
    [SerializeField] private float upperBorder = 50f;
    [SerializeField] private float lowerBorder = -5f;

    [Header("Wheel Collider")]
    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;
    
    [Header("Wheel Transform")]
    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;

    private Rigidbody rigidbody;
    private bool stop;
    private float horizontalInput;
    private float currentSteerAngle;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.centerOfMass = centerMass.position;
    }

    void Update()
    {
        if (stop)
            return;
        
        if (GameManager.IsGameOver())
            StopCar();

        horizontalInput = Input.GetAxis("Horizontal");

        HandleMotor();
        HandleSteering();
        UpdateWheels();
        CheckFlying();
    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = -speed;
        frontRightWheelCollider.motorTorque = -speed;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }
    
    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }
    
    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos = wheelTransform.position;
        Quaternion rot = wheelTransform.rotation;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }

    public void StopCar()
    {
        stop = true;
        rigidbody.drag = dragForce;
    }
    
    public void StartCar()
    {
        stop = false;
        rigidbody.drag = 0;
    }

    private void CheckFlying()
    {
        if (transform.position.y >= upperBorder || transform.position.y <= lowerBorder)
        {
            gameManager.EndGame();
        }
    }
}
