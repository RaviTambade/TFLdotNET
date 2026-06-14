using  MaxNewYorkInsurance.Models;
namespace MaxNewYorkInsurance.Departments;
class ClaimDepartment
{
    public void OnClaimRegistered(Claim theClaim)
    {

         List<Claim> claims = GetAllRegisterClaim();
        claims.Add(theClaim);
        this.SaveRegisterClaim(claims);

        Console.WriteLine($"Claim created. for {policy.PolicyNumber}  policy ");
        Console.WriteLine($"Claim amount {claimAmount} Rs");
        
    }
}
 