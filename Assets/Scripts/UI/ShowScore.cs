using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Show the score on the main window, blues score is in blue, reds score in red
/// The score data is derived from the appropriate base
/// </summary>
public class ShowScore : MonoBehaviour
{
    // The base keeps the score
    public GameObject FriendlyBase;
    private SetScore _scoreData;
    private Text _scoreDisplayText;

    // Use this for initialization
    void Start ()
    {
        _scoreData = FriendlyBase.GetComponent<SetScore>();
        _scoreDisplayText = gameObject.GetComponent<Text>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    // update the UI
    public void OnGUI()
    {
        _scoreDisplayText.text = _scoreData.Score.ToString();
    }
}
