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
}
 