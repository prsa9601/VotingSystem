
using System.ComponentModel.DataAnnotations;
using VotingLibrary.Core.Common.Filter;
using VotingLibrary.Data.Entities;

namespace VotingLibrary.Core.Services.Services.DTOs
{
    public class UserFilterParam : BaseFilterParam
    {
        [Phone]
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
    
    }
    public class UserFilterForCandidateParam : BaseFilterParam
    {
        public Guid CandidateId { get; set; }
        public Guid ElectionId { get; set; }
        public List<Guid> VoteIds { get; set; }

    }
    public class UserFilterResult:BaseFilter<User>
    {

    }

    public class CandidateFilterParam : BaseFilterParam
    {
        public Guid electionId { get; set; }
    }
    public class CandidateFilterResult : BaseFilter<Candidate>
    {

    }
    public class ElectionFilterParam : BaseFilterParam
    {
        public bool? IsFinished { get; set; }

    }
    public class ElectionFilterResult : BaseFilter<Election>
    {

    }
    public class VoteFilterParam : BaseFilterParam
    {
        public Guid candidateId { get; set; }
        public Guid electionId { get; set; }
    }
    public class VoteFilterResult : BaseFilter<Vote>
    {

    }
}
