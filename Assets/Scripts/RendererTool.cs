using UnityEngine;
using System.Collections;

public class RendererTool : MonoBehaviour {

  [ContextMenu("Remove Shadow Receiver")]
  void RemoveShadowReceiver() {
    foreach (Renderer rend in GetComponentsInChildren<Renderer>()) {
      rend.receiveShadows = false;
    }
  }

}
