using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnackBar.Infrastructure.Data.Entities
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string AdminKey { get; set; }
    }
}
