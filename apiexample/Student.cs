using System.ComponentModel.DataAnnotations;

namespace apiexample
{
    public class Student
    {
        [Key]
        public int Rno { get; set; }
        public string Name { get; set; }
        public string ?Branch { get; set; }
        public int ?Fees { get; set; }
    }
}
