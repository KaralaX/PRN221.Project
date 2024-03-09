namespace PRN221.Project.Domain.Entities
{
    public partial class Doctor
    {
        public Doctor()
        {
            Schedules = new HashSet<Schedule>();
            ServiceReviews = new HashSet<ServiceReview>();
            Services = new HashSet<Service>();
        }

        public Guid Id { get; set; }
        public string UserId { get; set; } = null!;

        public virtual PersonalInformation? PersonalInformation { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
        public virtual ICollection<ServiceReview> ServiceReviews { get; set; }

        public virtual ICollection<Service> Services { get; set; }
    }
}
