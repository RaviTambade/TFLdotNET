namespace MaxNewYorkInsurance.Managers;

using MaxNewYorkInsurance.Agents;

public class RenewalManager
{
    public event RenewalAgent? policyRenewed;
    public event RenewalAgent? renewalReminderSent;
    public event RenewalAgent? renewalExpired;

    public void RenewPolicy(string policyNumber)
        => policyRenewed?.Invoke(policyNumber);

    public void SendRenewalReminder(string policyNumber)
        => renewalReminderSent?.Invoke(policyNumber);

    public void ExpirePolicy(string policyNumber)
        => renewalExpired?.Invoke(policyNumber);
}