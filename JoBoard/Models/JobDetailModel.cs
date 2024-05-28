using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JoBoard.Models
{
    public class JobDetailModel
    {
        public JobModel Job { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Phone_Number { get; set; }
        public byte[] Resume { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string UserId { get; set; }
        public string CondidaId { get; set; }
        public int JobId { get; set; }
        public IndexViewModel Jobs { get; set; }
    }

}