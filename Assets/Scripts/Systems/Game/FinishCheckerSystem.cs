using System.Collections;
using System.Collections.Generic;
using Kuhpik;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishCheckerSystem : GameSystem, IIniting {
    [SerializeField] private CollisionListener boatCollisionListener;
    
    private bool systemInited = false;
    
    void IIniting.OnInit() {
        if (!systemInited) {
            boatCollisionListener.CollisionEnterEvent += CollisionEnterHandler;
            systemInited = true;
        }
    }

    private void CollisionEnterHandler(Transform other) {
        if (other.CompareTag(Constants.Tags.Rock)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
