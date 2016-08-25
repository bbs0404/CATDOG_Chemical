using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class FadeoutText : MonoBehaviour {
    
	void Start () {
        StartCoroutine(FadeOut());
	}

    IEnumerator FadeOut()
    {
        for (float i = 1f; i>=0; i -= 0.01f)
        {
            Color color = new Color(1, 0, 0, i);
            transform.GetComponent<Text>().color = color;
            GetComponent<RectTransform>().localPosition = GetComponent<RectTransform>().localPosition + new Vector3(0, 0.5f);
            yield return 0;
        }
        Destroy(this.gameObject);
    }
}
