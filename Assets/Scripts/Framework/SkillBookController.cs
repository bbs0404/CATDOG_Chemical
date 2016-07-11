using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkillBookController : MonoBehaviour {
	public static int SKILL_PER_PAGE = 8;

	[SerializeField]
	private GameObject skillCellPrefab;
	private int page;

	// UI
	[SerializeField]
	private GameObject skillPage;

	[SerializeField]
	private Button prevButton, nextButton;

	[SerializeField]
	private Text pageLabel;

	// Singleton Manager
	private SkillManager skillMgr;

	// Use this for initialization
	void Start () {
		skillMgr = SkillManager.Inst();
		page = 1;

		// TODO: remove block
		// Create temporary skills.
		skillMgr.addNewSkill("CHOP", 111, Type.foo);
		skillMgr.addNewSkill ("HO", 11, Type.foo);
		skillMgr.addNewSkill ("OHOH", 55, Type.foo);
		skillMgr.addNewSkill ("OHOHA", 55, Type.foo);
		skillMgr.addNewSkill ("OHOHB", 55, Type.foo);
		skillMgr.addNewSkill ("OHOHC", 55, Type.foo);
		skillMgr.addNewSkill ("OHOHD", 55, Type.foo);
		skillMgr.addNewSkill ("OHOHE", 55, Type.foo);
		skillMgr.addNewSkill ("OHOHF", 55, Type.foo);
		skillMgr.addNewSkill ("OHOHF", 55, Type.foo);
		skillMgr.addNewSkill ("OHOHF", 55, Type.foo);
		skillMgr.addNewSkill ("OHOHF", 55, Type.foo);
		skillMgr.addNewSkill ("OHOHF", 55, Type.foo);
		skillMgr.addNewSkill ("OHOHF", 55, Type.foo);
		skillMgr.addNewSkill ("OHOHF", 55, Type.foo);
		skillMgr.addNewSkill ("OHOHF", 55, Type.foo);
		skillMgr.addNewSkill ("OHOHF", 55, Type.foo);
		skillMgr.addNewSkill ("OHOHF", 55, Type.foo);
		skillMgr.getSkillListAll () [0].unlocked = true;
		skillMgr.getSkillListAll () [1].unlocked = true;
		skillMgr.getSkillListAll () [3].unlocked = true;
		skillMgr.getSkillListAll () [7].unlocked = true;
		skillMgr.getSkillListAll () [9].unlocked = true;

		refreshPage ();
	}


	void refreshPage () {
		var skillList = skillMgr.getSkillListAll ();

		int offset = (page - 1) * SKILL_PER_PAGE;
		int count = SKILL_PER_PAGE;
		if (count > skillList.Count - offset) {
			count = skillList.Count - offset;
		}

		// Clear page
		foreach (Transform child in skillPage.transform) {
			Destroy (child.gameObject);
		}

		// Write page
		foreach (var skill in skillList.GetRange (offset, count)) {
			var cell = Instantiate (skillCellPrefab) as GameObject;

			var text = cell.transform.FindChild ("Combination Text").GetComponent<Text>();
			text.text = skill.Combination;

			var hideImage = cell.transform.FindChild ("Hide Image").GetComponent<Image>();
			if (!skill.unlocked) {
				hideImage.gameObject.SetActive (true);
			}

			cell.transform.SetParent (skillPage.transform, false);
		}

		// Update label
		var last_page = (skillList.Count - 1) / SKILL_PER_PAGE + 1;
		pageLabel.text = System.String.Format("{0:d} / {1:d}", page, last_page);

		// Enable/disable Prev button
		if (isPageExists(page - 1)) {
			prevButton.interactable = true;
		} else {
			prevButton.interactable = false;
		}

		// Enable/disable Next button
		if (isPageExists(page + 1)) {
			nextButton.interactable = true;
		} else {
			nextButton.interactable = false;
		}
	}

	public bool isPageExists(int targetPage) {
		if (targetPage < 1) {
			return false;
		}
		if ((targetPage - 1) * SKILL_PER_PAGE >= skillMgr.getSkillListAll ().Count) {
			return false;
		}
		return true;
	}

	public void navigatePage(int delta) {
		int newPage = page + delta;
		if (!isPageExists(newPage)) {
			Debug.Log("Error: navigate to wrong page.");
			return;
		}
		page = newPage;

		refreshPage ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
