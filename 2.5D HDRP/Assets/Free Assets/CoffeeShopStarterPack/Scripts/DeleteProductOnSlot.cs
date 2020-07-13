// ******------------------------------------------------------******
// DeleteProductOnSlot.cs
//
// Author:
//       K.Sinan Acar <ksa@puzzledwizard.com>
//
// Copyright (c) 2019 PuzzledWizard
//
// ******------------------------------------------------------******
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
namespace PW
{

public class DeleteProductOnSlot : MonoBehaviour, IPointerClickHandler
{

    bool shownDeleteButton = true;
    Button deleteButton;
    [SerializeField]
    public int SlotIndex;

    private void Start()
    {
        deleteButton = GetComponentInChildren<Button>();
        //here we're adding onClick event to buttons from script.
        
        deleteButton.onClick.AddListener(delegate
        {
            //This will not be called unless user clicked on the button.
            DeletePlayerSlotImage();
        });

        ToggleDeleteMode();
    }


    void ToggleDeleteMode()
    {
        shownDeleteButton = !shownDeleteButton;
        ChangeUI();
    }

    void ChangeUI()
    {
        deleteButton.gameObject.SetActive(shownDeleteButton);
    }

    public void DeletePlayerSlotImage()
    {
        ToggleDeleteMode();
        GetComponent<Image>().sprite = null;
        BasicGameEvents.RaiseOnProductDeletedFromSlot(SlotIndex);
    }

    //This is for detecting click events on UI objects
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            ToggleDeleteMode();
        }
    }
}
}
