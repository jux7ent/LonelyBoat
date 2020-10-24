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
    [SerializeField] private Rigidbody boatRigidbody;

    [SerializeField] private ForcePointsForBoat rightPart;
    [SerializeField] private ForcePointsForBoat leftPart;

    [SerializeField] [BoxGroup("Boat movement")] private float viscosityCoeff = 0.1f;
    [SerializeField] [BoxGroup("Boat movement")] private Transform waterLevelTransform;
    [SerializeField] private float test = 1f;

    private Vector3 prevRightPaddleForcePointPos;
    private Vector3 prevLeftPaddleForcePointPos;
    private Vector3 prevBoatPosition;
    
    void IIniting.OnInit() {
    }

    void IRunning.OnRun() {
        ApplyForceFromPaddle(rightPart, ref prevRightPaddleForcePointPos);
        ApplyForceFromPaddle(leftPart, ref prevLeftPaddleForcePointPos);

        prevBoatPosition = boatRigidbody.transform.position;
    }

    private void ApplyForceFromPaddle(ForcePointsForBoat forcePointsForBoat, ref Vector3 prevPaddlePos) {
        float divingCoeff = waterLevelTransform.position.y - forcePointsForBoat.paddleForcePositionTranform.position.y;
        if (divingCoeff < 0) divingCoeff = 0;
        divingCoeff *= divingCoeff;
        
        Vector3 currPaddleForcePos = boatRigidbody.transform.InverseTransformPoint(forcePointsForBoat.paddleForcePositionTranform.position);
        Vector3 tempDeltaForVelocity = test*(currPaddleForcePos - prevPaddlePos);
        Vector3 tempDeltaForVelocityBoat = boatRigidbody.transform.position - prevBoatPosition;
        float paddleVelocity = (tempDeltaForVelocity + tempDeltaForVelocityBoat).sqrMagnitude;

        Vector3 forceDirection = -boatRigidbody.transform.TransformDirection(tempDeltaForVelocity) - tempDeltaForVelocityBoat;
           /* Vector3.Cross(-tempDeltaForVelocity + tempDeltaForVelocityBoat,
                forcePointsForBoat.forcePositionTransform.position -
                forcePointsForBoat.paddleForcePositionTranform.position).normalized;*/

      //  Debug.DrawRay(forcePointsForBoat.forcePositionTransform.position, (-tempDeltaForVelocity + tempDeltaForVelocityBoat) * 10f, Color.magenta);
     //   Debug.DrawRay(forcePointsForBoat.forcePositionTransform.position, forcePointsForBoat.paddleForcePositionTranform.position - forcePointsForBoat.forcePositionTransform.position, Color.green);
        Debug.DrawRay(forcePointsForBoat.forcePositionTransform.position, forceDirection * 100f, Color.red);

        prevPaddlePos = currPaddleForcePos;
        
        boatRigidbody.AddForceAtPosition(viscosityCoeff * paddleVelocity * divingCoeff * forceDirection, forcePointsForBoat.forcePositionTransform.position);
    }
}
