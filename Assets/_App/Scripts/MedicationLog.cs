using System;
using System.Collections.Generic;

public class MedicationLog
{
    public Medication Medication { get; set; }
    public List<DateTime> DosesTaken { get; set; }
    public List<string> Notes { get; set; }

    public MedicationLog(Medication medication)
    {
        Medication = medication;
        DosesTaken = new List<DateTime>();
        Notes = new List<string>();
    }

    public void AddDose(DateTime doseTaken)
    {
        DosesTaken.Add(doseTaken);
    }

    public void AddNote(string note)
    {
        Notes.Add(note);
    }
}