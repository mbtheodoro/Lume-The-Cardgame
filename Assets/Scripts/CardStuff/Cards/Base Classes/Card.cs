﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private string _cardName;

    public new string name
    {
        get
        {
            return _cardName;
        }

        set
        {
            _cardName = value;
        }
    }

    public virtual void SetParent(RectTransform parent)
    {
        RectTransform rect = (RectTransform)GetComponent<RectTransform>();
        rect.SetParent(parent);

        rect.sizeDelta = new Vector2(parent.sizeDelta.x, parent.sizeDelta.y);
        rect.anchoredPosition3D = Vector3.zero;
        rect.localScale = Vector3.one;

        rect.ForceUpdateRectTransforms();
    }
}
