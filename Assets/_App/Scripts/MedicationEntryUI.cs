using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MedicationEntryUI : MonoBehaviour
{
    public MedicationInventoryScriptableObject MedicationInventory;
    public TMP_InputField MedicationNameInput;
    public TMP_InputField MedicationQuantityInput;
    public TMP_InputField MedicationDosageAmountInput;
    public TMP_InputField MedicationDosageUnitInput;
    public TMP_InputField MedicationFrequencyInput;
    public TMP_InputField MedicationPrescriberNameInput;
    public TMP_InputField MedicationRefillsInput;
    public Button AddMedicationButton;
    public Button RemoveMedicationButton;

    private Medication currentMedication;

    private void Start()
    {
        AddMedicationButton.onClick.AddListener(AddMedication);
        RemoveMedicationButton.onClick.AddListener(RemoveMedication);
    }

    public void SetCurrentMedication(Medication medication)
    {
        currentMedication = medication;
        MedicationNameInput.text = medication.Name;
        MedicationQuantityInput.text = MedicationInventory.GetMedicationQuantity(medication).ToString();
        MedicationDosageAmountInput.text = medication.Dosage.Amount.ToString();
        MedicationDosageUnitInput.text = medication.Dosage.Unit;
        MedicationFrequencyInput.text = medication.Frequency.ToString();
        MedicationPrescriberNameInput.text = medication.Prescription.PrescriberName;
        MedicationRefillsInput.text = medication.Prescription.Refills.ToString();
    }

    private void AddMedication()
    {
        int quantity = int.Parse(MedicationQuantityInput.text);
        currentMedication.Dosage = new MedicationDosage(float.Parse(MedicationDosageAmountInput.text), MedicationDosageUnitInput.text);
        currentMedication.Frequency = int.Parse(MedicationFrequencyInput.text);
        currentMedication.Prescription = new MedicationPrescription(MedicationPrescriberNameInput.text);
        currentMedication.Prescription.Refills = int.Parse(MedicationRefillsInput.text);
        MedicationInventory.AddMedication(currentMedication, quantity);
    }

    private void RemoveMedication()
    {
        int quantity = int.Parse(MedicationQuantityInput.text);
        MedicationInventory.RemoveMedication(currentMedication);
    }
}
