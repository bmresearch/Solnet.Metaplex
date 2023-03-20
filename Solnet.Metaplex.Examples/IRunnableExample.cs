using System.Threading.Tasks;

namespace Solnet.Metaplex.Examples
{
    /// <summary>
    /// Defines functionality for an example.
    /// </summary>
    public interface IRunnableExample
    {
        /// <summary>
        /// Run the example.
        /// </summary>
        Task Run();
    }
}