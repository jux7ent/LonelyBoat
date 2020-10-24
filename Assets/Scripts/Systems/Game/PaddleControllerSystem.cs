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
    [SerializeField] [BoxGroup("Paddles movement")] private Vector2 minMaxAbsForward = new Vector2(-30, 30f);
    [SerializeField] [BoxGroup("Paddles movement")] private Vector2 minMaxAbsUp= new Vector2(-30f, 30f);

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

    // тут говно полное, оси перекручены и тп, что пздц
    private void PaddleMovement(Transform paddleTransform, Vector2 joystickDirection, bool incorrectTopBottom=false) {
        Vector3 angles = paddleTransform.localEulerAngles;
        
        joystickDirection *= cachedAnglesMultiplier;

        ///////////// пытаюсь как-то выкрутиться, тк для каждого весла свои направления
        float coeffInversion = 1;
        float coeffNotInversion = -1;

        if (incorrectTopBottom) {
            coeffInversion = -1;
            coeffNotInversion = 1;
        }
        ////////////////////
        
        joystickDirection.x = Mathf.Clamp(joystickDirection.x, minMaxAbsForward.x, minMaxAbsForward.y);
        joystickDirection.y = Mathf.Clamp(-joystickDirection.y, coeffNotInversion * minMaxAbsUp.x, coeffNotInversion * minMaxAbsUp.y);
        
        float temp = joystickDirection.x;
        // тут перепутаты оси.
        joystickDirection.x = Mathf.LerpAngle(angles.x, joystickDirection.y, lerpCoeffPaddlesMovement);
        joystickDirection.y = Mathf.LerpAngle(angles.y,temp, lerpCoeffPaddlesMovement);

        paddleTransform.localEulerAngles = joystickDirection;
    }
}
