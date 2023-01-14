using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MedicationHistory")]
public class MedicationHistoryScriptableObject : ScriptableObject
{
    public List<Medication> Medications = new List<Medication>();
    public List<DateTime> StartDates = new List<DateTime>();
    public List<DateTime> EndDates = new List<DateTime>();
    public List<string> Notes = new List<string>();

    public void AddMedication(Medication medication, DateTime startDate, DateTime endDate, string note)
    {
        Medications.Add(medication);
        StartDates.Add(startDate);
        EndDates.Add(endDate);
        Notes.Add(note);
    }

    public void RemoveMedication(Medication medication)
    {
        int index = Medications.IndexOf(medication);
        Medications.RemoveAt(index);
        StartDates.RemoveAt(index);
        EndDates.RemoveAt(index);
        Notes.RemoveAt(index);
    }
}