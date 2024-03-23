namespace PRN221.WebUi.Areas.Appointments.Pages;

public class AppointmentSlot
{
    public TimeSpan StartTime { get; set; }
    public string Duration { get; set; }
    public bool IsBooked { get; set; }
    public bool IsCurrent { get; set; } = false;
}