using System.Collections;
using System.Collections.Generic;
using Kuhpik;
using NaughtyAttributes;
using UnityEngine;

public class CameraChasingSystem : GameSystem, IIniting {
    [SerializeField] private float chasingLerpCoeffForPos = 0.125f;
    [SerializeField] private float chasingLerpCoeffForRot = 0.08f;
    [SerializeField] private float yOffsetForAngle = 90f;
    
    private Vector3 deltaFromCameraToBoatPos;

    private Vector3 cachedCameraPos;
    private Vector3 cachedCameraEulerAngles;
    
    void IIniting.OnInit() {
        deltaFromCameraToBoatPos = game.MainCameraTransform.position - game.BoatTransform.position;
    }

    void FixedUpdate() {
        cachedCameraPos = game.MainCameraTransform.position;
        cachedCameraPos = Vector3.Lerp(cachedCameraPos, game.BoatTransform.position + deltaFromCameraToBoatPos, chasingLerpCoeffForPos);

        cachedCameraEulerAngles = game.MainCameraTransform.eulerAngles;
        cachedCameraEulerAngles.y = Mathf.LerpAngle(cachedCameraEulerAngles.y,
            game.BoatTransform.eulerAngles.y + yOffsetForAngle, chasingLerpCoeffForRot);
        
        
        game.MainCameraTransform.SetPositionAndRotation(cachedCameraPos, Quaternion.Euler(cachedCameraEulerAngles));
    }
}
