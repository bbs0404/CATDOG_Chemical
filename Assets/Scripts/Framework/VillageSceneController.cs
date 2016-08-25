using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VillageSceneController : MonoBehaviour {

    [SerializeField]
    Image background;

    [SerializeField]
    Sprite[] backgroundImages;

	void Start () {
        background.sprite = backgroundImages[InGameSystemManager.villageNum % 4];
	}

    public void ChangeScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}
