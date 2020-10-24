using System.Collections;
using System.Collections.Generic;
using Kuhpik;
using NaughtyAttributes;
using UnityEngine;

public class GameUIScreen : UIScreen {
    [SerializeField] [BoxGroup("Joysticks")] private Joystick leftJoystick;
    [SerializeField] [BoxGroup("Joysticks")] private Joystick rightJoystick;

    public Joystick LeftJoystick => leftJoystick;
    public Joystick RightJoystick => rightJoystick;
}
