using  MaxNewYorkInsurance.Models;
using  MaxNewYorkInsurance.Repositories;
namespace MaxNewYorkInsurance.Departments;
public class ClaimDepartment
{
    public void OnClaimRegistered(Claim theClaim)
    {

        ClaimsRepository claimRepo = new ClaimsRepository();
        List<Claim> claims =claimRepo.GetAllRegisterClaim();
        claims.Add(theClaim);
        claimRepo.SaveRegisterClaim(claims);
        Console.WriteLine("Claim registered");
    }

    // View all registered claims
    public List<Claim> GetAllClaims()
    {
        return new List<Claim>();
    }

    // Find claim by Id
    public Claim? GetClaimById(int claimId)
    {
        return null;
    }

    // Find claim by policy number
    public Claim? GetClaimByPolicyNumber(string policyNumber)
    {
        return null;
    }

    // Verify submitted documents
    public bool VerifyDocuments(int claimId)
    {
        return false;
    }

    // Validate policy eligibility
    public bool ValidatePolicy(int claimId)
    {
        return false;
    }

    // Assign surveyor/investigator
    public void AssignSurveyor(int claimId, string surveyorName)
    {
    }

    // Calculate approved claim amount
    public decimal CalculateClaimAmount(int claimId)
    {
        return 0;
    }

    // Approve claim
    public void ApproveClaim(int claimId)
    {
    }

    // Reject claim
    public void RejectClaim(int claimId, string reason)
    {
    }

    // Mark claim as under investigation
    public void StartInvestigation(int claimId)
    {
    }

    // Complete investigation
    public void CompleteInvestigation(int claimId)
    {
    }

    // Settle claim payment
    public void SettleClaim(int claimId)
    {
    }

    // Cancel claim
    public void CancelClaim(int claimId)
    {
    }

    // Update claim details
    public void UpdateClaim(Claim claim)
    {
    }

    // Delete claim record
    public void DeleteClaim(int claimId)
    {
    }

    // Get claims by customer
    public List<Claim> GetClaimsByCustomer(int customerId)
    {
        return new List<Claim>();
    }

    // Get claims by policy
    public List<Claim> GetClaimsByPolicy(int policyId)
    {
        return new List<Claim>();
    }

    // Get pending claims
    public List<Claim> GetPendingClaims()
    {
        return new List<Claim>();
    }

    // Get approved claims
    public List<Claim> GetApprovedClaims()
    {
        return new List<Claim>();
    }

    // Get rejected claims
    public List<Claim> GetRejectedClaims()
    {
        return new List<Claim>();
    }

    // Generate claim summary report
    public void GenerateClaimReport()
    {
    }

    // Notify customer about claim status
    public void NotifyCustomer(int claimId)
    {
    }
}
 