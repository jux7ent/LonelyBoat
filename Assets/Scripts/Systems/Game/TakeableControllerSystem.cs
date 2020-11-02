using System.Collections;
using System.Collections.Generic;
using Kuhpik;
using UnityEngine;

public class TakeableControllerSystem : GameSystem, IIniting {
    private bool systemInited = false;

    void IIniting.OnInit() {
        if (!systemInited) {
            game.BoatCollisionListener.TriggerEnterEvent += TryTakeObj;
            systemInited = true;
        }
    }

    private void TryTakeObj(Transform other) {
        if (other.CompareTag(Constants.Tags.Takeable)) {
            other.gameObject.SetActive(false);
        }
    }
}
