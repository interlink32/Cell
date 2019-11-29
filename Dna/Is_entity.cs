namespace Dna
{
    public interface Is_entity
    {
        long id { get; set; }
        void update(long owner, s_entity entity);
    }
}