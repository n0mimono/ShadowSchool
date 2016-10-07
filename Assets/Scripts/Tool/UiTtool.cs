using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UiTtool : MonoBehaviour {

  [ContextMenu("Remove Raycast Target From All Texts")]
  void RemoveRaycastTargetFromAllTexts() {
    foreach (Text text in FindObjectsOfType<Text>()) {
      text.raycastTarget = false;
    }
  }

}
