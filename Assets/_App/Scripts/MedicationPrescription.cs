using System;

[System.Serializable]
public class MedicationPrescription
{
    public string RxNumber;
    public string PrescriberName;
    public int Refills;
    public DateTime DatePrescribed;

    public MedicationPrescription(string prescriberName, string rxNumber, int refills)
    {
        PrescriberName = prescriberName;
        RxNumber = rxNumber;
        Refills = refills;
        DatePrescribed = DateTime.Now;
    }
}