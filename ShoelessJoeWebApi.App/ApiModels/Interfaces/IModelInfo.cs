namespace ShoelessJoeWebApi.App.ApiModels.Interfaces
{
    public interface IModelInfo
    {
        int ManufacterId { get; set; }
        int ModelId { get; set; }
        string ModelName { get; set; }
    }
}
