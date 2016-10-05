using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class ShadowCamera : MonoBehaviour {
  [Header("Light Option")]
  public Transform dirLight;
  public float     distance;

  [Header("Shadow Map Options")]
  public int width;
  public int height;
  public RenderTexture shadowMap;

  void Update() {
    if (shadowMap == null) CreateShadowMap();

    transform.forward = dirLight.forward;
    transform.position = -1f * transform.forward * distance;

    Camera camera = GetComponent<Camera> ();
    Matrix4x4 view   = camera.worldToCameraMatrix;
    Matrix4x4 proj   = GL.GetGPUProjectionMatrix (camera.projectionMatrix, false);
    Matrix4x4 vp     = proj * view;
    Shader.SetGlobalMatrix ("_CustomShadowViewProj", vp);
    Shader.SetGlobalVector ("_WorldSpaceShadowCameraPos", transform.position);

    Vector4 zBufferParams;
    zBufferParams.x = 1f - camera.farClipPlane / camera.nearClipPlane;
    zBufferParams.y = camera.farClipPlane / camera.nearClipPlane;
    zBufferParams.z = zBufferParams.x / camera.farClipPlane;
    zBufferParams.w = zBufferParams.y / camera.farClipPlane;
    Shader.SetGlobalVector ("_CustomShadowZBufferParams", zBufferParams);
  }

  private void CreateShadowMap() {
    Camera camera = GetComponent<Camera> ();
    camera.depthTextureMode = DepthTextureMode.Depth;

    shadowMap = new RenderTexture (width, height, 24, RenderTextureFormat.Depth);
    shadowMap.name = "Custom Shadow Map";

    camera.targetTexture = shadowMap;
    Shader.SetGlobalTexture ("_CustomShadowMap", shadowMap);

    camera.SetReplacementShader (Shader.Find("DepthEncoder"), "");
  }

}
