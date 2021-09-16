using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagItem : MonoBehaviour
{
    public RawImage uiPicture;
    private BagUI _bagUI;
    private Button _button;
    private void Awake()
    {
        uiPicture = GetComponent<RawImage>();
        _bagUI = GetComponentInParent<BagUI>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ClickThis);

    }
    public void ClickThis()
    {
        _bagUI.LoadToUI(this.name);
    }
}
