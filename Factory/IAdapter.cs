namespace LottoSheli.SendPrinter.Settings.Factory
{
    public interface IAdapter<T>
    {
        T Get();
        void Save<T>();
    }
}
