using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract base class for interfacing option behaviours to display. 
/// </summary>
public abstract class MenuAction : MonoBehaviour, IMenuAction
{
    /// <summary>
    /// Interface implementation allowing access to Actions. 
    /// </summary>
    void IMenuAction.ExecuteAction()
    {
        Action(); 
    }

    /// <summary>
    /// function call for executing Actions. 
    /// </summary>
    internal abstract void Action(); 
}
