using System.ComponentModel.DataAnnotations;

namespace MyApiProject.Domain.Entities.Leads
{
    public class UpdateLeadRequest
    {
        public string Source { get; set; }
        public string ContactInfo { get; set; }
        public string AssignedTo { get; set; }
        public string Status { get; set; }

        [Timestamp]
        public uint RowVersion { get; set; }
    }
}