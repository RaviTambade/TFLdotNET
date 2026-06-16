namespace MaxNewYorkInsurance.Managers;

using MaxNewYorkInsurance.Models;
using MaxNewYorkInsurance.Agents;

public class ComplianceManager
{
    public event AuditAgent? policyAudited;
    public event AuditAgent? complianceChecked;
    public event AuditAgent? regulatoryReportGenerated;

    public void AuditPolicy(string policyNumber)
        => policyAudited?.Invoke(policyNumber);

    public void CheckCompliance(string policyNumber)
        => complianceChecked?.Invoke(policyNumber);
}