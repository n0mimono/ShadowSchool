using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class ShadowManager : MonoBehaviour {
  [Header("UI")]
  public List<GameObject> panels;
  public Slider sliderCamera;
  public Slider sliderShadow;

  [Header("Objects")]
  public Transform cameraTrans;

  [Header("Mesh")]
  public List<Mesh> meshes;

  public GameObject cubeBase;
  public GameObject planeBase;

  public GameObject caster;
  public GameObject receiver;

  public List<GameObject> cubeManies;
  public List<GameObject> planeManies;
  public GameObject planeOut;

  // screen data
  private Resolution scrRes;
  private int curRes = 0;
  private float[] res = { 1f, 0.5f, 0.25f }; 

  // mesh mode
  private int curMesh = 0;

  // scale mode
  private int curCubeScale = 0;
  private int curPlaneScale = 0;
  private float[] scl = { 1f, 2f, 0.5f };

  // many
  private int curCubeMany = 0;
  private int curPlaneMany = 0;

  void Start() {
    Application.targetFrameRate = 60;

    scrRes = Screen.currentResolution;
  }

  public void ChangeResolution() {
    curRes = (curRes + 1) % 3;
    Screen.SetResolution ((int)(scrRes.width * res [curRes]), (int)(scrRes.height * res [curRes]), true);
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
    cubeBase.SetActive (!cubeBase.activeInHierarchy);
  }

  public void SwitchCubeMany() {
    curCubeMany = (curCubeMany + 1) % 4;

    cubeManies.ForEach (m => m.SetActive (false));
    if (curCubeMany != 0) {
      cubeManies [curCubeMany - 1].SetActive (true);
    }
  }

  public void SwitchCubeScale() {
    curCubeScale = (curCubeScale + 1) % 3;

    Transform[] transes = caster.GetComponentsInChildren<Transform> (true);
    foreach (Transform trans in transes) {
      trans.localScale = Vector3.one * scl[curCubeScale];
    }
  }

  public void SwitchSpehre() {
    curMesh = (curMesh + 1) % 2;

    MeshFilter[] filters = caster.GetComponentsInChildren<MeshFilter> (true);
    foreach (MeshFilter filter in filters) {
      filter.sharedMesh = meshes [curMesh];
    }
  }

  public void SwitchPlaneBase() {
    planeBase.SetActive (!planeBase.activeInHierarchy);
  }

  public void SwitchPlaneMany() {
    curPlaneMany = (curPlaneMany + 1) % 4;

    planeManies.ForEach (m => m.SetActive (false));
    if (curPlaneMany != 0) {
      planeManies [curPlaneMany - 1].SetActive (true);
    }
  }

  public void SwitchPlaneScale() {
    curPlaneScale = (curPlaneScale + 1) % 3;

    Transform[] transes = receiver.GetComponentsInChildren<Transform> (true);
    foreach (Transform trans in transes) {
      trans.localScale = Vector3.one * scl[curPlaneScale];
    }
  }

  public void SwitchPlaneOut() {
    planeOut.SetActive (!planeOut.activeInHierarchy);
  }

  public void ShowPanel() {
    bool isActive = !panels [0].activeInHierarchy;
    panels.ForEach (p => p.SetActive (isActive));
  }

  public void OnChangeSliderCamera() {
    cameraTrans.localPosition = Vector3.forward * sliderCamera.value;
  }

  public void OnChangeSliderShadow() {
    QualitySettings.shadowDistance = sliderShadow.value;
  }

}
