using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneManagerOfInventory : MonoBehaviour {

    
    public void OnGUI(int a)
    {
       
        SceneManager.UnloadScene(a);
        SceneManager.LoadSceneAsync(a,LoadSceneMode.Additive);
        Debug.Log("성공!");
    }
}
