using System.Collections;
using System.Collections.Generic;
using Kuhpik;
using NaughtyAttributes;
using UnityEngine;

public class PaddleControllerSystem : GameSystemWithScreen<GameUIScreen>, IIniting, IRunning {
    [SerializeField] [BoxGroup("Paddles")] private Transform leftPaddleTransform;
    [SerializeField] [BoxGroup("Paddles")] private Transform rightPaddleTransform;
    
    void IIniting.OnInit() {
        
    }

    void IRunning.OnRun() {
        print(screen.LeftJoystick.Direction);
    }
}
