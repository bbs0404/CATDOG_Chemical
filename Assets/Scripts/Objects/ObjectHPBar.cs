using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ObjectHPBar : MonoBehaviour {

    [SerializeField]
    private float width = 130f;
    [SerializeField]
    private RectTransform face;
    [SerializeField]
    private Text text; 

	void Start () {}
	void Update () {}

    public void SetValue(float hp, float maxhp) {
        face.sizeDelta = new Vector2(width * (hp / maxhp), face.sizeDelta.y);
        text.text = string.Format("{0} / {1}", Mathf.RoundToInt(hp * 10), Mathf.RoundToInt(maxhp * 10));
    }
}
