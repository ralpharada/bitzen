namespace Bitzen.Core.Events
{
    public class ResultEvent : IEvent
    {
        public bool Success { get; }
        public object Data { get; } = null!;
        public long? TotalRows { get; }
        public int? Id { get; } = null!;
        public ResultEvent() { }
        public ResultEvent(bool _success, object _data, long? totalRows = null, int? id = null)
        {
            Success = _success;
            Data = _data;
            TotalRows = totalRows;
            Id = id;
        }
    }
}
