using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInputHandler : MonoBehaviour
{

    //Componenents
    CarController carController;

    void Awake() {
        carController = GetComponent<CarController>();
    }

    // Start is called before the first frame upd

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = Vector2.zero;

        inputVector.x = inputVector.GetAxis("Horizontal");
        inputVector.x = inputVector.GetAxis("Vertical");

        carController.SetInputVector(inputvector);
        
    }
}
