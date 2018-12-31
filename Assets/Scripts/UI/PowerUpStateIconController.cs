using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Shows the powerup status of the AI agent on the UI
/// </summary>
public class PowerUpStateIconController : MonoBehaviour
{
    public Sprite PowerupSprite;
    // We need this because otherwise a white square is drawn when no object is present
    public Sprite EmptySprite;

    private Image _icon;
    private AgentData _agentData;


    // Use this for initialization
    void Start ()
    {
        _agentData = GetComponentInParent<AgentData>();
        _icon = GetComponent<Image>();
    }

    /// <summary>
    /// Update the UI
    /// </summary>
    private void OnGUI()
    {
        if(_agentData.IsPoweredUp)
        {
            _icon.sprite = PowerupSprite;
        }
        else
        {
            _icon.sprite = EmptySprite;
        }
    }
}
