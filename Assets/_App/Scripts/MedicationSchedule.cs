using UnityEngine;
using System.Collections.Generic;

public class MedicationSchedule : MonoBehaviour
{
    public MedicationInventoryScriptableObject MedicationInventory;
    public RefillReminder RefillReminder;
    public List<Reminder> Reminders;

    private void Start()
    {
        GenerateReminders();
        RefillReminder.MedicationInventory = MedicationInventory;
    }

    public void GenerateReminders()
    {
        Reminders.Clear();
        foreach (MedicationStock stock in MedicationInventory.MedicationStockList)
        {
            for (int i = 0; i < stock.medication.Frequency; i++)
            {
                Reminders.Add(new Reminder(stock.medication, i));
            }
        }
    }
}