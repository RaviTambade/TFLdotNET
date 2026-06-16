namespace MaxNewYorkInsurance.Managers;

using MaxNewYorkInsurance.Agents;

public class NotificationManager
{
    public event NotificationAgent? emailSent;
    public event NotificationAgent? smsSent;
    public event NotificationAgent? policyDocumentSent;

    public void SendEmail(string message)
        => emailSent?.Invoke(message);

    public void SendSMS(string message)
        => smsSent?.Invoke(message);

    public void SendPolicyDocument(string policyNumber)
        => policyDocumentSent?.Invoke(policyNumber);
}