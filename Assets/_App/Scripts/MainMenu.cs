using System;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public MedicationInventoryScriptableObject MedicationInventory;
    public TMP_InputField EmailInput;
    public TMP_InputField LoginEmailInput;
    public TMP_InputField FirstNameInput;
    public TMP_InputField LastNameInput;
    public TMP_InputField DateOfBirthInput;
    public TMP_InputField PasswordInput;
    public TMP_InputField LoginPasswordInput;
    public TMP_InputField PasswordConfirmInput;
    public GameObject MessageText;

    

    public void CreateAccount()
    {
        string email = EmailInput.text;
        string firstName = FirstNameInput.text;
        string lastName = LastNameInput.text;
        string dateOfBirth = DateOfBirthInput.text;
        string password = PasswordInput.text;
        string passwordConfirm = PasswordConfirmInput.text;

        if (!IsValidEmail(email))
        {
            ShowMessage("Invalid email format. Please try again.",Color.red);
            return;
        }

        if (AppManager.Instance.users.Exists(x=> x.email == email))
        {
            ShowMessage("Email already in use. Please try again.",Color.red);
            return;
        }

        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
        {
            ShowMessage("Please enter your first and last name.",Color.red);
            return;
        }
        
        if (!IsValidDate(dateOfBirth))
        {
            ShowMessage("Invalid date format. Please try again.",Color.red);
            return;
        }

        if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(passwordConfirm))
        {
            ShowMessage("Please enter your password.",Color.red);
            return;
        }

        if (password != passwordConfirm)
        {
            ShowMessage("Passwords do not match. Please try again.",Color.red);
            return;
        }

        try
        {
            var user = new User(email, firstName,lastName,dateOfBirth, password, MedicationInventory.MedicationStockList);
            User.UserCreated(user);
            ShowMessage("Account Created!", Color.green);
        }
        catch (Exception e)
        {
            ShowMessage($"Error: {e.Message}",Color.red);
            return;
        }

    }

    


    public void Login()
    {
        string email = LoginEmailInput.text;
        string password = LoginPasswordInput.text;
        if (!IsValidEmail(email))
        {
            ShowMessage("Invalid email format. Please try again.", Color.red);
            return;
        }
        var user = AppManager.Instance.users.Find(x => String.Equals(x.email, email, StringComparison.CurrentCultureIgnoreCase));
        if (user == null)
        {
            ShowMessage("Email not found. Please try again.", Color.red);
            return;
        }
        else
        {
            if (user.password != password)
            {
                ShowMessage("Incorrect password. Please try again.", Color.red);
                return;
            }
            User.UserLogin(user);
            ShowMessage("Logged In!", Color.green);
            SceneManager.LoadScene(1);
        }
        // Load the main menu or navigate to the next scene
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
    private bool IsValidDate(string dateOfBirth)
    {
        //return if a string is a valid date that can be parsed.
        try
        {
            DateTime.Parse(dateOfBirth);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private void ShowMessage(string message, Color color)
    {
        MessageText.SetActive(true);
        MessageText.GetComponent<TMP_Text>().color = color;
        MessageText.GetComponent<TMP_Text>().text = message;
    }
}
