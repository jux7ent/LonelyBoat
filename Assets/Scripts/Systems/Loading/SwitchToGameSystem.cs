using System.Collections;
using System.Collections.Generic;
using Kuhpik;
using UnityEngine;

public class SwitchToGameSystem : GameSystem, IIniting {
    void IIniting.OnInit() {
        Bootstrap.ChangeGameState(EGamestate.Game);
    }
}
