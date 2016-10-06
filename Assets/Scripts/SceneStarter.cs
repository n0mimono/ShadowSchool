using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneStarter : MonoBehaviour {
  public GameObject[] offObjs;
  public GameObject backPanel;
  public string[] sceneNames;

  public void StartScene(int type) {
    StartCoroutine (Load (type));
  }

  private IEnumerator Load(int type) {
    foreach (GameObject go in offObjs) {
      go.SetActive (false);
    }

    yield return SceneManager.LoadSceneAsync (sceneNames[type], LoadSceneMode.Additive);
    Scene scene = SceneManager.GetSceneByName (sceneNames [type]);
    SceneManager.SetActiveScene (scene);

    backPanel.SetActive (true);
  }

  public void Back() {
    SceneManager.LoadScene ("SceneStarter");
  }

}
