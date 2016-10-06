using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class ShadowProjector : MonoBehaviour {
  [Header("Light Option")]
  public Transform dirLight;
  public float     distance;

  [Header("Shadow Map Options")]
  public int width;
  public int height;
  public RenderTexture shadowMap;
  public Shader        replacedShader;

  [Header("Projector Options")]
  public Projector projector;
  public Material  projMat;

  private Camera camera;

  void LateUpdate() {
    if (shadowMap == null) CreateShadowMap ();

    // auto-tranform
    transform.forward = dirLight.forward;
    transform.position = -1f * transform.forward * distance;

    // auto-set projector
    projector.orthographicSize = camera.orthographicSize;
  }

  private void CreateShadowMap() {
    // create shadow map
    shadowMap = new RenderTexture (width, height, 0, RenderTextureFormat.ARGB32);
    shadowMap.name = "CustomShadowMap_" + name;

    // set camera and rendering settings
    camera = GetComponent<Camera> ();
    camera.depthTextureMode = DepthTextureMode.Depth;
    camera.targetTexture = shadowMap;
    camera.SetReplacementShader(replacedShader, "");

    // set projection material
    //projMat = Instantiate(projMat);
    //projMat.SetTexture ();
    //projector.material = projMat;
  }

}
