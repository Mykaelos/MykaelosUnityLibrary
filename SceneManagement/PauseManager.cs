using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour {


    public static void Pause() {
        Time.timeScale = 0;
    }

    public static void Unpause() {
        Time.timeScale = 1;
    }
}
