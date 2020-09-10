using System;

namespace MarketFlight.DTO
{
    public class UserDTO
    {
        public UserDTO( int userId, string firstName, string lastName )
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
