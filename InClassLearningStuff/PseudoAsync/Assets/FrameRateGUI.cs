using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRateGUI : MonoBehaviour
{
    public float measurementWindow = 1.0f;

    private float frameRateTimer = 0.0f;
    private float frameRate = 0.0f;
    private int frameCount = 0;

    private System.DateTime lastFrameUpdate;

	// Use this for initialization
	void Start ()
    {
        lastFrameUpdate = System.DateTime.Now;
        ResetTimer();
	}

    void ResetTimer()
    {
        frameRateTimer = 0.0f;
        frameCount = 0;
    }

    // Update is called once per frame
    void Update ()
    {
        float elapsedTime = (float)(System.DateTime.Now - lastFrameUpdate).TotalSeconds;
        lastFrameUpdate = System.DateTime.Now;

        frameCount++;
        frameRateTimer += elapsedTime;
        if (frameRateTimer >= measurementWindow)
        {
            frameRate = (float)frameCount / frameRateTimer;
            ResetTimer();
        }

	}

    public void OnGUI()
    {
        if (frameRate > 0f)
        {
            GUILayout.Label(frameRate.ToString("0.00"));
        }
    }
}
