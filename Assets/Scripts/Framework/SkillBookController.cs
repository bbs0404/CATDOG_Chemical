using UnityEngine;
using System.Collections;

public class SkillBookController : MonoBehaviour {
	[SerializeField]
	private GameObject skillCellPrefab;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 6; i++) {
			var cell = Instantiate (skillCellPrefab) as GameObject;
			var skill = cell.GetComponent<Skill>();
			cell.transform.SetParent (this.gameObject.transform, false);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
