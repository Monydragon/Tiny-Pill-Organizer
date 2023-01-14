using UnityEngine;
using System;

[System.Serializable]
public class Reminder
{
    public Medication Medication;
    public DateTime ReminderTime;
    public bool IsActive;
    public Action OnReminderShown;

    public Reminder(Medication medication, int reminderNumber)
    {
        Medication = medication;
        ReminderTime = GetReminderTime(medication, reminderNumber);
        IsActive = true;
    }

    private DateTime GetReminderTime(Medication medication, int reminderNumber)
    {
        TimeSpan timeOfDay = TimeSpan.FromHours(medication.ReminderTime);
        return DateTime.Today + timeOfDay + TimeSpan.FromDays(reminderNumber);
    }

    public void ShowReminder()
    {
        if (OnReminderShown != null) OnReminderShown.Invoke();
        // Show reminder UI
        Debug.Log("Reminder for: " + Medication.Name);
    }
}