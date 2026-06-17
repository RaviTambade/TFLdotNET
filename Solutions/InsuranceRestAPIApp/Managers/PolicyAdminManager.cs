namespace MaxNewYorkInsurance.Managers;
using MaxNewYorkInsurance.Models;
using MaxNewYorkInsurance.Agents;

public class PolicyAdminManager
{
    public event PolicyAgent? policyIssued;
    public event PolicyAgent? policyAssigned;
    public event PolicyAgent? policyDocumentGenerated;
    public event PolicyAgent? nomineeUpdated;
    public event PolicyAgent? beneficiaryChanged;

    public void AssignPolicyAgent(Policy policy, Agent agent)
        => policyAssigned?.Invoke(policy, agent);

    public void SendPolicyDocument(Policy policy)
        => policyDocumentGenerated?.Invoke(policyNumber);

    public void UpdateNominee(string policyNumber)
        => nomineeUpdated?.Invoke(policyNumber);

    public void ChangeBeneficiary(string policyNumber)
        => beneficiaryChanged?.Invoke(policyNumber);
}