using MyApiProject.Domain.Entities.Leads;

namespace MyApiProject.Infrastructure.Services.Leads
{
    public class LeadConcurrencyException : Exception
    {
        public Lead? CurrentState { get; }
        public UpdateLeadRequest AttemptedChange { get; }

        public LeadConcurrencyException(string message, Lead? currentState, UpdateLeadRequest attemptedChange)
            : base(message)
        {
            CurrentState = currentState;
            AttemptedChange = attemptedChange;
        }
    }
}