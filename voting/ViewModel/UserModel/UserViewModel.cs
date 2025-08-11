using System.ComponentModel.DataAnnotations;
using VotingLibrary.Core.Common.Attribute;
using VotingLibrary.Data.Entities;

namespace voting.ViewModel.UserModel
{
    public class UserViewModel
    {
        [Phone]
        //[IranianPhoneNumber]
        public string PhoneNumber { get; set; }
        public int VoteAccessNumber { get; set; }

    }
    public class EditUserViewModel
    {
        public Guid Id { get; set; }
        [Phone]
        //[IranianPhoneNumber]
        public string PhoneNumber { get; set; }
        public int VoteAccessNumber { get; set; }

    }
}
