using System.IO.Pipelines;
using Microsoft.AspNetCore.Mvc;
using MyWebApiApp.Models;

namespace MyWebApiApp.Controllers;

//Attribute (Annotations) (decorators)  metadata

[ApiController]
[Route("api/[controller]")]
public class AssessmentController : ControllerBase
{ 

    private static readonly List<Assessment> _assessments = new(){
        new Assessment
        {
            Id = 1,
            TestId = 101,
            CandidateId = 1001,
            Status = AssessmentStatus.Created,
            CreatedBy = 1,
            CreatedOn = DateTime.Now.AddDays(-2),
            ScheduledStart = DateTime.Now.AddHours(1),
            ScheduledEnd = DateTime.Now.AddHours(2),
            Deleted = YesNo.No
        }
    };

    public AssessmentController()
    {  }

    public List<Assessment> Get()
    {
       return  _assessments;    
    }

    [HttpPost]
    public IActionResult Create([FromBody] Assessment assessment)
    {
        assessment.Id = _assessments.Max(a => a.Id) + 1;
        assessment.CreatedOn = DateTime.Now;
        assessment.Status = AssessmentStatus.Created;

        _assessments.Add(assessment);

        return Ok("Created");
    }


    
    // ===========================
    // PUT: api/assessment/1
    // ===========================
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] Assessment updatedAssessment)
    {
        var assessment = _assessments.FirstOrDefault(a => a.Id == id);

        if (assessment == null)
            return NotFound();

        assessment.TestId = updatedAssessment.TestId;
        assessment.CandidateId = updatedAssessment.CandidateId;
        assessment.Status = updatedAssessment.Status;
        assessment.ScheduledStart = updatedAssessment.ScheduledStart;
        assessment.ScheduledEnd = updatedAssessment.ScheduledEnd;
        assessment.ModifiedOn = DateTime.Now;

        return Ok(assessment);
    }
  

     // ===========================
    // DELETE: api/assessment/1
    // (Soft Delete)
    // ===========================
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var assessment = _assessments.FirstOrDefault(a => a.Id == id);

        if (assessment == null)
            return NotFound();

        assessment.Deleted = YesNo.Yes;
        assessment.DeletedBy = 1;
        assessment.ModifiedOn = DateTime.Now;
        return Ok(new { message = "Assessment deleted (soft delete)" });
    }
}