using UnityEngine;

public class FpsCap : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = 60; // Limite � 60 FPS
    }

}
