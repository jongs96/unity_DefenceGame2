using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterProperty : MonoBehaviour
{
    Animator _anim = null;
    protected Animator myAnim
    {
        get
        {
            if(_anim == null)
            {
                _anim = GetComponent<Animator>();
                if(_anim == null)
                {
                    _anim = GetComponentInChildren<Animator>();
                }
            }
            return _anim;
        }
    }

    Renderer _renderer = null;
    protected Renderer myRenderer
    {
        get
        {
            if(_renderer == null)
            {
                _renderer = GetComponent<Renderer>();
                if(_renderer == null)
                {
                    _renderer = GetComponentInChildren<Renderer>();
                }
            }
            return _renderer;
        }
    }

    Renderer[] _allRenderer = null;
    protected Renderer[] allRenderer
    {
        get
        {
            if(_allRenderer == null)
            {
                _allRenderer = GetComponentsInChildren<Renderer>();
            }
            return _allRenderer;
        }
    }
}
