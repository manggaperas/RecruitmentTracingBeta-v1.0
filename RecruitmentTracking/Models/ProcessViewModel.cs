namespace RecruitmentTracking.Models;

public class ProcessViewModel
{
    public List<DataCandidateJobs>? Administration { get; set; }
    public List<DataCandidateJobs>? HRInterview { get; set; }
    public List<DataCandidateJobs>? UserInterview { get; set; }
    public List<DataCandidateJobs>? Offering { get; set; }

}