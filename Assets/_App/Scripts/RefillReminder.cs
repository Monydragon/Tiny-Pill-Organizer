using System;
using System.Threading.Tasks;
using UnityEngine;

public class RefillReminder
{
    public Medication Medication;
    public MedicationInventoryScriptableObject MedicationInventory;
    public int LowMedicationLevel = 5;

    public void CheckForLowMedicationLevel()
    {
        int stock = MedicationInventory.GetMedicationQuantity(Medication);

        if (stock <= LowMedicationLevel)
        {
            ScheduleReminder();
        }
    }

    public async void ScheduleReminder()
    {
        // if (Medication.frequencyTimeFrame == MedicationFrequencyTimeFrame.AsNeeded) return;
        // if (Medication.frequencyTimeFrame == MedicationFrequencyTimeFrame.Weekly) return;
        //
        // int hoursBetweenDoses = 24 / (int)Medication.frequencyTimeFrame;
        // DateTime nextReminderTime = DateTime.Now.AddHours(hoursBetweenDoses);
        //
        // while (nextReminderTime < DateTime.Now)
        // {
        //     nextReminderTime = nextReminderTime.AddHours(hoursBetweenDoses);
        // }
        //
        // await ShowReminder(nextReminderTime);
    }

    private async Task ShowReminder(DateTime nextReminderTime)
    {
        TimeSpan timeUntilReminder = nextReminderTime - DateTime.Now;
        await Task.Delay((int)timeUntilReminder.TotalMilliseconds);
        Debug.Log("Reminder: Take your " + Medication.Name + " medication");
    }
}