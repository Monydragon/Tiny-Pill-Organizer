using System;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class Medication
{
    public static event Action<Medication> OnTakeMedication;
    public static event Action<Medication> OnScheduleMedication;
    
    public string Name;
    public MedicationDosage Dosage;
    public MedicationPrescription Prescription;
    public MedicationFrequency Frequency;
    public int LowMedicationAmount;
    
    public void TakeMedication()
    {
        // Perform actions when medication is taken
        Debug.Log("Medication taken: " + Name);
        OnTakeMedication?.Invoke(this);
    }

    public void ScheduleMedication()
    {
        Debug.Log("Medication Scheduled: " + Name);
        OnScheduleMedication?.Invoke(this);
    }
}