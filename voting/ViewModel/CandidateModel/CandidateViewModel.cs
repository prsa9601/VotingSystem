using System.ComponentModel.DataAnnotations;
using VotingLibrary.Core.Common.Attribute;

namespace voting.ViewModel.CandidateModel
{
    public class CandidateViewModel
    {
        public string FullName { get; set; }
        [Phone]
        //[IranianPhoneNumber]
        public string PhoneNumber { get; set; }
    }
    public class EditCandidateViewModel
    {
        public Guid Id { get; set; }    
        public string FullName { get; set; }
        [Phone]
        //[IranianPhoneNumber]
        public string PhoneNumber { get; set; }
    }
}
