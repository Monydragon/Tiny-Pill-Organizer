using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class MedicationUIDisplay : MonoBehaviour
{
    public TMP_Text medicationInfoText,rxNumberText, providerNameText, userNameText, frequencyText, refillText;
    public Medication medication;

    private MedicationEntryUI medicationEntry;
    private MedicationListUI medicationListUI;

    private void Awake()
    {
        medicationEntry = GameObject.FindObjectOfType<MedicationEntryUI>(true);
        medicationListUI = GameObject.FindObjectOfType<MedicationListUI>(true); 
    }

    public void UpdateDisplay()
    {
        if (medication != null)
        {
            medicationInfoText.text = $"{medication.Name} {medication.Dosage.Amount} {medication.Dosage.Unit}";
            rxNumberText.text = $"RX# {medication.Prescription.RxNumber}";
            providerNameText.text = $"Prescriber: {medication.Prescription.PrescriberName}";
            userNameText.text = $"For: {AppManager.Instance.currentUser.firstName} {AppManager.Instance.currentUser.lastName}";
            frequencyText.text= $"{medication.Frequency.instructions} {medication.Frequency.amount} {medication.Frequency.timeFrame}";
            refillText.text = $"Remaining Refills ({medication.Prescription.Refills})";
        }
    }

    public void SetCurrentMedication()
    {
        if (medication != null && medicationEntry != null && medicationListUI != null)
        {
            AppManager.Instance.currentMedication = medication;
            medicationListUI.gameObject.SetActive(false);
            medicationEntry.gameObject.SetActive(true);
        }
    }
}
