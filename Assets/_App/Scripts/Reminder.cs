using UnityEngine;
using System;

[System.Serializable]
public class Reminder
{
    public Medication Medication;
    public DateTime ReminderTime;
    public bool IsActive;
    public Action OnReminderShown;

    public void ShowReminder()
    {
        if (!IsActive) return;
        // code to display the reminder, for example:
        Debug.Log("Reminder: Take " + Medication.Name + " at " + ReminderTime.ToString());
        OnReminderShown?.Invoke();
    }

    public void ScheduleReminder()
    {
        // if (Medication.frequencyTimeFrame == MedicationFrequencyTimeFrame.AsNeeded) return;
        // if (Medication.frequencyTimeFrame == MedicationFrequencyTimeFrame.Weekly) return;
        //
        // int hoursBetweenDoses = 24 / (int)Medication.frequencyTimeFrame;
        // ReminderTime = ReminderTime.AddHours(hoursBetweenDoses);
        // IsActive = true;
    }
}
