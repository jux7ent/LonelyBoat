using System.Collections;
using System.Collections.Generic;
using Kuhpik;
using NaughtyAttributes;
using UnityEngine;

public class PaddleControllerSystem : GameSystemWithScreen<GameUIScreen>, IIniting, IRunning {
    // x < 0 -> down; y > 0 -> forward
    [SerializeField] [BoxGroup("Paddles")] private Transform leftPaddleTransform;
    [SerializeField] [BoxGroup("Paddles")] private Transform rightPaddleTransform;

    [SerializeField] [BoxGroup("Paddles movement")] private float joystickMultiplier = 0.2f;
    [SerializeField] [BoxGroup("Paddles movement")] private float lerpCoeffPaddlesMovement = 0.125f;

    private Vector2 cachedJoystickDirection;
    private float cachedAnglesMultiplier;

    void IIniting.OnInit() {
        cachedAnglesMultiplier = 360f * joystickMultiplier;
    }

    void IRunning.OnRun() {
        if (Input.GetMouseButton(0)) {
            PaddleMovement(rightPaddleTransform, screen.RightJoystick.Direction, true);
            PaddleMovement(leftPaddleTransform, screen.LeftJoystick.Direction);
        }
    }

    private void PaddleMovement(Transform paddleTransform, Vector2 joystickDirection, bool incorrectTopBottom=false) {
        Vector3 angles = paddleTransform.localEulerAngles;
        
        if (incorrectTopBottom) {
            joystickDirection.y *= -1;
        }
        
        float temp = joystickDirection.x;
        joystickDirection.x = Mathf.LerpAngle(angles.x, joystickDirection.y * cachedAnglesMultiplier, lerpCoeffPaddlesMovement);
        joystickDirection.y = Mathf.LerpAngle(angles.y,temp * cachedAnglesMultiplier, lerpCoeffPaddlesMovement);

        paddleTransform.localEulerAngles = joystickDirection;
    }
}
