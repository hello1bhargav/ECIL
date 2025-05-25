namespace ECILWebApp.Models
{
    public class Request
    {
        public int Id { get; set; }
        public string Department { get; set; }
        public DateTime RequestDate { get; set; }
        public string Status { get; set; }
        public string ITChampionName { get; set; }
        public string ITChampionCode { get; set; }
        public string ITChampionDivision { get; set; }
    }
}