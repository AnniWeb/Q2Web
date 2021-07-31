namespace BusinessLogic.Abstractions.Model
{
    public class BaseModel<TUniqueId>
        where TUniqueId : struct
    {
        public TUniqueId Id { get; set; }
    }
}