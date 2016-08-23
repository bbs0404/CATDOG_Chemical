using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class OptionButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void OnClick()
    {
        SceneManager.LoadScene("Option");
        Debug.Log("Button Clicked!!");
    }

}
