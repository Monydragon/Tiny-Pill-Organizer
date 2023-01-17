using System;
using System.Collections.Generic;

public class MedicationSchedule
{
    public Medication Medication;
    public MedicationInventoryScriptableObject MedicationInventory;
    public RefillReminder RefillReminder;
    public List<Reminder> Reminders;

    public void GenerateReminders()
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
        // Reminder reminder = new Reminder();
        // reminder.ReminderTime = nextReminderTime;
        // reminder.Medication = Medication;
        // Reminders.Add(reminder);
    }

    public void TakeMedication()
    {
        int stock = MedicationInventory.GetMedicationQuantity(Medication);
        if (stock > 0)
        {
            MedicationInventory.RemoveMedicationStock(Medication, 1);
            RefillReminder.CheckForLowMedicationLevel();
        }
    }
}