namespace MyAssessment.Services;
using MyAssessment.Entities;
public interface IAssessmentService
{

    Assessment  CreateAssessment(Object request);
    bool  UpdateAssessment(int assessmentId, Object request);
    bool DeleteAssessment(int assessmentId);
    Assessment  GetAssessmentById(int assessmentId);
    List<Assessment> GetAssessmentsBySubjectAsync(int subjectId);   
}