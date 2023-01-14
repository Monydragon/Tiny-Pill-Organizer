using System;

[System.Serializable]
public class MedicationDosage
{
    public float Amount;
    public string Unit;

    public MedicationDosage(float amount, string unit)
    {
        Amount = amount;
        Unit = unit;
    }

    public override string ToString()
    {
        return Amount + " " + Unit;
    }
}