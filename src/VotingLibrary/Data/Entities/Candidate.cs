using System.ComponentModel.DataAnnotations;

namespace VotingLibrary.Data.Entities
{
    public class Candidate : BaseClass
    {
        //public Guid Id { get; private set; }
        public string? FullName { get; set; }
        public bool IsWin { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public List<Guid> VotesId { get; set; }
        public List<Guid> ElectionsId { get; set; }

        private Candidate()
        {

        }
        public Candidate(string phoneNumber)
        {
            VotesId = new List<Guid>();
            ElectionsId = new List<Guid>();
            PhoneNumber = phoneNumber;
        }
        public void Edit(string fullName, string phoneNumber)
        {
            PhoneNumber = phoneNumber;
            FullName = fullName;
        }
        public void AddVotes(List<Guid> votesId)
        {
            VotesId.AddRange(votesId);
        }
        public void SetPhoneNumber(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }
        public void AddVote(Guid voteId)
        {
            VotesId.Add(voteId);
        }
        public void ClearVote(List<Guid> id)
        {
            foreach (var item in id)
            {
                VotesId.Remove(item);
            }
        }
        public void AddElections(List<Guid> electionsId)
        {
            ElectionsId.AddRange(electionsId);
        }
        public void AddElection(Guid election)
        {
            ElectionsId.Add(election);
        }
        public void ClearElection(Guid id)
        {
            ElectionsId.Remove(id);
        }
        public void ChangeIsWinVisibility() => IsWin = true ? IsWin = false : IsWin = true;
    }
}