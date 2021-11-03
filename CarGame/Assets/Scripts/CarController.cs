using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Car Settings")]
    public float accelerationfactor = 30.0f;
    public float turnfactor = 3.5f;

    float accelerationInput = 0;
    float steeringInput = 0;
    float rotationAngle = 0;

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
        
    }

    void fixedUpdate() 
    {
        ApplyEngineForce();

        ApplySteering();


    }

    void ApplyEngineForce() {
        Vector2 engineForceVector = transform.up * accelerationInput * accelerationfactor;

        carRigidBody2D.AddForce(engineForceVector, ForceMode2D.Force);
    }

    void ApplySteering() {
        rotationAngle -= steeringInput * turnfactor;

        carRigidBody2D.MoveRotation(rotationAngle);
    }

    public void SetInputFactor(Vector2 inputVector){
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
    }
}
