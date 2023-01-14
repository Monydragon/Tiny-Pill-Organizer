using System;

[System.Serializable]
public class MedicationPrescription
{
    public string PrescriberName;
    public int Refills;
    public DateTime DatePrescribed;

    public MedicationPrescription(string prescriberName)
    {
        PrescriberName = prescriberName;
        DatePrescribed = DateTime.Now;
    }
}