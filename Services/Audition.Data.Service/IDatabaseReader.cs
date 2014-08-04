namespace Audition.Data.Service
{
    public interface IDatabaseReader
    {
        /// <summary>
        /// Reads and returns all data in the data source.
        /// </summary>
        /// <returns>
        /// The JSON string representing data
        /// </returns>
        string ReadAll();
    }
}