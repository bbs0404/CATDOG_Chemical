using UnityEngine;
using UnityEngine.UI;

public class HPBarDamageEffect: MonoBehaviour
{
    private float life = 1.0f;

    private Text text;

    void Start() {
        text = GetComponent<Text>();
    }

    void Update() {
        life -= Time.deltaTime;
        if ( life > 0.0f ) {
            transform.localPosition += new Vector3(0, 100 * Time.deltaTime, 0);
            text.color = new Color(text.color.r, text.color.g, text.color.b, life);
        }
        else {
            Destroy(gameObject);
        }
    }
}