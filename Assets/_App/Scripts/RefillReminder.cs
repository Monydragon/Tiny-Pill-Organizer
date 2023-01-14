using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class RefillReminder : MonoBehaviour
{
    public MedicationInventoryScriptableObject MedicationInventory;
    public int LowMedicationLevel = 5;
    public float CheckInterval = 60f;
    private float nextCheckTime = 0f;
    private List<Reminder> reminders = new List<Reminder>();

    private void Update()
    {
        if (Time.time > nextCheckTime)
        {
            nextCheckTime = Time.time + CheckInterval;
            CheckForLowMedicationLevel();
        }
    }

    public void GenerateReminders()
    {
        reminders.Clear();
        foreach (MedicationStock stock in MedicationInventory.MedicationStockList)
        {
            for (int i = 0; i < stock.medication.Frequency; i++)
            {
                Reminder reminder = new Reminder(stock.medication, i);
                reminder.ReminderTime = GetReminderTime(stock.medication, i);
                reminders.Add(reminder);
                ScheduleReminder(reminder);
            }
        }
    }

    private void CheckForLowMedicationLevel()
    {
        var lowMedications = MedicationInventory.MedicationStockList
            .Where(medication => medication.quantity <= LowMedicationLevel)
            .Select(medication => medication.medication);

        foreach (Medication medication in lowMedications)
        {
            if (medication.Prescription.Refills > 0 &&
                DateTime.Now.Subtract(medication.Prescription.DatePrescribed).TotalDays > 30)
            {
                SendRefillRequest(medication);
            }
        }
    }

    private void SendRefillRequest(Medication medication)
    {
        // Send refill request to the prescriber
        Debug.Log("Refill request sent for: " + medication.Name);
        medication.Prescription.Refills--;
    }
    
    private DateTime GetReminderTime(Medication medication, int reminderNumber)
    {
        TimeSpan timeOfDay = TimeSpan.FromHours(medication.ReminderTime);
        return DateTime.Today + timeOfDay + TimeSpan.FromDays(reminderNumber);
    }
    
    private void ScheduleReminder(Reminder reminder)
    {
        // Schedule the reminder using platform-specific APIs or a custom scheduling solution
        Debug.Log("Reminder scheduled for: " + reminder.ReminderTime);
    }
}
