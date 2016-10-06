using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class ShadowProjector : MonoBehaviour {
  [Header("Light Option")]
  public Transform target;
  public Transform dirLight;
  public float     distance;
  public Vector3   offset;

  [Header("Shadow Map Options")]
  public Camera        camera;
  public int           width;
  public int           height;
  public RenderTexture shadowMap;
  public Shader        replacedShader;

  [Header("Projector Options")]
  public Projector projector;
  public Material  projMat;

  void Start() {
    CreateShadowMap ();
  }

  void LateUpdate() {
    if (shadowMap == null) CreateShadowMap ();

    // auto-tranform
    transform.position = target.position -1f * transform.forward * distance;
    transform.LookAt (target);
    transform.position += offset;

    // auto-set projector
    projector.material.SetTexture ("_ShadowTex", shadowMap);
    projector.orthographicSize = camera.orthographicSize;
  }

  private void CreateShadowMap() {
    // create shadow map
    shadowMap = new RenderTexture (width, height, 0, RenderTextureFormat.ARGB32);
    shadowMap.name = "CustomShadowMap_" + name;

    // set camera and rendering settings
    camera.depthTextureMode = DepthTextureMode.Depth;
    camera.targetTexture = shadowMap;
    camera.SetReplacementShader(replacedShader, "");

    // set projection material
    projector.material = Instantiate(projMat);
  }

}
