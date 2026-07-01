namespace MaxNewYorkInsurance.Managers;

using MaxNewYorkInsurance.Models;
using MaxNewYorkInsurance.Agents;

public class ClaimsManager
{
    public event ClaimsAgent? claimRegistered;
    public event ClaimsAgent? claimVerified;
    public event ClaimsAgent? claimApproved;
    public event ClaimsAgent? claimRejected;
    public event ClaimsAgent? claimSettled;
    public event ClaimsAgent? fraudCheckRequested;

    public void RegisterClaim(Claim claim)
        => claimRegistered?.Invoke(claim);

    public void ApproveClaim(Claim claim)
        => claimApproved?.Invoke(claim);

    public void RejectClaim(Claim claim)
        => claimRejected?.Invoke(claim);

    public void SettleClaim(Claim claim)
        => claimSettled?.Invoke(claim);

    public void DetectFraud(Claim claim)
        => fraudCheckRequested?.Invoke(claim);
}