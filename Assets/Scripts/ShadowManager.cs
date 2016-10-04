using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class ShadowManager : MonoBehaviour {
  [Header("UI")]
  public List<GameObject> panels;
  public Slider slider;

  [Header("Objects")]
  public Transform cameraTrans;

  // screen data
  private int scrWidth;
  private int scrHeight;
  private int curRes = 0;
  private float[] res = { 1f, 0.5f, 0.25f }; 

  void Start() {
    Application.targetFrameRate = 60;

    scrWidth = Screen.width;
    scrHeight = Screen.height;
  }

  public void ChangeResolution() {
    curRes = (curRes + 1) % 3;
    Screen.SetResolution ((int)(scrWidth * res [curRes]), (int)(scrHeight + res [curRes]), true);
  }

  public void ChangeShadow() {
    Light light = FindObjectOfType<Light> ();
    light.shadows = (LightShadows)(((int)(light.shadows) + 1) % 3);
  }

  public void ChangeQuality() {
    Light light = FindObjectOfType<Light> ();
    light.shadowResolution = (LightShadowResolution)(((int)(light.shadowResolution) + 1) % 4);
  }

  public void SwitchCubeBase() {
  }

  public void SwitchCubeMany() {
  }

  public void SwitchCubeScale() {
  }

  public void SwitchSpehre() {
  }

  public void SwitchPlaneBase() {
  }

  public void SwitchPlaneMany() {
  }

  public void SwitchPlaneScale() {
  }

  public void SwitchPlaneOut() {
  }

  public void ShowPanel() {
    bool isActive = !panels [0].activeInHierarchy;
    panels.ForEach (p => p.SetActive (isActive));
  }

  public void OnChangeSlider() {
    cameraTrans.localPosition = Vector3.forward * slider.value;
  }

}
