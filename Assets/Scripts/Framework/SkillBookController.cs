using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkillBookController : MonoBehaviour {
	public static int SKILL_PER_PAGE = 8;

	[SerializeField]
	private GameObject skillCellPrefab;
	private int page, sortKey;

	// UI
	[SerializeField]
	private GameObject skillPage;

	[SerializeField]
	private Button prevButton, nextButton;

	[SerializeField]
	private Text pageLabel;

	// Singleton Manager
	private SkillManager skillMgr;
	private ElementManager elemMgr;

	// Use this for initialization
	void Start () {
		skillMgr = SkillManager.Inst();
		elemMgr = ElementManager.Inst ();
		page = 1;
		sortKey = 0;

        refreshPage ();
	}


	// TODO: Skill에 Computed property로 넣으면 좋을듯 합니다.
	private int computeSkillCost(Skill skill) {
		int sum = 0;
		foreach (char elementSymbol in skill.Combination) {
			sum += elemMgr.getElement (elementSymbol).cost;
		}
		return sum;
	}


	void refreshPage () {
		/* * TODO: Apply sorting - 상성
		 * 오후 11:51 이지혜 컴16 코스트/상성/데미지 이정도면 될거 같습니다
		 * 상성은 기체>액체>고체 순으로 정렬되게 해주세요
		 * */
		var skillList = skillMgr.getSkillListAll ();

		// Sort
		switch (sortKey) {
		case 0:
			// Cost
			skillList.Sort(delegate(Skill x, Skill y) {
				int costX = computeSkillCost(x);
				int costY = computeSkillCost(y);
				return ((costX == costY) ? 0 : (costX < costY) ? -1 : 0);
			});
			break;
		case 1:
			// Damage
			skillList.Sort (delegate(Skill x, Skill y) {
				return ((x.damage == y.damage) ? 0 : (x.damage < y.damage) ? -1 : 0);
			});
			break;
		case 2:
			// 상성
			// TODO:
			break;
		}

		// Pagination
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

//			var text = cell.transform.FindChild ("Combination Text").GetComponent<Text>();
//			text.text = skill.Combination;

			var hideImage = cell.transform.FindChild ("Hide Image").GetComponent<Image>();
			if (!skill.unlocked) {
				hideImage.gameObject.SetActive (true);
			}

			cell.transform.SetParent (skillPage.transform, false);
            cell.GetComponent<Image>().sprite = skill.sprite;
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

	private void navigateTo(int newPage) {
		if (!isPageExists(newPage)) {
			Debug.Log("Error: navigate to wrong page.");
			return;
		}
		page = newPage;

		refreshPage ();
	}

	public void navigatePage(int delta) {
		navigateTo(page + delta);
	}

	public void sortKeyChanged(int newKey) {
		if (sortKey == newKey) {
			return;
		}
		sortKey = newKey;
		navigateTo (1);
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
