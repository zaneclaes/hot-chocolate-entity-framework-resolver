namespace OpenWorkShop.Data.GraphqlExtensions {
  public interface IModelId<TKey> where TKey : class {
    TKey Id { get; set; }
  }
}
