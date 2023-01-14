using UnityEngine;

[System.Serializable]
public class Medication
{
    public string Name;
    public MedicationDosage Dosage;
    public MedicationPrescription Prescription;
    public float ReminderTime;
    public int Frequency;
    public AudioClip ReminderSound;

    public void TakeMedication()
    {
        // Perform actions when medication is taken
        Debug.Log("Medication taken: " + Name);
    }
}