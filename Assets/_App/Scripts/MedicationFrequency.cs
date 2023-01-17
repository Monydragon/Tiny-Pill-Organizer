public class MedicationFrequency
{
    public string instructions;
    public int amount;
    public MedicationFrequencyTimeFrame timeFrame;

    public MedicationFrequency(string instructions, int amount, MedicationFrequencyTimeFrame timeFrame)
    {
        this.instructions = instructions;
        this.amount = amount;
        this.timeFrame = timeFrame;
    }
}