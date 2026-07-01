namespace MaxNewYorkInsurance.Managers;
using MaxNewYorkInsurance.Models;
using MaxNewYorkInsurance.Agents;

public class PolicyAdminManager
{
    public event PolicyAgent? policyIssued;
    public event AssignPolicyAgent? policyAssigned;
    public event PolicyNumberAgent? policyDocumentGenerated;
    public event PolicyNumberAgent? nomineeUpdated;
    public event PolicyNumberAgent? beneficiaryChanged;
    public event PolicyNumberAgent? policyCancelled;

    public void AssignPolicyAgent(string policyNumber, Agent agent)
        => policyAssigned?.Invoke(policyNumber, agent);

    public void SendPolicyDocument(string policyNumber)
        => policyDocumentGenerated?.Invoke(policyNumber);

    public void UpdateNominee(string policyNumber)
        => nomineeUpdated?.Invoke(policyNumber);

    public void ChangeBeneficiary(string policyNumber)
        => beneficiaryChanged?.Invoke(policyNumber);

    public void CancelPolicy(string policyNumber)
    =>  policyCancelled?.Invoke(policyNumber);

    

}
