namespace Services.Abstracts
{
    public interface IBaseService<TRequestDTO, TEntity, TResponseDTO>
    {
        TResponseDTO Create(TRequestDTO requestDTO);
    }
}