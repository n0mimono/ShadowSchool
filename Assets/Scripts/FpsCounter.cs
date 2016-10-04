using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]
public class FpsCounter : MonoBehaviour {
  private Text text;

  private int frameCount;
  private float prevTime;

  void Start() {
    Application.targetFrameRate = 60;

    text = GetComponent<UnityEngine.UI.Text> ();

    frameCount = 0;
    prevTime = 0.0f;
  }

  void Update() {
    
    ++frameCount;
    float time = Time.realtimeSinceStartup - prevTime;

    if (time >= 0.5f) {
      text.text = (frameCount / time).ToString ("F1");

      frameCount = 0;
      prevTime = Time.realtimeSinceStartup;
    }

  }
}
