using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Set the sprite to indicate the AI agents current state / action or activity, you can add to these
/// if you want or need to indicate more states or actions, just add the new sprites below as variables 
/// of type 'Sprite'.
/// 
/// Note that the use of the word 'states' does not mean you have to use a state machine, this is just a
/// way to represent what your AI is currently doing for debugging.
/// </summary>
public class AiMoodIconController : MonoBehaviour
{
    private Image _icon;
    private AgentData _agentData;

    // Add new sprites here to customise this, you will have to drag the sprite from the sprite list onto the public variable in the inspector
    public Sprite Idle;
    public Sprite Attacking;
    public Sprite Fleeing;
    public Sprite Winning;
    public Sprite Losing;
    public Sprite Dead;

    // Use this for initialization
    void Start ()
    {
        _agentData = GetComponentInParent<AgentData>();
        _icon = GetComponent<Image>();
        _icon.sprite = Idle;
    }

    /// <summary>
    /// This will update the sprite indicator depending on the value of the parameter which is an enum declared above
    /// If you want to add new states you will also have to update this method. The method uses a switch statement
    /// to select the sprite depending on the state or action of the AI, to add new states add them to the switch statement
    /// </summary>
    /// <param name="agentData">Agent data to get the AI state we want to represent</param>
    public void OnGUI()
    {
        switch(_agentData.AiMood)
        {
            case AiMood.Idle:
                _icon.sprite = Idle;
                break;
            case AiMood.Attacking:
                _icon.sprite = Attacking;
                break;
            case AiMood.Fleeing:
                _icon.sprite = Fleeing;
                break;
            case AiMood.Winning:
                _icon.sprite = Winning;
                break;
            case AiMood.Losing:
                _icon.sprite = Losing;
                break;
            case AiMood.Dead:
                _icon.sprite = Dead;
                break;
            default:
                _icon.sprite = Idle;
                break;
        }
    }
}
