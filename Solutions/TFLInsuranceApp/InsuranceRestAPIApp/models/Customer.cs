namespace MaxNewYorkInsurance.Models
{
    public class Customer
    {
        // Identity
        public int CustomerId { get; set; }
        public string CustomerCode { get; set; } = string.Empty;

        // Personal Information
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } = string.Empty;

        // Contact Information
        public string Email { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public string AlternateMobileNumber { get; set; } = string.Empty;

        // Address
        public string AddressLine1 { get; set; } = string.Empty;
        public string AddressLine2 { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

        // Government Identity
        public string PanNumber { get; set; } = string.Empty;
        public string AadhaarNumber { get; set; } = string.Empty;

        // Employment Information
        public string Occupation { get; set; } = string.Empty;
        public decimal AnnualIncome { get; set; }

        // Nominee Information
        public string NomineeName { get; set; } = string.Empty;
        public string NomineeRelationship { get; set; } = string.Empty;
        public string NomineeContactNumber { get; set; } = string.Empty;

        // Insurance Details
        public DateTime RegistrationDate { get; set; }
        public bool IsActive { get; set; }
        public int TotalPoliciesPurchased { get; set; }

        // Convenience Property
        //annonymous Function

        public string FullName => $"{FirstName} {LastName}";
    
        public override string ToString()
        {
            return $"{CustomerId} - {FullName} ({Email}, {MobileNumber})";
        }
    }
}