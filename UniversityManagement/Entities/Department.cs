namespace UniversityManagement.Entities
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CollegeId { get; set; }
        public College College { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}