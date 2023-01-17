using System;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MedicationEntryUI : MonoBehaviour
{
    public MedicationInventoryScriptableObject MedicationInventory;
    public TMP_InputField MedicationNameInput;
    public TMP_InputField MedicationRxNumberInput;
    public TMP_InputField MedicationQuantityInput;
    public TMP_InputField MedicationDosageAmountInput;
    public TMP_InputField MedicationDosageUnitInput;
    public TMP_InputField MedicationFrequencyInstructionsInput;
    public TMP_InputField MedicationFrequencyAmountInput;
    public TMP_Dropdown MedicationFrequencyTimeFrameDropdown;
    public TMP_InputField MedicationPrescriberNameInput;
    public TMP_InputField MedicationRefillsInput;
    public TMP_InputField MedicationLowReminderAmountInput;
    public Button AddMedicationButton;
    public Button RemoveMedicationButton;
    public Button EditMedicationButton;
    public GameObject MessageText;

    private Medication currentMedication;

    private void Start()
    {
        
        AddMedicationButton.onClick.AddListener(AddMedication);
        RemoveMedicationButton.onClick.AddListener(RemoveMedication);
        EditMedicationButton.onClick.AddListener(EditMedication);

        //Fill the dropdown with the names of the MedicationFrequency Enum
        MedicationFrequencyTimeFrameDropdown.ClearOptions();
        MedicationFrequencyTimeFrameDropdown.AddOptions(Enum.GetNames(typeof(MedicationFrequencyTimeFrame)).ToList());
    }

    private void OnEnable()
    {
        if (AppManager.Instance.currentMedication != null)
        {
            SetCurrentMedication(AppManager.Instance.currentMedication);
        }
        else
        {
            currentMedication = new Medication();
        }
    }

    public void SetCurrentMedication(Medication medication)
    {
        try
        {
            currentMedication = medication;
            MedicationNameInput.text = medication.Name;
            MedicationRxNumberInput.text = medication.Prescription.RxNumber;
            MedicationQuantityInput.text = MedicationInventory.GetMedicationQuantity(medication).ToString();
            MedicationDosageAmountInput.text = medication.Dosage.Amount.ToString();
            MedicationDosageUnitInput.text = medication.Dosage.Unit;
            MedicationFrequencyInstructionsInput.text = medication.Frequency.instructions;
            MedicationFrequencyAmountInput.text = medication.Frequency.amount.ToString();
            MedicationFrequencyTimeFrameDropdown.value = (int)medication.Frequency.timeFrame;
            MedicationPrescriberNameInput.text = medication.Prescription.PrescriberName;
            MedicationRefillsInput.text = medication.Prescription.Refills.ToString();
            MedicationLowReminderAmountInput.text = medication.LowMedicationAmount.ToString();
            ShowMessage($"Medication Set: {medication.Name}", Color.green);

        }
        catch (Exception e)
        {
            ShowMessage($"Error: {e.Message}", Color.red);
        }
    }

    private void AddMedication()
    {
        try
        {
            var med = MedicationInventory.MedicationStockList.Find(x =>
                    string.Equals(x.medication.Name, MedicationNameInput.text, StringComparison.InvariantCultureIgnoreCase))
                ?.medication;
            if (med != null)
            {
                ShowMessage($"Unable to add Medication: {med.Name} since it already exists.", Color.red);
            }
            else
            {
                int quantity = int.Parse(MedicationQuantityInput.text);
                currentMedication.Name = (MedicationNameInput.text);
                currentMedication.Dosage = new MedicationDosage(float.Parse(MedicationDosageAmountInput.text), MedicationDosageUnitInput.text);
                currentMedication.Frequency = new MedicationFrequency(MedicationFrequencyInstructionsInput.text,int.Parse(MedicationFrequencyAmountInput.text),(MedicationFrequencyTimeFrame)MedicationFrequencyTimeFrameDropdown.value);
                currentMedication.Prescription = new MedicationPrescription(MedicationPrescriberNameInput.text, MedicationRxNumberInput.text,int.Parse(MedicationRefillsInput.text));
                currentMedication.LowMedicationAmount = int.Parse(MedicationLowReminderAmountInput.text);
                MedicationInventory.AddMedication(currentMedication, quantity);
                ShowMessage($"Medication Added: {currentMedication.Name}", Color.green);
            }


        }
        catch (Exception e)
        {
            ShowMessage($"Error: {e.Message}", Color.red);
        }

    }

    private void RemoveMedication()
    {
        try
        {
            var med = MedicationInventory.MedicationStockList.Find(x =>
                    string.Equals(x.medication.Name, MedicationNameInput.text, StringComparison.InvariantCultureIgnoreCase))
                ?.medication;
            if (med != null)
            {
                PopupDisplayUI.instance.ShowPopup("Confirm Remove?", PopupDisplayUI.PopupPosition.Middle, () =>
                {
                    MedicationInventory.RemoveMedication(med);
                    ShowMessage($"Medication Removed: {med.Name}", Color.green);
                }, () => {});
            }
            else
            {
                ShowMessage($"Medication Not Found: {med.Name}", Color.red);
            }
        }
        catch (Exception e)
        {
            ShowMessage($"Error: {e.Message}", Color.red);
        }

    }
    private void EditMedication()
    {
        try
        {
            var med = MedicationInventory.MedicationStockList.Find(x =>
                    string.Equals(x.medication.Name, MedicationNameInput.text, StringComparison.InvariantCultureIgnoreCase))
                ?.medication;
        
            if (med != null)
            {
                currentMedication = med;
                int quantity = int.Parse(MedicationQuantityInput.text);
                currentMedication.Dosage = new MedicationDosage(float.Parse(MedicationDosageAmountInput.text), MedicationDosageUnitInput.text);
                currentMedication.Frequency = new MedicationFrequency(MedicationFrequencyInstructionsInput.text,int.Parse(MedicationFrequencyAmountInput.text),(MedicationFrequencyTimeFrame)MedicationFrequencyTimeFrameDropdown.value);
                currentMedication.Prescription = new MedicationPrescription(MedicationPrescriberNameInput.text, MedicationRxNumberInput.text,int.Parse(MedicationRefillsInput.text));
                currentMedication.LowMedicationAmount = int.Parse(MedicationLowReminderAmountInput.text);
                MedicationInventory.UpdateMedication(currentMedication, quantity);
                ShowMessage($"Medication Edited: {currentMedication.Name}", Color.green);
            }
            else
            {
                ShowMessage($"No Medication By the Name: {MedicationNameInput.text} to edit.", Color.red);
            }
        }
        catch (Exception e)
        {
            ShowMessage($"Error: {e.Message}", Color.red);
        }
    }
    private void ShowMessage(string message, Color color)
    {
        MessageText.SetActive(true);
        MessageText.GetComponent<TMP_Text>().color = color;
        MessageText.GetComponent<TMP_Text>().text = message;
    }
}
