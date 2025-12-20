namespace MyAssessment.Entities;

public class Assessment
{
    //auto property
    public int Id { get; set; }

    public int? TestId { get; set; }
    public int? CandidateId { get; set; }

    public AssessmentStatus Status { get; set; } = AssessmentStatus.Created;

    public int? CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }

    public int? ModifiedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }

    public DateTime? ScheduledStart { get; set; }
    public DateTime? ScheduledEnd { get; set; }

    public int? DeletedBy { get; set; }
    public YesNo Deleted { get; set; } = YesNo.No;

    // Navigation Properties

}

public enum AssessmentStatus
{
    Created,
    Scheduled,
    Cancelled,
    Conducted
}

public enum YesNo
{
    Yes,
    No
}
