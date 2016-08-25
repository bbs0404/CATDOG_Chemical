using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class VideoPlayer : MonoBehaviour {

    public float movieLength;
    public string nextScene;

    MovieTexture movie;
    float time = 0;

	void Start () {
        movie = GetComponent<RawImage>().texture as MovieTexture;
        movie.Play();
        time = 0;
	}

    void Update() {
        time += Time.deltaTime;
        if (time > movieLength) {
            SceneManager.LoadScene(nextScene);
        }
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) {
            if (movie.isPlaying)
                movie.Pause();
            else
                movie.Play();
        }
    }

}
