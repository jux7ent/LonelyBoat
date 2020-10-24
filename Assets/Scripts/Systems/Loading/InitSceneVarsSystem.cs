using System.Collections;
using System.Collections.Generic;
using Kuhpik;
using UnityEngine;

public class InitSceneVarsSystem : GameSystem, IIniting {
    [SerializeField] private Rigidbody boatRigidbody;

    private bool systemInited = false;

    void IIniting.OnInit() {
        if (!systemInited) {
            game.SetVariables(boatRigidbody, Camera.main);
            
            systemInited = true;
        }
    }
}
