using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighScoreUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] GameObject highScoreUIElementPrefab;
    [SerializeField] Transform elementWrapper;

	List<GameObject> uiElements = new List<GameObject>();

private void OnEnable(){HighScoreHandler.onHighScoreListChanged += UpdateUI;}
private void OnDisable(){HighScoreHandler.onHighScoreListChanged -= UpdateUI;}

private void UpdateUI (List<HighScoreElement> list){

	for(int i = 0; i < list.Count; i++){
		HighScoreElement el = list[i];
		if(el.score > 0 && el != null){
			if(i >= uiElements.Count){
				var inst = Instantiate(highScoreUIElementPrefab, Vector3.zero, Quaternion.identity);
			inst.transform.SetParent(elementWrapper,false);
			uiElements.Add(inst);
			}
			var texts = uiElements[i].GetComponentsInChildren<TextMeshProUGUI>();
			texts[0].text = el.name;
			texts[1].text = el.score.ToString();
		}
	}
}
}
