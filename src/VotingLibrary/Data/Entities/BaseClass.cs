namespace VotingLibrary.Data.Entities
{
    public class BaseClass
    {
        public Guid Id { get; set; }

        public BaseClass()
        {
            Id = Guid.NewGuid();
        }
    }
}
