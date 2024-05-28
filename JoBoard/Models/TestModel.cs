using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JoBoard.Data;

namespace JoBoard.Models
{
    public class TestModel
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
    }
}