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

    private Vector3 prevRightPaddleForcePointPos;
    private Vector3 prevLeftPaddleForcePointPos;
    
    void IIniting.OnInit() {
        
    }

    void IRunning.OnRun() {
        ApplyForceFromPaddle(rightPart, ref prevRightPaddleForcePointPos);
        ApplyForceFromPaddle(leftPart, ref prevLeftPaddleForcePointPos);
    }

    private void ApplyForceFromPaddle(ForcePointsForBoat forcePointsForBoat, ref Vector3 prevPaddlePos) {
        float divingCoeff = waterLevelTransform.position.y - forcePointsForBoat.paddleForcePositionTranform.position.y;
        if (divingCoeff < 0) divingCoeff = 0;

        Vector3 tempDeltaForVelocity = forcePointsForBoat.paddleForcePositionTranform.position - prevPaddlePos;
        float paddleVelocity = tempDeltaForVelocity.sqrMagnitude;

        Vector3 forceDirection =
            Vector3.Cross(-tempDeltaForVelocity,
                forcePointsForBoat.forcePositionTransform.position -
                forcePointsForBoat.paddleForcePositionTranform.position).normalized;

        Debug.DrawRay(forcePointsForBoat.forcePositionTransform.position, tempDeltaForVelocity * 10f, Color.magenta);
        Debug.DrawRay(forcePointsForBoat.forcePositionTransform.position, forcePointsForBoat.paddleForcePositionTranform.position - forcePointsForBoat.forcePositionTransform.position, Color.green);
        Debug.DrawRay(forcePointsForBoat.forcePositionTransform.position, forceDirection, Color.red);

        prevPaddlePos = forcePointsForBoat.paddleForcePositionTranform.position;
        
        boatRigidbody.AddForceAtPosition(viscosityCoeff * paddleVelocity * divingCoeff * forceDirection, forcePointsForBoat.forcePositionTransform.position);
    }
}
