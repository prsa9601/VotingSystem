using Microsoft.AspNetCore.Routing.Matching;
using System.ComponentModel.DataAnnotations;

namespace VotingLibrary.Data.Entities
{
    public class User : BaseClass
    {
        //public Guid Id { get; private set; }

        [Phone]
        public string PhoneNumber { get; private set; }
        public string? FullName { get; private set; }
        public bool AccessVote { get; private set; } = false;
        public int VoteAccessNumber { get; set; }
        public Role Role { get; set; } = Role.User;

        public List<Guid> VotesId { get; set; } = new();

        public User()
        {
            
        }
        public User(string phoneNumber, int voteAccessNumber)
        {
            //Id = Guid.NewGuid();
            VoteAccessNumber = voteAccessNumber;
            PhoneNumber = phoneNumber;
        }
        public void SetFullName(string name)
        {
            FullName = name;
        }
        public void SetVoteAccessNumber(int voteAccessNumber)
        {
            VoteAccessNumber = voteAccessNumber;
        }
        public void SetRole(Role role)
        {
            Role = role;
        }
        public void AddVote(Guid voteId)
        {
            VotesId.Add(voteId);
        }
        public void SetPhoneNumber(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }
        public void AddVote(List<Guid> voteId)
        {
            foreach (var item in voteId)
            {
                if (!voteId.Any(i => i.Equals(item)))
                {
                    VotesId.Add(item);
                }

            }
        }
        public void ClearVote(List<Guid> id)
        {
            foreach (var item in id)
            {
                VotesId.Remove(item);
            }
        }
        public List<Guid> GetVotesForEdit(List<Guid> id)
        {
            var votes = VotesId;
            foreach (var item in id)
            {
                VotesId.Remove(item);
            }
            return votes;
        }
        public void ChangeAccessVoteVisibility() => AccessVote = true ? AccessVote = false : AccessVote = true;
    }
    public enum Role
    {
        Admin,
        Candidate,
        Voter,
        User
    }
}

