using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionUi : MonoBehaviour
{

    [SerializeField]
    private FirstPersonPlayerManager _firstPerson;
    private TMP_Text _text;

    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        IInteractable interactable = _firstPerson._highlightedObject;
        if (interactable != null)
        {
            _text.text = interactable.GetInteractableLabel();

        }
        else
        {
            _text.text = "";
        }
    }
}
