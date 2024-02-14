namespace ExerciseAPI.ViewModels
{
    public class PagingVM
    {
        public int Draw { get; set; }
        public List<ColumnOrder> Order { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public Search Search { get; set; }

    }

    public class ColumnOrder
    {
        public int Column { get; set; }
        public string Dir { get; set; }
    }

    public class Search
    {
        public string Value { get; set; }
    }
}
