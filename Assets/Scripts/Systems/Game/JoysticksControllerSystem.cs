using System.Collections;
using System.Collections.Generic;
using Kuhpik;
using UnityEngine;

public class JoysticksControllerSystem : GameSystemWithScreen<GameUIScreen>, IIniting, IRunning {
    void IIniting.OnInit() {
        
    }

    void IRunning.OnRun() {
        print(screen.LeftJoystick.Direction);
    }
}
