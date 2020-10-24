using System.Collections;
using System.Collections.Generic;
using Kuhpik;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIScreen : UIScreen {
    [SerializeField] [BoxGroup("Joysticks")] private Joystick leftJoystick;
    [SerializeField] [BoxGroup("Joysticks")] private Joystick rightJoystick;

    [SerializeField] [BoxGroup("Buttons")] private Button restartButton;

    public Joystick LeftJoystick => leftJoystick;
    public Joystick RightJoystick => rightJoystick;

    public override void Subscribe() {
        base.Subscribe();
        
        restartButton.onClick.AddListener(() => {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });
    }
}
