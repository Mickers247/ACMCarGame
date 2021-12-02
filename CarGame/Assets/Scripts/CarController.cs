using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Car Settings")]
    public float accelerationfactor = 1.5f;
    public float turnfactor = .5f;
    public float driftfactor = .095f;
    public float maxSpeed = 20;

    float accelerationInput = 0;
    float steeringInput = 0;
    float rotationAngle = 0;
    float velocityVsUp = 0;

    //Componenets
    Rigidbody2D carRigidBody2D;

    void Awake() {
        carRigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ApplyEngineForce();

        KillOrthogonalVelocity();

        ApplySteering();
    }

    void fixedUpdate() 
    {



    }

    void ApplyEngineForce() {

        velocityVsUp = Vector2.Dot(transform.up, carRigidBody2D.velocity);


        if (velocityVsUp > maxSpeed && accelerationInput > 0)
            return;

        if (velocityVsUp < -maxSpeed * 0.5f && accelerationInput < 0)
            return;

        if (carRigidBody2D.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0)
            return;

        if (accelerationInput == 0)
            carRigidBody2D.drag = Mathf.Lerp(carRigidBody2D.drag, 3.0f, Time.fixedDeltaTime * 3);
        else carRigidBody2D.drag = 0;

        Vector2 engineForceVector = transform.up * accelerationInput * accelerationfactor;

        carRigidBody2D.AddForce(engineForceVector, ForceMode2D.Force);
    }

    void ApplySteering() {
        float minSpeedBeforeAllowTurningFactor = (carRigidBody2D.velocity.magnitude / 8);

        minSpeedBeforeAllowTurningFactor = Mathf.Clamp01(minSpeedBeforeAllowTurningFactor);

        rotationAngle -= steeringInput * turnfactor * minSpeedBeforeAllowTurningFactor;

        carRigidBody2D.MoveRotation(rotationAngle);
    }

    void KillOrthogonalVelocity()
    {
        //Get forward and right velocity of the car
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidBody2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidBody2D.velocity, transform.right);

        //Kill the orthogonal velocity (side velocity) based on how much the car should drift. 
        carRigidBody2D.velocity = forwardVelocity + rightVelocity * driftfactor;
    }

    public void SetInputVector(Vector2 inputVector){
        
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
    }
}
