namespace RecruitmentTracking.Models;

public class AuthStatus
{
    public bool Status { get; set; }

    public AuthStatus(bool status)
    {
        Status = status;
    }
}
