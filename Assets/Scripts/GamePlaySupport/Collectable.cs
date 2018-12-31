using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides basic functionality for collectable objects, changes parent and
/// makse the object invisible and non colliding while in the inventory
/// </summary>
public abstract class Collectable : MonoBehaviour
{
    /// <summary>
    /// An AI agent collects an object
    /// </summary>
    /// <param name="agentData">The AI doing the collecting</param>
    public virtual void Collect(AgentData agentData)
    {
        // Parent to the collecting AI
        gameObject.transform.parent = agentData.transform;

        // We don't want to see it or collide with it
        gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;

        // Not visible to AI either
        gameObject.layer = 0;
    }

    /// <summary>
    /// An AI agent drops a collectable object, the object becomes physical and visible once
    /// again. It is no longer parented to the AI agent
    /// </summary>
    /// <param name="agentData">The AI agent dropping the collectable</param>
    /// <param name="position">The position to drop the object</param>
    public virtual void Drop(AgentData agentData, Vector3 position)
    {
        gameObject.transform.parent = null;
        gameObject.transform.position = position;

        gameObject.GetComponent<BoxCollider>().enabled = true;
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.layer = LayerMask.NameToLayer("VisibleToAI");
    }
}
