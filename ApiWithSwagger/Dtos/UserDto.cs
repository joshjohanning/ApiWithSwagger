using ApiWithSwagger.Data.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiWithSwagger.Dtos
{
    public class UserDto
    {
        /// <summary>
        /// user email, only lowercase letters
        /// </summary>
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public List<Item> DepositItems { get; set; }
    }
}
