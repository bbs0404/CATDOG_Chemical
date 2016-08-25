using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class VideoPlayer : MonoBehaviour {

    public string moviePath;
    public string nextScene;

	void Start () {
        StartCoroutine(PlayVideo());
    }
    
    IEnumerator PlayVideo() {
        Handheld.PlayFullScreenMovie(moviePath, Color.black, FullScreenMovieControlMode.Hidden, FullScreenMovieScalingMode.AspectFit);
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        Debug.Log("Video Ended");
        SceneManager.LoadScene(nextScene);
    }
}
