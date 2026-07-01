using MaxNewYorkInsurance.Models;
using MaxNewYorkInsurance.Repositories;
namespace MaxNewYorkInsurance.Departments;

public class ClaimDepartment
{



    public void OnClaimRegistered(Claim claim)
    {
        ClaimsRepository claimsRepository = new ClaimsRepository();
        PolicyRepository policyRepository = new PolicyRepository();
        List<Policy> policies = policyRepository.GetAllPolicies();
        bool status = false;
        foreach (Policy policy in policies)
        {
            if (policy.PolicyNumber == claim.PolicyNumber)
            {
                List<Claim> claims = claimsRepository.GetAllRegisterClaim();
                claims.Add(claim);
                claimsRepository.SaveRegisterClaim(claims);
                status = true;
                break;
            }
        }
        if (status)
        {
            Console.WriteLine("====================================");
            Console.WriteLine("Claims Department");
            Console.WriteLine($"Claim {claim.ClaimId} registered successfully.");
            Console.WriteLine("Claim forwarded for verification.");
            Console.WriteLine("====================================");
        }
        else
        {
            Console.WriteLine("====================================");
            Console.WriteLine("Claims Department");
            Console.WriteLine($"Claim {claim.ClaimId} not  registered.");
            Console.WriteLine("====================================");
        }

    }

    public void OnClaimVerified(Claim claim)
    {
        Console.WriteLine("====================================");
        Console.WriteLine($"Claim {claim.ClaimId} has been verified.");
        Console.WriteLine("Supporting documents validated.");
        Console.WriteLine("====================================");
    }

    public void OnClaimApproved(Claim claim)
    {
        bool status = false;
        ClaimsRepository claimsRepository = new ClaimsRepository();
        List<Claim> claims = claimsRepository.GetAllRegisterClaim();

        foreach (Claim claim2 in claims)
        {
            if (claim2.ClaimId == claim.ClaimId)
            {
                if (claim2.Status != "Approved")
                {
                    claim2.Status = "Approved";
                    claim2.Remarks = "Documents verified successfully";
                    // calculate approved amount (90% of claim) if needed
                    decimal approvedAmount = claim2.ClaimAmount * 90 / 100;
                    claim2.ApprovedAmount = approvedAmount;
                    claimsRepository.SaveRegisterClaim(claims);
                    status = true;
                    break;
                }
            }
        }
        if (status)
        {
            Console.WriteLine("====================================");
            Console.WriteLine($"Claim {claim.ClaimId} approved.");
            Console.WriteLine("Settlement process initiated.");
            Console.WriteLine("====================================");
        }
        else
        {
            Console.WriteLine("====================================");
            Console.WriteLine($"Claim {claim.ClaimId} not approved .");
            Console.WriteLine("register claim first");
            Console.WriteLine("====================================");
        }


    }
    public void OnClaimRejected(Claim claim)
    {
        bool status = false;
        ClaimsRepository claimsRepository = new ClaimsRepository();
        List<Claim> claims = claimsRepository.GetAllRegisterClaim();

        foreach (Claim claim2 in claims)
        {
            if (claim2.ClaimId == claim.ClaimId)
            {

                if (claim2.Status != "Approved")
                {
                    claim2.Status = "Rejected";
                    claim2.Remarks = "Documents not verified";
                    claimsRepository.SaveRegisterClaim(claims);
                    status = true;
                    break;
                }

            }
        }
        if (status)
        {
            Console.WriteLine("====================================");
            Console.WriteLine($"Claim {claim.ClaimId} rejected.");
            Console.WriteLine("Customer will be notified with the reason.");
            Console.WriteLine("====================================");
        }
        else
        {
            Console.WriteLine("====================================");
            Console.WriteLine($"Claim {claim.ClaimId} approved.");
            Console.WriteLine("====================================");
        }
    }

    public void OnClaimSettled(Claim claim)
    {
        bool status = false;
        ClaimsRepository claimsRepository = new ClaimsRepository();
        List<Claim> claims = claimsRepository.GetAllRegisterClaim();

        foreach (Claim claim2 in claims)
        {
            if (claim2.ClaimId == claim.ClaimId)
            {
                if (claim2.Status == "Approved")
                {
                    claim2.Status="Settled";
                    claim2.SettlementDate = DateTime.Now;
                    claimsRepository.SaveRegisterClaim(claims);
                    status = true;
                    break;
                }
            }
        }
        if (status)
        {
                Console.WriteLine("====================================");
            Console.WriteLine($"Claim {claim.ClaimId} settled successfully.");
            Console.WriteLine("Payment released to the customer.");
            Console.WriteLine("====================================");
        }
        else
        {
            Console.WriteLine("====================================");
            Console.WriteLine($"Claim {claim.ClaimId}  not settled.");
            Console.WriteLine("====================================");
        }
    }

    public void OnFraudCheckRequested(Claim claim)
    {
        Console.WriteLine("====================================");
        Console.WriteLine($"Fraud investigation initiated for Claim {claim.ClaimId}.");
        Console.WriteLine("Claim sent to the investigation team.");
        Console.WriteLine("====================================");
    }
    public void OnClaimReopened(Claim claim)
    {
        Console.WriteLine("====================================");
        Console.WriteLine($"Claim {claim.ClaimId} has been reopened.");
        Console.WriteLine("The claim will be reviewed again.");
        Console.WriteLine("====================================");
    }

    public void OnClaimClosed(Claim claim)
    {
        Console.WriteLine("====================================");
        Console.WriteLine($"Claim {claim.ClaimId} closed.");
        Console.WriteLine("No further processing is pending.");
        Console.WriteLine("====================================");
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
