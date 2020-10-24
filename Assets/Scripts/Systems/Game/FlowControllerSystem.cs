using System.Collections;
using System.Collections.Generic;
using Kuhpik;
using UnityEngine;

public class FlowControllerSystem : GameSystem, IIniting, IRunning {
    [SerializeField] private Vector3 flowDirection;
    [SerializeField] private float flowSpeed = 10f;
    
    
    void IIniting.OnInit() {
        flowDirection.Normalize();
    }

    void IRunning.OnRun() {
        game.BoatRigidbody.AddForce(flowDirection * flowSpeed);
    }
}
