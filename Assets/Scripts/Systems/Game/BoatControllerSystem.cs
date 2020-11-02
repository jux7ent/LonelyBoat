using System;
using System.Collections;
using System.Collections.Generic;
using Kuhpik;
using NaughtyAttributes;
using UnityEngine;

[Serializable]
public class ForcePointsForBoat {
    public Transform forcePositionTransform;
    public Transform paddleForcePositionTranform;
}

public class BoatControllerSystem : GameSystem, IIniting, IRunning {
    [SerializeField] private ForcePointsForBoat rightPart;
    [SerializeField] private ForcePointsForBoat leftPart;

    [SerializeField] [BoxGroup("Boat movement")] private float viscosityCoeff = 0.1f;
    [SerializeField] [BoxGroup("Boat movement")] private Transform waterLevelTransform;

    [SerializeField] [BoxGroup("Boat friction")] private float movementFrictionCoeff = 0.1f;
    [SerializeField] [BoxGroup("Boat friction")] private float rotationFrictionCoeff = 0.1f;

    private Vector3 prevRightPaddleForcePointPos;
    private Vector3 prevLeftPaddleForcePointPos;
    private Vector3 prevBoatPosition;
    
    void IIniting.OnInit() {
    }

    void IRunning.OnRun() {
        ApplyForceFromPaddle(rightPart, ref prevRightPaddleForcePointPos);
        ApplyForceFromPaddle(leftPart, ref prevLeftPaddleForcePointPos);
        ApplyFrictionForce();

        prevBoatPosition = game.BoatTransform.position;
    }

    private void ApplyForceFromPaddle(ForcePointsForBoat forcePointsForBoat, ref Vector3 prevPaddlePos) {
        float divingCoeff = waterLevelTransform.position.y - forcePointsForBoat.paddleForcePositionTranform.position.y;
        if (divingCoeff < 0) divingCoeff = 0;
        divingCoeff *= divingCoeff;
        
        Vector3 currPaddleForcePos = game.BoatTransform.InverseTransformPoint(forcePointsForBoat.paddleForcePositionTranform.position);
        Vector3 tempDeltaForVelocity = currPaddleForcePos - prevPaddlePos;
        Vector3 tempDeltaForVelocityBoat = game.BoatTransform.position - prevBoatPosition;
        float paddleVelocity = (tempDeltaForVelocity + tempDeltaForVelocityBoat).sqrMagnitude;

        Vector3 forceDirection = -game.BoatTransform.TransformDirection(tempDeltaForVelocity) - tempDeltaForVelocityBoat;

        prevPaddlePos = currPaddleForcePos;
        
        game.BoatRigidbody.AddForceAtPosition(viscosityCoeff * paddleVelocity * divingCoeff * forceDirection, forcePointsForBoat.forcePositionTransform.position);
    }

    private void ApplyFrictionForce() {
        Vector3 boatMovementVelocity = game.BoatRigidbody.velocity;
        Vector3 boatAngularVelocity = game.BoatRigidbody.angularVelocity;
        
        float sqMovementVel = boatMovementVelocity.magnitude;
        float sqAngularVel = boatAngularVelocity.magnitude;
        
        game.BoatRigidbody.AddForce(-movementFrictionCoeff * sqMovementVel * boatMovementVelocity.normalized);
        game.BoatRigidbody.AddTorque(-rotationFrictionCoeff * sqAngularVel * boatAngularVelocity.normalized);
    }
}
