namespace ServeMe.Repository.Models
{
    public class UserDbModel
    {
        public int UserID { get; set; }

        public string UserName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public bool ReceiveCommunication { get; set; }

        public int Point { get; set; }
    }
}
